namespace YourFood.Services.Controllers
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
    public class ProductsController : BaseODataController
    {
        public ProductsController(IYourFoodData yourFoodData)
            : base(yourFoodData)
        {
        }

        // GET: api/Products
        [HttpGet]
        [EnableQuery]
        public IQueryable<Product> GetProducts()
        {
            return this.Data.Products.All();
        }

        // GET: api/Products(5)
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri]
                                                int key)
        {
            var product = this.Data.Products.All()
                              .Where(p => p.Id == key);

            return SingleResult.Create(product);
        }

        // PUT: api/Products(5)
        public IHttpActionResult Put([FromODataUri]
                                     int key, Delta<Product> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            Product product = this.Data.Products.Find(key);
            if (product == null)
            {
                return this.NotFound();
            }

            patch.Put(product);

            try
            {
                this.Data.SaveChanges();
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

        // POST: api/Products
        public IHttpActionResult Post(Product product)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.Data.Products.Add(product);
            this.Data.SaveChanges();

            return this.Created(product);
        }

        // PATCH: api/Products(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri]
                                       int key, Delta<Product> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            Product product = this.Data.Products.Find(key);
            if (product == null)
            {
                return this.NotFound();
            }

            patch.Patch(product);

            try
            {
                this.Data.SaveChanges();
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

        // DELETE: api/Products(5)
        public IHttpActionResult Delete([FromODataUri]
                                        int key)
        {
            var product = this.Data.Products.Find(key);
            if (product == null)
            {
                return this.NotFound();
            }

            this.Data.Products.Delete(product);
            this.Data.SaveChanges();

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/Products(5)/Category
        [EnableQuery]
        public SingleResult<ProductCategory> GetCategory([FromODataUri]
                                                         int key)
        {
            var product = this.Data.Products.All()
                              .Where(m => m.Id == key)
                              .Select(m => m.Category);

            return SingleResult.Create(product);
        }

        private bool ProductExists(int key)
        {
            return this.Data.Products.All().Count(e => e.Id == key) > 0;
        }
    }
}