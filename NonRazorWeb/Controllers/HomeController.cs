﻿// <copyright file="HomeController.cs" company="John Colagioia">
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

                        html = "<h1>Play Ball&hellip;!</h1>" + Environment.NewLine;
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
                        ViewDataDictionary viewdata = this.ViewData;

                        if (session == null)
                        {
                                throw new ObjectDisposedException(GetType().Name);
                        }

                        string message = this.ProcessTurn();
                        string html = (string)session["Html"];

                        viewdata["History"] = html;
                        message += HomeController.BreakPadding(message, 7);
                        viewdata["Message"] = message;
                        return this.View();
                }

                /// <summary>
                /// Controller for the roll partial page.
                /// </summary>
                /// <returns>The next view.</returns>
                [AcceptVerbs(HttpVerbs.Get)]
                public ActionResult Roll()
                {
                        string message = this.ProcessTurn();

                        if (this.Session == null)
                        {
                                throw new ObjectDisposedException(GetType().Name);
                        }

                        message += HomeController.BreakPadding(message, 7);
                        Response.Write(message);
                        return null;
                }

                /// <summary>
                /// Adds padding to the line breaks.
                /// </summary>
                /// <returns>The padding.</returns>
                /// <param name="message">Message to pad.</param>
                /// <param name="max">Maximum number of lines.</param>
                private static string BreakPadding(string message, int max)
                {
                        string pad = string.Empty;
                        string nl = Environment.NewLine;
                        string[] delim = { "<br>" };
                        int lines;

                        if (string.IsNullOrWhiteSpace(message))
                        {
                                return pad;
                        }

                        lines = message.Split(delim, StringSplitOptions.None).Length;
                        for (int i = lines; i < max; i++)
                        {
                                pad += "<br>" + nl;
                        }

                        return pad;
                }

                /// <summary>
                /// Merge the logic for the turn pages.
                /// </summary>
                /// <returns>The turn results.</returns>
                private string ProcessTurn()
                {
                        HttpSessionStateBase session = this.Session;
                        ViewDataDictionary viewdata = this.ViewData;
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

                        s = this.ShowNextPlay(null);
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                                s = s.Replace(nl, "<br>").Replace(" * ", "<b>").Replace(" *", "</b>") + "<br>" + nl;
                        }

                        if (!done)
                        {
                                roll = g.LastRoll();
                                foreach (int die in roll)
                                {
                                        dice += "<img src=\"/Images/d" + die.ToString() + "pip.png\">" + nl;
                                }

                                message = dice + "<br>" + s + "<img src=\"/Images/Diamond" + g.Diamond() + ".png\"><br><br>" + nl;
                                html += message;
                        }

                        session["Html"] = html;
                        return message;
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