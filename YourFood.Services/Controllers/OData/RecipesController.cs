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
    using YourFood.EverliveAPI.Contracts;
    using YourFood.Models;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RecipesController : BaseODataController
    {
        private readonly IImageUploader imageUploader;

        public RecipesController(IYourFoodData yourFoodData, IImageUploader imageUploader)
            : base(yourFoodData)
        {
            this.imageUploader = imageUploader;
        }

        // GET: api/Recipes
        [EnableQuery]
        public IQueryable<Recipe> GetRecipes()
        {
            return this.Data.Recipes.All();
        }

        // GET: api/Recipes(5)
        [EnableQuery]
        public SingleResult<Recipe> GetRecipe([FromODataUri]
                                              int key)
        {
            var recipe = this.Data.Recipes.All()
                             .Where(r => r.Id == key);

            return SingleResult.Create(recipe);
        }

        // PUT: api/Recipes(5)
        public IHttpActionResult Put([FromODataUri]
                                     int key, Delta<Recipe> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            Recipe recipe = this.Data.Recipes.Find(key);
            if (recipe == null)
            {
                return this.NotFound();
            }

            patch.Put(recipe);

            try
            {
                this.Data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.RecipeExists(key))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.Updated(recipe);
        }

        // POST: api/Recipes
        public IHttpActionResult Post(Recipe recipe)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var imageUrl = this.imageUploader.UrlFromBase64Image(recipe.ImageUrl);
            recipe.ImageUrl = imageUrl;

            this.Data.Recipes.Add(recipe);
            this.Data.SaveChanges();

            return this.Created(recipe);
        }

        // DELETE: api/Recipes(5)
        public IHttpActionResult Delete([FromODataUri]
                                        int key)
        {
            Recipe recipe = this.Data.Recipes.Find(key);
            if (recipe == null)
            {
                return this.NotFound();
            }

            this.Data.Recipes.Delete(recipe);
            this.Data.SaveChanges();

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/Recipes(5)/Category
        [EnableQuery]
        public SingleResult<RecipeCategory> GetCategory([FromODataUri]
                                                        int key)
        {
            var category = this.Data.Recipes
                               .All()
                               .Where(m => m.Id == key)
                               .Select(m => m.Category);

            return SingleResult.Create(category);
        }

        // GET: api/Recipes(5)/Ingredients
        [EnableQuery]
        public IQueryable<RecipeProduct> GetIngredients([FromODataUri]
                                                        int key)
        {
            return this.Data.Recipes.All()
                       .Where(m => m.Id == key)
                       .SelectMany(m => m.Ingredients);
        }

        private bool RecipeExists(int key)
        {
            return this.Data.Recipes.All().Count(e => e.Id == key) > 0;
        }
    }
}