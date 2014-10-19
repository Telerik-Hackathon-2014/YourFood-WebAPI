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
    public class ShoppingListsController : BaseODataController
    {
        private readonly IUserInfoProvider userInfoProvider;

        public ShoppingListsController(IYourFoodData yourFoodData, IUserInfoProvider userInfoProvider)
            : base(yourFoodData)
        {
            this.userInfoProvider = userInfoProvider;
        }

        // GET: api/ShoppingLists
        [EnableQuery]
        public SingleResult<ShoppingList> GetShoppingLists()
        {
            var list = this.Data.ShoppingLists
                .All()
                .Where(sl => sl.UserId == this.userInfoProvider.GetUserId() && !sl.IsFinished);
            
            return SingleResult.Create(list);
        }

        // GET: api/ShoppingLists(5)
        [EnableQuery]
        public SingleResult<ShoppingList> GetShoppingList([FromODataUri]
                                                          int key)
        {
            var shoppingList = this.Data.ShoppingLists
                                   .All()
                                   .Where(s => s.Id == key);

            return SingleResult.Create(shoppingList);
        }

        // PUT: api/ShoppingLists(5)
        public IHttpActionResult Put([FromODataUri]
                                     int key, Delta<ShoppingList> patch)
        {
            this.Validate(patch.GetEntity());

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            ShoppingList shoppingList = this.Data.ShoppingLists.Find(key);
            if (shoppingList == null)
            {
                return this.NotFound();
            }

            patch.Put(shoppingList);

            try
            {
                this.Data.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ShoppingListExists(key))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.Updated(shoppingList);
        }

        // POST: api/ShoppingLists
        public IHttpActionResult Post()
        {
            ShoppingList shoppingList = new ShoppingList();
            shoppingList.DateCreated = DateTime.Now;
            shoppingList.IsFinished = false;
            shoppingList.UserId = this.userInfoProvider.GetUserId();

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.Data.ShoppingLists.Add(shoppingList);
            this.Data.SaveChanges();

            return this.Created(shoppingList);
        }

        // DELETE: api/ShoppingLists(5)
        public IHttpActionResult Delete([FromODataUri]
                                        int key)
        {
            ShoppingList shoppingList = this.Data.ShoppingLists.Find(key);
            if (shoppingList == null)
            {
                return this.NotFound();
            }

            this.Data.ShoppingLists.Delete(shoppingList);
            this.Data.SaveChanges();

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/ShoppingLists(5)/Products
        [EnableQuery]
        public IQueryable<RecipeProduct> GetProducts([FromODataUri]
                                                     int key)
        {
            var products = this.Data.ShoppingLists
                               .All()
                               .Where(m => m.Id == key)
                               .SelectMany(m => m.Products);

            return products;
        }

        // GET: api/ShoppingLists(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri]
                                          int key)
        {
            var user = this.Data.ShoppingLists
                           .All()
                           .Where(m => m.Id == key)
                           .Select(m => m.User);

            return SingleResult.Create(user);
        }

        private bool ShoppingListExists(int key)
        {
            return this.Data.ShoppingLists.All().Count(e => e.Id == key) > 0;
        }
    }
}