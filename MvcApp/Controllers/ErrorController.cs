using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    /// <summary>
    /// The error controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ErrorController : Controller
    {
        #region Public methods
        /// <summary>
        /// Renders the "Not found" page
        /// </summary>
        /// <returns>The page</returns>
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        /// <summary>
        /// Renders the "Error" page
        /// </summary>
        /// <returns>The page</returns>
        public ActionResult Error()
        {
            Response.StatusCode = 500;
            return View();
        }
        
        #endregion
    }
}
