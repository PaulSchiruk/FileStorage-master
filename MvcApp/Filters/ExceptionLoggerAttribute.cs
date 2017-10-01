using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Filters
{
    /// <summary>
    /// Filter that logs exceptions
    /// </summary>
    /// <seealso cref="System.Web.Mvc.FilterAttribute" />
    /// <seealso cref="System.Web.Mvc.IExceptionFilter" />
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
        #region Fields

        private ILogger _logger;
        #endregion

        #region Properties

        private ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = System.Web.Mvc.DependencyResolver.Current.GetService<ILogger>();
                }
                return _logger;
            }
        }
        #endregion

        #region Public methods        
        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {
            Logger.Error(filterContext.Exception);
        } 
        #endregion
    }
}