using System.Configuration;

namespace ZZZ.Config
{
    public static class ConnectString
    {
        public static string Connection = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
    }
}
