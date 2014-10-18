namespace YourFood.Common
{
    using System;
    using System.Linq;
    
    public class ConnectionStrings
    {
        public const string DefaultConnection = @"Data Source=.\sqlexpress;Initial Catalog=YourFood;Integrated Security=True";
        public const string CloudDatabaseConnection = @"Server=1ea8c815-907c-4522-b728-a3c800e5bde9.sqlserver.sequelizer.com;Database=db1ea8c815907c4522b728a3c800e5bde9;User ID=wrvjkpafltseffou;Password=ADWaHKWxADxZTXSz2X6XrqQaBhjveCkcj3v4RN6r3B34oiAKbsrwpTEjVWFAzzs7;";
        public const string EverliveAppKey = "Co6BSJivwBpDikcY";
    }
}