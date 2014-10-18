namespace YourFood.Services.Controllers.API
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;
    using YourFood.Data.UoW;
    using YourFood.Services.Models;

    public class ReceiptScannerController : BaseApiController
    {
        public ReceiptScannerController(IYourFoodData yourFoodData)
            : base(yourFoodData)
        {
        }

        // POST: api/ReceiptScanner
        [ResponseType(typeof(ReceiptScannerModel))]
        public IHttpActionResult PostReceiptScannerModel(ReceiptScannerModel receiptScannerModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            //
            // Logic
            //

            return this.Ok("All it's OK! " + receiptScannerModel.ImageData);
        }
    }
}