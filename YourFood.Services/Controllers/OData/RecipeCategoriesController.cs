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
    public class RecipeCategoriesController : BaseODataController
    {
        public RecipeCategoriesController(IYourFoodData yourFoodData)
            : base(yourFoodData)
        {
        }

        // GET: api/RecipeCategories
        [EnableQuery]
        public IQueryable<RecipeCategory> GetRecipeCategories()
        {
            return this.Data.RecipeCategories.All();
        }

        // GET: api/RecipeCategories(5)
        [EnableQuery]
        public SingleResult<RecipeCategory> GetRecipeCategory([FromODataUri]
                                                              int key)
        {
            var category = this.Data.RecipeCategories
                               .All()
                               .Where(recipeCategory => recipeCategory.Id == key);

            return SingleResult.Create(category);
        }

        // PUT: api/RecipeCategories(5)
        public IHttpActionResult Put([FromODataUri]
                                     int key, Delta<RecipeCategory> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            RecipeCategory recipeCategory = this.Data.RecipeCategories.Find(key);
            if (recipeCategory == null)
            {
                return this.NotFound();
            }

            patch.Put(recipeCategory);

            try
            {
                this.Data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.RecipeCategoryExists(key))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.Updated(recipeCategory);
        }

        // POST: api/RecipeCategories
        public IHttpActionResult Post(RecipeCategory recipeCategory)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.Data.RecipeCategories.Add(recipeCategory);
            this.Data.SaveChanges();

            return this.Created(recipeCategory);
        }
       
        // DELETE: api/RecipeCategories(5)
        public IHttpActionResult Delete([FromODataUri]
                                        int key)
        {
            RecipeCategory recipeCategory = this.Data.RecipeCategories.Find(key);
            if (recipeCategory == null)
            {
                return this.NotFound();
            }

            this.Data.RecipeCategories.Delete(recipeCategory);
            this.Data.SaveChanges();

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/RecipeCategories(5)/Recipes
        [EnableQuery]
        public IQueryable<Recipe> GetRecipes([FromODataUri]
                                             int key)
        {
            var recipes = this.Data.RecipeCategories
                              .All()
                              .Where(m => m.Id == key)
                              .SelectMany(m => m.Recipes);
            return recipes;
        }

        private bool RecipeCategoryExists(int key)
        {
            return this.Data.RecipeCategories.All().Count(e => e.Id == key) > 0;
        }
    }
}