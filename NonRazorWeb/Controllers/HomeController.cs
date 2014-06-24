// <copyright file="HomeController.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace NonRazorWeb.Controllers
{
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Web;
        using System.Web.Mvc;
        using System.Web.Mvc.Ajax;
        using BaseDice;

        /// <summary>
        /// Home controller.
        /// </summary>
        public class HomeController : Controller
        {
                /// <summary>
                /// Controller for the front page.
                /// </summary>
                /// <returns>The view.</returns>
                public ActionResult Index()
                {
                        Game g = new Game();
                        string s = string.Empty;
                        string html = string.Empty;

                        while (g.Done())
                        {
                                s = g.TakeTurn();
                                if (!string.IsNullOrWhiteSpace(s))
                                {
                                        html += s.Replace(Environment.NewLine, "<br>") + "<br>\n";
                                }
                        }

                        html += g.FinalTally();

                        this.ViewData["Message"] = html;
                        return this.View();
                }
        }
}