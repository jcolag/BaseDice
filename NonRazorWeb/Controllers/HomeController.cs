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
                /// The running HTML report.
                /// </summary>
                private string html = string.Empty;

                /// <summary>
                /// The game is done.
                /// </summary>
                private bool done = false;

                /// <summary>
                /// Controller for the front page.
                /// </summary>
                /// <returns>The view.</returns>
                public ActionResult Index()
                {
                        Game g = new Game();
                        string s = string.Empty;

                        while (!done)
                        {
                                s = ShowNextPlay(g);
                                if (!string.IsNullOrWhiteSpace(s))
                                {
                                        this.html += s.Replace(Environment.NewLine, "<br>") + "<br>\n";
                                }
                        }

                        this.ViewData["Message"] = this.html;
                        return this.View();
                }

                /// <summary>
                /// Shows the next play.
                /// </summary>
                /// <returns>The next play.</returns>
                /// <param name="g">The game component.</param>
                private string ShowNextPlay(Game g)
                {
                        string s = string.Empty;

                        if (done)
                        {
                                // Do nothing
                        }
                        else if (g.Done())
                        {
                                s = g.FinalTally();
                                done = true;
                        }
                        else
                        {
                                s = g.TakeTurn();
                        }

                        return s;
                }
        }
}