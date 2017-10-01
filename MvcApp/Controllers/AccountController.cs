using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MvcApp.Models;
using BLL.Interfaces.Services;

namespace MvcApp.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class AccountController : Controller
    {
        #region Fields

        private IUserService _userService;
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <exception cref="System.ArgumentNullException">userService</exception>
        public AccountController(IUserService userService)
        {
            if (userService == null) throw new ArgumentNullException("userService");
            _userService = userService;
        } 
        #endregion

        #region Public methods        
        /// <summary>
        /// Checks the user not exist.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public JsonResult CheckUserNotExist(string userName)
        {
            if (UserExist(userName))
                return Json("User with this email already register", JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Renders the login page.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>The register page</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Logins the specified user model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The URL to return to.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        /// <summary>
        /// Log off the current user.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Renders the register page
        /// </summary>
        /// <returns>The register page</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Registers the specified user model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Default page</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus status;
                MembershipUser membershipUser = Membership.Provider.CreateUser(model.UserName, model.Password, model.UserName, null, null, false, null, out status);
                if (status == MembershipCreateStatus.Success && membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("UserFiles", "Drive");
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while registering your account. Please, try again later.");
                }
            }
            return View(model);
        }
        #endregion


        #region Private methods

        private bool UserExist(string email)
        {
            return _userService.UserExist(email);
        }
        
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("UserFiles", "Drive");
            }
        }

        #endregion
    }
}
