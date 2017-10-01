using BLL.Interfaces.Services;
using MvcApp.Models.FilesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcApp.Infrastructure.Mappers;
using System.IO;
using MvcApp.Infrastructure.Formatters;
using System.Web.Configuration;
using NLog;

namespace MvcApp.Controllers
{
    /// <summary>
    /// The drive controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [Authorize]
    public class DriveController : Controller
    {
        #region Fields

        private IFileService _fileService;
        private IUserService _userService;
        private IComponentFacadeService _componentService;
        #endregion

        #region Constructors		
        /// <summary>
        /// Initializes a new instance of the <see cref="DriveController"/> class.
        /// </summary>
        /// <param name="fileService">The file service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="componentService">The component service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// fileService
        /// or
        /// userService
        /// or
        /// componentService
        /// </exception>
        public DriveController(IFileService fileService, IUserService userService, IComponentFacadeService componentService)
        {
            if (fileService == null) throw new ArgumentNullException("fileService");
            _fileService = fileService;

            if (userService == null) throw new ArgumentNullException("userService");
            _userService = userService;

            if (componentService == null) throw new ArgumentNullException("componentService");
            _componentService = componentService;

        }
	    #endregion      

        #region Public methods                
        /// <summary>
        /// Returns user files.
        /// </summary>
        /// <param name="rootFolderId">The root folder identifier.</param>
        /// <returns>Page with user files</returns>
        public ActionResult UserFiles(int? rootFolderId = null)
        {
            var files = _componentService.GetComponentsOfUser(User.Identity.Name, rootFolderId).Select(f => f.ToComponentViewModel());
            ViewBag.NumberOfPages = _componentService.GetCountOfComponentsPages(10, rootFolderId, User.Identity.Name);
            ViewBag.RootFolderId = rootFolderId;
            return View(files);
        }

        /// <summary>
        /// Gets the page with files.
        /// </summary>
        /// <param name="rootFolderId">The root folder identifier.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>Page with user files</returns>
        [HttpGet]
        public ActionResult GetPage(int? rootFolderId, int pageNumber)
        {
            var files = _componentService.GetComponentsOfUser(User.Identity.Name, rootFolderId, pageNumber).Select(f => f.ToComponentViewModel());
            if (files.Count() == 0) return Content("There is no files here");
            return PartialView("_PageWithFiles", files);
        }

        /// <summary>
        /// Gets the pagination.
        /// </summary>
        /// <param name="rootFolderId">The root folder identifier.</param>
        /// <returns>Page with pagination</returns>
        [HttpGet]
        public ActionResult GetPagination(int? rootFolderId)
        {
            return PartialView("_Pagination", _componentService.GetCountOfComponentsPages(10, rootFolderId, User.Identity.Name));
        }

        /// <summary>
        /// Renders page with file uploading
        /// </summary>
        /// <returns>The page</returns>
        public ActionResult UploadNewFile()
        {
            ViewBag.MaxFileSize = ToMegabytes(WebConfigurationManager.AppSettings["maxFilesSize"]);
            return View();
        }

        /// <summary>
        /// Uploads the files.
        /// </summary>
        /// <returns>Json result whether file uploadimg was successfull</returns>
        /// <exception cref="System.ApplicationException">File to upload is too big</exception>
        public ActionResult UploadFiles()
        {
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];

                    if (int.Parse(WebConfigurationManager.AppSettings["maxFilesSize"]) < file.ContentLength)
                        throw new ApplicationException("File to upload is too big");
                    var bllFile = file.ToBllFile();
                    bllFile.Content = ReadFully(file.InputStream);
                    bllFile.Size = file.ContentLength;
                    _fileService.UploadFile(bllFile, User.Identity.Name);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                System.Web.Mvc.DependencyResolver.Current.GetService<ILogger>().Error(String.Format("Error occcured while uploading file by user {0}", User.Identity.Name));
                System.Web.Mvc.DependencyResolver.Current.GetService<ILogger>().Error(ex);
                return Content("Error saving file");
            }

            return Json(new { Message = fName });
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <returns>File for downloading</returns>
        /// <exception cref="System.ArgumentNullException">fileId</exception>
        /// <exception cref="System.ApplicationException">Illegal file downloading attempted</exception>
        public ActionResult DownloadFile(int? fileId)
        {
            if (fileId == null) throw new ArgumentNullException("fileId");
            int id = fileId.Value;

            if (!_userService.UserHaveFile(User.Identity.Name, id))
                throw new ApplicationException("Illegal file downloading attempted");
            var file = _fileService.GetFileWithContent(id);

            string filename = file.Name;
            byte[] filedata = file.Content;

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());
            var b = cd.ToString();
            return File(filedata, file.ContentType);

        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        [HttpPost]
        public void DeleteFile(int fileId)
        {
            if (!_userService.UserHaveFile(User.Identity.Name, fileId)) return;
            _fileService.DeleteFile(fileId);
        }

        /// <summary>
        /// Moves the file.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="destinationFolderId">The destination folder identifier.</param>
        [HttpPost]
        public void MoveFile(int fileId, int? destinationFolderId)
        {
            if (!_userService.UserHaveFile(User.Identity.Name, fileId)) return;
            if (destinationFolderId != null && !_userService.UserHaveFolder(User.Identity.Name, destinationFolderId.Value)) return;
            _fileService.MoveFile(fileId, destinationFolderId);
        }

        /// <summary>
        /// Renders page with 'Not realized' content
        /// </summary>
        /// <returns>The page</returns>
        public ActionResult NotRealized()
        {
            return View("NotRealized");
        }
        
        #endregion

        #region Private methods

        //TODO: move to bll layer
        //TODO: big files support
        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            input.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        private int ToMegabytes(string bytes)
        {
            return int.Parse(bytes) / 1048576;
        }
        
        #endregion
    }
}
