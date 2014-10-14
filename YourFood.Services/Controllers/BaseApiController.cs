namespace YourFood.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using YourFood.Data.UoW;

    [EnableCors("*", "*", "*")]
    public class BaseApiController : ApiController
    {
        private IYourFoodData data;

        public BaseApiController(IYourFoodData data)
        {
            this.data = data;
        }

        protected IYourFoodData Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }
    }
}