namespace YourFood.Services.Controllers.API
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using YourFood.Data.UoW;

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