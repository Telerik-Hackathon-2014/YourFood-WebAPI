namespace ForumSystem.Common
{
    using System;
    using System.Linq;
    
    public class ConnectionStrings
    {
        public const string DefaultConnection = @"Data Source=.\sqlexpress;Initial Catalog=YourFood;Integrated Security=True";
        public const string CloudDatabaseConnection = @"Data Source=.\sqlexpress;Initial Catalog=YourFood;Integrated Security=True";
    }
}