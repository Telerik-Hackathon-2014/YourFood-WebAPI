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
   
    public class ProductsController : ODataController
    {
        private YourFoodDbContext db = new YourFoodDbContext();

        // GET: odata/Products
        [EnableQuery]
        public IQueryable<Product> GetProducts()
        {
            return this.db.Products;
        }

        // GET: odata/Products(5)
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri]
                                                int key)
        {
            return SingleResult.Create(this.db.Products.Where(product => product.Id == key));
        }

        // PUT: odata/Products(5)
        public IHttpActionResult Put([FromODataUri]
                                     int key, Delta<Product> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            Product product = this.db.Products.Find(key);
            if (product == null)
            {
                return this.NotFound();
            }

            patch.Put(product);

            try
            {
                this.db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ProductExists(key))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.Updated(product);
        }

        // POST: odata/Products
        public IHttpActionResult Post(Product product)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            this.db.Products.Add(product);
            this.db.SaveChanges();

            return this.Created(product);
        }

        // PATCH: odata/Products(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri]
                                       int key, Delta<Product> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            Product product = this.db.Products.Find(key);
            if (product == null)
            {
                return this.NotFound();
            }

            patch.Patch(product);

            try
            {
                this.db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ProductExists(key))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.Updated(product);
        }

        // DELETE: odata/Products(5)
        public IHttpActionResult Delete([FromODataUri]
                                        int key)
        {
            Product product = this.db.Products.Find(key);
            if (product == null)
            {
                return this.NotFound();
            }

            this.db.Products.Remove(product);
            this.db.SaveChanges();

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Products(5)/Category
        [EnableQuery]
        public SingleResult<ProductCategory> GetCategory([FromODataUri]
                                                         int key)
        {
            return SingleResult.Create(this.db.Products.Where(m => m.Id == key).Select(m => m.Category));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int key)
        {
            return this.db.Products.Count(e => e.Id == key) > 0;
        }
    }
}