using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTest.Util
{
    public class HtmlResult : ActionResult
    {
        private string _htmlCode;

        public HtmlResult(string html)
        {
            _htmlCode = html;
        }
        public override void ExecuteResult(ControllerContext context)
        {
          string fullhtmlCode =  String.Format(
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
                         </body></html>", _htmlCode);
            context.HttpContext.Response.Write(fullhtmlCode);
        }
    }
}