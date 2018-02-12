using System;
using System.Data;
using System.Web;
using System.Windows;

namespace DBF
{
   public class Test : System.Web.UI.Page
    {
        public void Download(DataTable dt)
        {
            try
            {
                Byte[] bytes = (Byte[])dt.Rows[0]["ImageColumn"];
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application / vnd.ms - word";
                Response.AddHeader("content-disposition", "attachment;filename="
                + "sdfdf");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                
            }
        }
    }
}
