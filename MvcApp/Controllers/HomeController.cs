using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    /// <summary>
    /// The home controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        #region Public methods
        /// <summary>
        /// Renders the homepage
        /// </summary>
        /// <returns>The page</returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

    }
}
