namespace YourFood.Common
{
    using System;
    using System.Linq;
    
    public class ConnectionStrings
    {
        public const string DefaultConnection = @"Data Source=.\sqlexpress;Initial Catalog=YourFood;Integrated Security=True";
        public const string CloudDatabaseConnection = @"Server=49efb363-8dcb-4c0c-8a29-a3c4014b3d99.sqlserver.sequelizer.com;Database=db49efb3638dcb4c0c8a29a3c4014b3d99;User ID=vdiumcqovosjusyr;Password=cLpSyJuD8Uqvx6rndvFBhcUdgKcXsAHowbUkeHGqVYYRXrk4pEszkDGaZmKWGDmE;";
        public const string EverliveAppKey = "aXTOUMq6Hy3At5wQ";
    }
}