using BLL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    /// <summary>
    /// The folder controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class FolderController : Controller
    {
        #region Fields

        private IFolderService _folderService;
        private IUserService _userService;        
        #endregion

        #region Contructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="FolderController"/> class.
        /// </summary>
        /// <param name="folderService">The folder service.</param>
        /// <param name="userService">The user service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// folderService
        /// or
        /// userService
        /// </exception>
        public FolderController(IFolderService folderService, IUserService userService)
        {
            if (folderService == null) throw new ArgumentNullException("folderService");
            _folderService = folderService;

            if (userService == null) throw new ArgumentNullException("userService");
            _userService = userService;
        }        
        #endregion

        #region Public methods        
        /// <summary>
        /// Renders the page with adding new folder
        /// </summary>
        /// <param name="rootFolderId">The root folder identifier.</param>
        /// <returns>The page</returns>
        [HttpGet]
        public ActionResult AddNewFolder(int? rootFolderId)
        {
            ViewBag.RootFolderId = rootFolderId;
            return View();
        }

        /// <summary>
        /// Adds the new folder.
        /// </summary>
        /// <param name="rootFolderId">The root folder identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNewFolder(int? rootFolderId, string name)
        {
            CreateFolder(rootFolderId, name);
            return RedirectToAction("UserFiles", "Drive");
        }

        /// <summary>
        /// Gets the root folder identifier.
        /// </summary>
        /// <param name="currentFolderId">The current folder identifier.</param>
        /// <returns>Identifier of root to this folder, null if this folder is root</returns>
        [HttpGet]
        public int? GetRootFolderId(int? currentFolderId)
        {
            if (currentFolderId == null) return null;
            return _folderService.GetRootFolderId(currentFolderId.Value);
        }

        /// <summary>
        /// Creates a new folder.
        /// </summary>
        /// <param name="rootFolderId">The root folder identifier.</param>
        /// <param name="name">The name.</param>
        public void CreateFolder(int? rootFolderId, string name)
        {
            _folderService.CreateFolder(rootFolderId, name, User.Identity.Name);
        }

        [HttpPost]
        public void DeleteFolder(int? folderId) 
        {
            if (folderId == null) throw new ArgumentNullException("folderId");
            if (!_userService.UserHaveFolder(User.Identity.Name, folderId.Value)) return;
            _folderService.DeleteFolder(folderId.Value);
        }
        #endregion
    }
}
