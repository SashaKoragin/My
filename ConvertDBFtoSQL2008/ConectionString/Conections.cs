using System.Configuration;

namespace ConvertDBFtoSQL2008.ConectionString
{
    class Conections
    {
        public static string DbfConect = ConfigurationManager.ConnectionStrings["DBF"].ConnectionString;
        public static string SqlConection = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;

    }
}
