using System.Configuration;

namespace TestIFNSTools.Arhivator.Pathing
{
    public class PathName
    {
       public static string Path = ConfigurationManager.AppSettings["path"];
       public static string Path1 = ConfigurationManager.AppSettings["path1"];
       public static string Path2 = ConfigurationManager.AppSettings["path2"];
       public static string Path3 = ConfigurationManager.AppSettings["path3"];
       public static string Path4 = ConfigurationManager.AppSettings["path4"]; 
    }

    public static class ConnectString
    { 
       public static string Connection = ConfigurationManager.ConnectionStrings["DBF"].ConnectionString;
    }

    public static class PathMiNdfl
    {
        public static string PathResult = ConfigurationManager.AppSettings["PathResult"];
        public static string PathResultAuto = ConfigurationManager.AppSettings["PathResultAuto"];
    }
}
