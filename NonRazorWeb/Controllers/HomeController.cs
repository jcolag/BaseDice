using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using BaseDice;

namespace NonRazorWeb.Controllers
{
        public class HomeController : Controller
        {
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

                        ViewData["Message"] = html;
                        return View();
                }
        }
}

