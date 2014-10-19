namespace YourFood.Services.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Http.Description;
    using YourFood.Data.Infrastructure;
    using YourFood.Data.UoW;
    using YourFood.Models;
    using YourFood.Services.Contracts;
    using YourFood.Services.Models;

    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReceiptScannerController : BaseApiController
    {
        private readonly IUserInfoProvider userInfoProvider;
        private readonly IReceiptScanner receiptScanner;
        private IList<Tag> tagsInMemory;

        public ReceiptScannerController(IYourFoodData yourFoodData, IUserInfoProvider userInfoProvider, IReceiptScanner receiptScanner)
            : base(yourFoodData)
        {
            this.userInfoProvider = userInfoProvider;
            this.receiptScanner = receiptScanner;
        }

        // POST: api/ReceiptScanner
        [ResponseType(typeof(ReceiptScannerModel))]
        public IHttpActionResult PostReceiptScannerModel(ReceiptScannerModel receiptScannerModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentUserId = this.userInfoProvider.GetUserId();
            var currentUser = this.Data.Users.Find(currentUserId);

            var imagePath = "images/" + Guid.NewGuid().ToString();
            var bitmap = this.SaveByteArrayAsImage(receiptScannerModel.ImageData);

            var p = HttpContext.Current.Request.PhysicalApplicationPath + "OCRS\\tessdata";
            var productIds = this.ExtractProductIds(this.receiptScanner.GetLines(bitmap, p));
            var addedProducts = new List<AvailabilityProduct>();

            foreach (var productId in productIds)
            {
                var lifeTime = this.Data.CatalogProducts.Find(productId).LifetimeInDays;
                var expirationDate = DateTime.Now.AddDays(lifeTime);
                var product = new AvailabilityProduct()
                {
                    ProductId = productId,
                    UserId = currentUserId,
                    DateAdded = DateTime.Now,
                    ExpirationDate = expirationDate
                };

                addedProducts.Add(product);
                currentUser.AvailabilityProducts.Add(product);
            }

            this.Data.SaveChanges();

            return this.Ok(addedProducts);
        }

        private IList<int> ExtractProductIds(IList<string> lines)
        {
            this.tagsInMemory = this.Data.Tags.All().ToList();
            var productIds = new List<int>();

            foreach (var line in lines)
            {
                var matches = Regex.Matches(line, @"[a-zA-Z]{2,}");
                var wordsOnLine = matches.Cast<Match>().Select(m => m.Value);

                if (wordsOnLine.Count() == 0)
                {
                    continue;
                }

                var hashSet = new HashSet<int>();

                foreach (var word in wordsOnLine)
                {
                    var wordMatches = this.GetMatches(word);
                    if (wordMatches.Count != 0)
                    {
                        if (hashSet.Count == 0)
                        {
                            hashSet.UnionWith(wordMatches);
                        }
                        else
                        {
                            hashSet.IntersectWith(wordMatches);
                        }
                    }
                }

                if (hashSet.Count != 0)
                {
                    productIds.Add(hashSet.First());
                }
            }

            return productIds;
        }

        private IList<int> GetMatches(string word)
        {
            return this.tagsInMemory.Where(t => t.Word == word).Select(t => t.ProductId).ToList();
        }

        private Bitmap SaveByteArrayAsImage(string base64String)
        {
            Bitmap bmpReturn = null;

            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn; 
        }
        //
        //private void SaveByteArrayAsImage(string fullOutputPath, string base64String)
        //{
        //    var bytes = Convert.FromBase64String(base64String);
        //    var p = HttpContext.Current.Request.PhysicalPath + "images";
        //    using (var imageFile = new FileStream(p, FileMode.Open, FileAccess.ReadWrite))
        //    {
        //        imageFile.Write(bytes, 0, bytes.Length);
        //        imageFile.Flush();
        //    }
        //}
    }
}