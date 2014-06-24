// <copyright file="HomeController.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace DiceWeb.Controllers
{
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Web;
        using System.Web.Mvc;
        using System.Web.Mvc.Ajax;

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
                        this.ViewData["Message"] = "Welcome to ASP.NET MVC on Mono!";
                        return this.View();
                }
        }
}