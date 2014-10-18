namespace YourFood.Services.Controllers.OData
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Http.OData;
    using YourFood.Data.Infrastructure;
    using YourFood.Data.UoW;
    using YourFood.Models;

    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AvailabilityProductsController : BaseODataController
    {
        private readonly IUserInfoProvider userInfoProvider;

        public AvailabilityProductsController(IYourFoodData yourFoodData, IUserInfoProvider userInfoProvider)
            : base(yourFoodData)
        {
            this.userInfoProvider = userInfoProvider;
        }

        // GET: api/AvailabilityProducts
        [EnableQuery]
        public IQueryable<AvailabilityProduct> GetAvailabilityProducts()
        {
            //
            // Check by UserId
            //
            var userId = this.userInfoProvider.GetUserId();
            return this.Data.AvailabilityProducts.All().Where(p => p.UserId == userId);
        }

        // GET: api/AvailabilityProducts(5)
        [EnableQuery]
        public SingleResult<AvailabilityProduct> GetAvailabilityProduct([FromODataUri]
                                                                        int key)
        {
            //
            // Check by UserId
            //
            var product = this.Data.AvailabilityProducts.All()
                              .Where(availabilityProduct => availabilityProduct.Id == key);

            return SingleResult.Create(product);
        }

        // PUT: api/AvailabilityProducts(5)
        public IHttpActionResult Put([FromODataUri]
                                     int key, Delta<AvailabilityProduct> patch)
        {
            //
            // Check by UserId
            //
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            AvailabilityProduct availabilityProduct = this.Data.AvailabilityProducts.Find(key);
            if (availabilityProduct == null)
            {
                return this.NotFound();
            }

            patch.Put(availabilityProduct);

            try
            {
                this.Data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.AvailabilityProductExists(key))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.Updated(availabilityProduct);
        }

        // POST: api/AvailabilityProducts
        public IHttpActionResult Post(AvailabilityProduct availabilityProduct)
        {
            //
            // Check by UserId
            //
            availabilityProduct.UserId = this.userInfoProvider.GetUserId();
            availabilityProduct.DateAdded = DateTime.Now;

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.Data.AvailabilityProducts.Add(availabilityProduct);
            this.Data.SaveChanges();

            return this.Created(availabilityProduct);
        }

        // DELETE: api/AvailabilityProducts(5)
        public IHttpActionResult Delete([FromODataUri]
                                        int key)
        {
            //
            // Check by UserId
            //
            var availabilityProduct = this.Data.AvailabilityProducts.Find(key);
            if (availabilityProduct == null)
            {
                return this.NotFound();
            }

            this.Data.AvailabilityProducts.Delete(availabilityProduct);
            this.Data.SaveChanges();

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/AvailabilityProducts(5)/Product
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri]
                                                int key)
        {
            //
            // Check by UserId
            //
            var product = this.Data.AvailabilityProducts.All()
                              .Where(m => m.Id == key)
                              .Select(m => m.Product);

            return SingleResult.Create(product);
        }

        private bool AvailabilityProductExists(int key)
        {
            return this.Data.AvailabilityProducts.All().Count(e => e.Id == key) > 0;
        }
    }
}