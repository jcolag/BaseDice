// <copyright file="HomeController.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
[assembly: System.Reflection.AssemblyVersion("1.0.*")]
[assembly: System.CLSCompliant(true)]
[assembly: System.Runtime.InteropServices.ComVisible(false)]

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
                        HttpSessionStateBase session = this.Session;
                        Game g = new Game();
                        string s = string.Empty;
                        string html = string.Empty;
                        bool done = false;
                        string nl = Environment.NewLine;

                        if (session == null)
                        {
                                throw new ObjectDisposedException(GetType().Name);
                        }

                        html = "<h1>Play ball&hellip;!</h1>" + nl;
                        session.Add("Game", g);
                        session.Add("Done", done);

                        while (!done)
                        {
                                s = this.ShowNextPlay(null);
                                if (!string.IsNullOrWhiteSpace(s))
                                {
                                        html += s.Replace(nl, "<br>") + "<br>" + nl;
                                }

                                if (session["Done"] != null)
                                {
                                        done = (bool)session["Done"];
                                }
                        }

                        this.ViewData["Message"] = html;
                        session.Add("Html", html);
                        return this.View();
                }

                /// <summary>
                /// Shows the next play.
                /// </summary>
                /// <returns>The next play.</returns>
                /// <param name="game">The game component.</param>
                private string ShowNextPlay(Game game)
                {
                        HttpSessionStateBase session = this.Session;
                        Game g = game;
                        string s = string.Empty;

                        if (game == null)
                        {
                                g = (Game)session["Game"];
                        }

                        if (session["Done"] != null && (bool)session["Done"] != false)
                        {
                                // Do nothing
                        }
                        else if (g.Done())
                        {
                                s = g.FinalTally();
                                session["Done"] = true;
                        }
                        else
                        {
                                s = g.TakeTurn();
                        }

                        return s;
                }
        }
}