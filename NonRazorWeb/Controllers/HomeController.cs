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
                        bool done = false;

                        html = "<h1>Play ball&hellip;!</h1>\n";
                        this.Session.Add("Game", g);
                        this.Session.Add("Done", done);

                        while (!done)
                        {
                                s = this.ShowNextPlay(null);
                                if (!string.IsNullOrWhiteSpace(s))
                                {
                                        html += s.Replace(Environment.NewLine, "<br>") + "<br>\n";
                                }

                                if (this.Session["Done"] != null)
                                {
                                        done = (bool)this.Session["Done"];
                                }
                        }

                        this.ViewData["Message"] = html;
                        this.Session.Add("Html", html);
                        return this.View();
                }

                /// <summary>
                /// Shows the next play.
                /// </summary>
                /// <returns>The next play.</returns>
                /// <param name="game">The game component.</param>
                private string ShowNextPlay(Game game)
                {
                        Game g = game;
                        string s = string.Empty;

                        if (game == null)
                        {
                                g = (Game)this.Session["Game"];
                        }

                        if (this.Session["Done"] != null && (bool)this.Session["Done"] != false)
                        {
                                // Do nothing
                        }
                        else if (g.Done())
                        {
                                s = g.FinalTally();
                                this.Session["Done"] = true;
                        }
                        else
                        {
                                s = g.TakeTurn();
                        }

                        return s;
                }
        }
}