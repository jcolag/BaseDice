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
        using System.Text.RegularExpressions;
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
                /// Options for the weather.
                /// </summary>
                private static string[] weather =
                {
                        "rain",
                        "graupel",
                        "hail",
                        "sleet",
                        "freezing rain",
                        "snow",
                        "smog",
                        "rain and snow mix",
                        "lightning",
                        "high winds",
                        "squall line",
                        "blizzard",
                        "downburst",
                        "dust storm",
                        "extratropical cyclone",
                        "ice storm",
                        "firestorm",
                        "tornado",
                        "tropical cyclone",
                        "waterspout",
                        "non-convective flooding",
                        "dark of night",
                        "rain of toads"
                };

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
                /// Handle start button from front page.
                /// </summary>
                /// <returns>The view.</returns>
                /// <param name="file">Uploaded player file.</param>
                [HttpPost]
                public ActionResult Index(HttpPostedFileBase file)
                {
                        HttpSessionStateBase session = this.Session;

                        if (session == null)
                        {
                                throw new ObjectDisposedException(GetType().Name);
                        }

                        if (file != null && file.ContentLength > 0 && file.ContentType == "text/xml")
                        {
                                var document = new System.Xml.XmlDocument();
                                document.Load(file.InputStream);
                                var pfile = new PlayerFile(document);
                                session["Player"] = pfile;
                                if (session["Game"] != null)
                                {
                                        Game g = (Game)session["Game"];
                                        g.Name = pfile.PlayerName;
                                }
                        }

                        return this.RedirectToAction("Next");
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
                        Game game;
                        int runs;

                        if (session == null)
                        {
                                throw new ObjectDisposedException(GetType().Name);
                        }

                        game = (Game)session["Game"];
                        string message = this.ProcessTurn();
                        string html = (string)session["Html"];

                        viewdata["History"] = html;
                        message += HomeController.BreakPadding(message, 8);
                        viewdata["Message"] = message;
                        viewdata["Player_Name"] = game.Name;
                        for (int i = 0; i <= 9; i++)
                        {
                                runs = game.InningScore(i);
                                viewdata["Inning_" + i.ToString()] = runs > 0 ? runs.ToString() : "&mdash;";
                        }

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

                        message += HomeController.BreakPadding(message, 8);
                        Response.Write(message);
                        return null;
                }

                /// <summary>
                /// Controller for the score partial page.
                /// </summary>
                /// <returns>The next view.</returns>
                [AcceptVerbs(HttpVerbs.Get)]
                public ActionResult Score()
                {
                        HttpSessionStateBase session = this.Session;
                        Game game;
                        string resp;
                        string[] pathParts = Request.Path.Split("/".ToArray());
                        int which = 0;

                        if (session == null)
                        {
                                throw new ObjectDisposedException(GetType().Name);
                        }

                        game = (Game)session["Game"];
                        if (pathParts.Length <= 2)
                        {
                                which = game.WhatInning();
                        }
                        else
                        {
                                try
                                {
                                        int.TryParse(pathParts[3], out which);
                                }
                                catch (ArgumentException)
                                {
                                }
                        }

                        resp = which.ToString() + "," + game.InningScore(which).ToString();
                        Response.Write(resp);
                        return null;
                }

                /// <summary>
                /// Returns bonuses for the player (stub).
                /// </summary>
                /// <returns>The bonus partial.</returns>
                [AcceptVerbs(HttpVerbs.Get)]
                public ActionResult Bonuses()
                {
                        string list = "-";

                        PlayerFile player = (PlayerFile)this.Session["Player"];

                        if (player != null)
                        {
                                foreach (PlayerBoost b in player.Bonuses)
                                {
                                        list += ";" + b.ToString();
                                }
                        }

                        list = Regex.Replace(list, "(\\B[A-Z])", " $1");
                        this.Response.Write(list);
                        return null;
                }

                /// <summary>
                /// Get the sound for this past at-bat.
                /// </summary>
                /// <returns>The sound partial.</returns>
                [AcceptVerbs(HttpVerbs.Get)]
                public ActionResult Sound()
                {
                        var g = (Game)this.Session["Game"];
                        this.Response.Write(g.Sound);
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
                        bool done = false;

                        if (g == null)
                        {
                                string weather = (string)session["Weather"];
                                if (string.IsNullOrWhiteSpace(weather))
                                {
                                        double max = Math.Pow(2, HomeController.weather.Length);
                                        var rand = new Random();
                                        int value = rand.Next(Convert.ToInt32(max));
                                        int index = 0;
                                        while (value > 1)
                                        {
                                                ++index;
                                                value /= 2;
                                        }

                                        index = HomeController.weather.Length - index - 1;
                                        weather = HomeController.weather[index];
                                        session.Add("Weather", weather);
                                }

                                return "Game called on account of " + weather + "!";
                        }

                        try
                        {
                                done = (bool)session["Done"];
                        }
                        catch (InvalidCastException)
                        {
                        }
                        catch (ArgumentNullException)
                        {
                        }
                        catch (NullReferenceException)
                        {
                        }

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

                        if (g == null)
                        {
                                return string.Empty;
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
                                var player = (PlayerFile)session["Player"];
                                string param = (string)Request.Params["bonus"];
                                PlayerBoost bonus = PlayerBoost.Nothing;
                                if (!string.IsNullOrWhiteSpace(param))
                                {
                                        bonus = (PlayerBoost)Enum.Parse(typeof(PlayerBoost), param);
                                }

                                s = g.TakeTurn(bonus);
                                if (player != null)
                                {
                                        player.Remove(bonus);
                                }
                        }

                        return s;
                }
        }
}