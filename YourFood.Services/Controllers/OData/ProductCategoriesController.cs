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
  
    public class ProductCategoriesController : BaseODataController
    {
        public ProductCategoriesController(IYourFoodData yourFoodData)
            : base(yourFoodData)
        {
        }

        // GET: api/ProductCategories
        [EnableQuery]
        public IQueryable<ProductCategory> GetProductCategories()
        {
            return this.Data.ProductCategories.All();
        }

        // GET: api/ProductCategories(5)
        [EnableQuery]
        public SingleResult<ProductCategory> GetProductCategory([FromODataUri]
                                                                int key)
        {
            var category = this.Data.ProductCategories
                               .All()
                               .Where(productCategory => productCategory.Id == key);

            return SingleResult.Create(category);
        }

        // PUT: api/ProductCategories(5)
        public IHttpActionResult Put([FromODataUri]
                                     int key, Delta<ProductCategory> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            ProductCategory productCategory = this.Data.ProductCategories.Find(key);
            if (productCategory == null)
            {
                return this.NotFound();
            }

            patch.Put(productCategory);

            try
            {
                this.Data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ProductCategoryExists(key))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.Updated(productCategory);
        }

        // POST: api/ProductCategories
        public IHttpActionResult Post(ProductCategory productCategory)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.Data.ProductCategories.Add(productCategory);
            this.Data.SaveChanges();

            return this.Created(productCategory);
        }

        // DELETE: api/ProductCategories(5)
        public IHttpActionResult Delete([FromODataUri]
                                        int key)
        {
            ProductCategory productCategory = this.Data.ProductCategories.Find(key);
            if (productCategory == null)
            {
                return this.NotFound();
            }

            this.Data.ProductCategories.Delete(productCategory);
            this.Data.SaveChanges();

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/ProductCategories(5)/Products
        [EnableQuery]
        public IQueryable<Product> GetProducts([FromODataUri]
                                               int key)
        {
            var products = this.Data.ProductCategories
                               .All()
                               .Where(m => m.Id == key)
                               .SelectMany(m => m.Products);
            return products;
        }

        private bool ProductCategoryExists(int key)
        {
            return this.Data.ProductCategories.All().Count(e => e.Id == key) > 0;
        }
    }
}