namespace YourFood.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http.Cors;
    using System.Web.Http.OData;
    using YourFood.Data.UoW;

    [EnableCors("*", "*", "*")]
    public class BaseODataController : ODataController
    {
        private IYourFoodData data;

        public BaseODataController(IYourFoodData data)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Data.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}