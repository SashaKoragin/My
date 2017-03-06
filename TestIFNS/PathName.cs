using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TestIFNSTools
{
    public static class PathName
    {
       public static string path = ConfigurationManager.AppSettings["path"];
       public static string path1 = ConfigurationManager.AppSettings["path1"];
       public static string path2 = ConfigurationManager.AppSettings["path2"]; 
    }

}
