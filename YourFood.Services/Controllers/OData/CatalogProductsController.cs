namespace YourFood.Services.Controllers.OData
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Http.OData;
    using YourFood.Data.UoW;
    using YourFood.Models;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CatalogProductsController : BaseODataController
    {
        public CatalogProductsController(IYourFoodData yourFoodData)
            : base(yourFoodData)
        {
        }

        // GET: api/CatalogProducts
        [EnableQuery]
        public IQueryable<CatalogProduct> GetCatalogProducts()
        {
            return this.Data.CatalogProducts.All();
        }

        // GET: api/CatalogProducts(5)
        [EnableQuery]
        public SingleResult<CatalogProduct> GetCatalogProduct([FromODataUri]
                                                              int key)
        {
            var product = this.Data.CatalogProducts.All()
                              .Where(catalogProduct => catalogProduct.Id == key);

            return SingleResult.Create(product);
        }

        // PUT: api/CatalogProducts(5)
        public IHttpActionResult Put([FromODataUri]
                                     int key, Delta<CatalogProduct> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            CatalogProduct catalogProduct = this.Data.CatalogProducts.Find(key);
            if (catalogProduct == null)
            {
                return this.NotFound();
            }

            patch.Put(catalogProduct);

            try
            {
                this.Data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.CatalogProductExists(key))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.Updated(catalogProduct);
        }

        // POST: api/CatalogProducts
        public IHttpActionResult Post(CatalogProduct catalogProduct)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.Data.CatalogProducts.Add(catalogProduct);
            this.Data.SaveChanges();

            return this.Created(catalogProduct);
        }

        // DELETE: api/CatalogProducts(5)
        public IHttpActionResult Delete([FromODataUri]
                                        int key)
        {
            CatalogProduct catalogProduct = this.Data.CatalogProducts.Find(key);
            if (catalogProduct == null)
            {
                return this.NotFound();
            }

            this.Data.CatalogProducts.Delete(catalogProduct);
            this.Data.SaveChanges();

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/CatalogProducts(5)/Product
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri]
                                                int key)
        {
            var product = this.Data.CatalogProducts.All()
                              .Where(m => m.Id == key)
                              .Select(m => m.Product);

            return SingleResult.Create(product);
        }
  
        private bool CatalogProductExists(int key)
        {
            return this.Data.CatalogProducts.All().Count(e => e.Id == key) > 0;
        }
    }
}