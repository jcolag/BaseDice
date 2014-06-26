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
                        string html = string.Empty;

                        if (session == null)
                        {
                                throw new ObjectDisposedException(GetType().Name);
                        }

                        html = "<h1>Play ball&hellip;!</h1>" + Environment.NewLine;
                        session.Add("Game", new Game());
                        session.Add("Done", false);

                        this.ViewData["Message"] = html;
                        session.Add("Html", html);
                        return this.View();
                }

                /// <summary>
                /// Controller for the next page.
                /// </summary>
                /// <returns>The next view.</returns>
                [AcceptVerbs(HttpVerbs.Get)]
                public ActionResult Next()
                {
                        HttpSessionStateBase session = this.Session;
                        Game g = (Game)session["Game"];
                        System.Collections.ObjectModel.Collection<int> roll;
                        bool done = (bool)session["Done"];
                        string html = (string)session["Html"];
                        string s = string.Empty;
                        string message = string.Empty;
                        string nl = Environment.NewLine;
                        string dice = string.Empty;

                        if (session == null)
                        {
                                throw new ObjectDisposedException(GetType().Name);
                        }

                        if (done)
                        {
                                return null;
                        }

                        s = this.ShowNextPlay(null);
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                                s = s.Replace(nl, "<br>").Replace(" * ", "<b>").Replace(" *", "</b>") +
                                        "<br>" + nl;
                        }

                        roll = g.LastRoll();
                        foreach (int die in roll)
                        {
                                dice += "<img src=\"/Images/d" + die.ToString() + "pip.png\">" + nl;
                        }

                        message = dice + "<br>" + s +
                                "<img src=\"/Images/Diamond" + g.Diamond() + ".png\"><br><br>" + nl;
                        html += message;
                        this.ViewData["Message"] = message;
                        this.ViewData["History"] = html;
                        session["Html"] = html;
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