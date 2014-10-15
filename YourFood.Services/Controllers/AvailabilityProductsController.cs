namespace YourFood.Services.Controllers
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.OData;
    using YourFood.Data.DbContext;
    using YourFood.Models;

    public class AvailabilityProductsController : ODataController
    {
        private YourFoodDbContext db = new YourFoodDbContext();

        // GET: odata/AvailabilityProducts
        [EnableQuery]
        public IQueryable<AvailabilityProduct> GetAvailabilityProducts()
        {
            return this.db.AvailabilityProducts;
        }

        // GET: odata/AvailabilityProducts(5)
        [EnableQuery]
        public SingleResult<AvailabilityProduct> GetAvailabilityProduct([FromODataUri]
                                                                        int key)
        {
            return SingleResult.Create(this.db.AvailabilityProducts.Where(availabilityProduct => availabilityProduct.Id == key));
        }

        // PUT: odata/AvailabilityProducts(5)
        public IHttpActionResult Put([FromODataUri]
                                     int key, Delta<AvailabilityProduct> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            AvailabilityProduct availabilityProduct = this.db.AvailabilityProducts.Find(key);
            if (availabilityProduct == null)
            {
                return this.NotFound();
            }

            patch.Put(availabilityProduct);

            try
            {
                this.db.SaveChanges();
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

        // POST: odata/AvailabilityProducts
        public IHttpActionResult Post(AvailabilityProduct availabilityProduct)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            this.db.AvailabilityProducts.Add(availabilityProduct);
            this.db.SaveChanges();

            return this.Created(availabilityProduct);
        }

        // PATCH: odata/AvailabilityProducts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri]
                                       int key, Delta<AvailabilityProduct> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            AvailabilityProduct availabilityProduct = this.db.AvailabilityProducts.Find(key);
            if (availabilityProduct == null)
            {
                return this.NotFound();
            }

            patch.Patch(availabilityProduct);

            try
            {
                this.db.SaveChanges();
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

        // DELETE: odata/AvailabilityProducts(5)
        public IHttpActionResult Delete([FromODataUri]
                                        int key)
        {
            AvailabilityProduct availabilityProduct = this.db.AvailabilityProducts.Find(key);
            if (availabilityProduct == null)
            {
                return this.NotFound();
            }

            this.db.AvailabilityProducts.Remove(availabilityProduct);
            this.db.SaveChanges();

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AvailabilityProducts(5)/Product
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri]
                                                int key)
        {
            return SingleResult.Create(this.db.AvailabilityProducts.Where(m => m.Id == key).Select(m => m.Product));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AvailabilityProductExists(int key)
        {
            return this.db.AvailabilityProducts.Count(e => e.Id == key) > 0;
        }
    }
}