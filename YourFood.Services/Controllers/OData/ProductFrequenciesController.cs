namespace YourFood.Services.Controllers.OData
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.OData;
    using YourFood.Data.UoW;
    using YourFood.Models;

    public class ProductFrequenciesController : BaseODataController
    {
        public ProductFrequenciesController(IYourFoodData yourFoodData)
            : base(yourFoodData)
        {
        }

        // GET: api/ProductFrequencies
        [EnableQuery]
        public IQueryable<ProductFrequency> GetProductFrequencies()
        {
            return this.Data.ProductFrequencies.All();
        }

        // GET: api/ProductFrequencies(5)
        [EnableQuery]
        public SingleResult<ProductFrequency> GetProductFrequency([FromODataUri]
                                                                  int key)
        {
            var product = this.Data.ProductFrequencies
                              .All()
                              .Where(productFrequency => productFrequency.ProductFrequencyId == key);

            return SingleResult.Create(product);
        }

        // PUT: api/ProductFrequencies(5)
        public IHttpActionResult Put([FromODataUri]
                                     int key, Delta<ProductFrequency> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            ProductFrequency productFrequency = this.Data.ProductFrequencies.Find(key);
            if (productFrequency == null)
            {
                return this.NotFound();
            }

            patch.Put(productFrequency);

            try
            {
                this.Data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ProductFrequencyExists(key))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.Updated(productFrequency);
        }

        // POST: api/ProductFrequencies
        public IHttpActionResult Post(ProductFrequency productFrequency)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.Data.ProductFrequencies.Add(productFrequency);
            this.Data.SaveChanges();

            return this.Created(productFrequency);
        }

        // DELETE: api/ProductFrequencies(5)
        public IHttpActionResult Delete([FromODataUri]
                                        int key)
        {
            ProductFrequency productFrequency = this.Data.ProductFrequencies.Find(key);
            if (productFrequency == null)
            {
                return this.NotFound();
            }

            this.Data.ProductFrequencies.Delete(productFrequency);
            this.Data.SaveChanges();

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/ProductFrequencies(5)/Product
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri]
                                                int key)
        {
            var product = this.Data.ProductFrequencies
                              .All()
                              .Where(m => m.ProductFrequencyId == key)
                              .Select(m => m.Product);

            return SingleResult.Create(product);
        }

        // GET: api/ProductFrequencies(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri]
                                          int key)
        {
            var user = this.Data.ProductFrequencies
                           .All()
                           .Where(m => m.ProductFrequencyId == key)
                           .Select(m => m.User);

            return SingleResult.Create(user);
        }

        private bool ProductFrequencyExists(int key)
        {
            return this.Data.ProductFrequencies.All().Count(e => e.ProductFrequencyId == key) > 0;
        }
    }
}