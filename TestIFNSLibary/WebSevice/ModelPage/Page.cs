using System;

using System.Web.Mvc;
using TestIFNSLibary.WebSevice.Logica;

namespace TestIFNSLibary.WebSevice.ModelPage
{
   public class Page : ActionResult
    {
        public const string PageGlavnay = @"<!DOCTYPE html>
        <html>
         <meta charset=utp-8/>
                                  <head>
                                     <title>Главная страница</title>


                                  </head>
                                <body>
                                   <h3>FFFFFFFFFF</h3>
                                 <p>
                                       <input type=""button"" value=""Строка"" onclick=@Backup.Ff()/>
    </form></p>
                                 </body></html>";

        public override void ExecuteResult(ControllerContext context)
        {
            string fullhtmlCode =
                @"<!DOCTYPE html>
<html>
 <meta charset=utp-8/>
                          <head>
                             <title>Главная страница</title>
                             
                          </head>
    <body>
                            <h3>FFFFFFFFFF</h3>
                         {0}
                          <p>
                            <nav>
                                <ul class=""dropdown-menu"">
                                   <li><a href = ""/Home/Contact"">Купить</li>
                               </ul>
                          </nav>
                             </p>
                         </body></html>";
            context.HttpContext.Response.Write(fullhtmlCode);
        }

        //public void dd()
        //{
        //    Backup.Ff();
        //}
    }
}
