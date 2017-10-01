using BLL.Interfaces.Models.FilesModels;
using BLL.Interfaces.Services;
using BLL.Models.FilesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ComponentFacadeService : IComponentFacadeService
    {
        private IFolderService _folderService;
        private IFileService _fileService;
        private IUserService _userService;

        public ComponentFacadeService(IFolderService folderSevice, IFileService fileService, IUserService userService)
        {
            if (folderSevice == null) throw new ArgumentNullException("folderSevice");
            this._folderService = folderSevice;

            if (fileService == null) throw new ArgumentNullException("fileService");
            this._fileService = fileService;

            if (userService == null) throw new ArgumentNullException("userService");
            this._userService = userService;
        }

        public IEnumerable<IBllComponent> GetComponentsOfUser(string userName, int? rootFolderId, int pageNumber = 0, int countOnPage = 10)
        {
            return _fileService.GetFilesOfUser(userName, rootFolderId)
                .Select(x => new BllComponent() { Id = x.Id, Name = x.Name, DateUploaded = x.DateUploaded, ContentType = x.ContentType, Size = x.Size})
                .Union(_folderService.GetFoldersOfUser(userName, rootFolderId).Select(x => new BllComponent() { Id = x.Id, Name = x.Name, DateUploaded = x.DateUploaded, ContentType = "folder", Size = 0 }))
                .OrderBy(f => f.Name).Skip(pageNumber * countOnPage).Take(countOnPage).ToList();
            
        }

        public int GetCountOfComponentsPages(int countComponentsOnPage, int? rootFolderId, string userName)
        {
            if (countComponentsOnPage <= 0) throw new ArgumentException("Can't be 0 or less cmponents on the page");

            var countOfAllFiles = _fileService.GetFilesOfUser(userName, rootFolderId).Count();
            var countOfAllFolders = _folderService.GetFoldersOfUser(userName, rootFolderId).Count();

            var resultCount = countOfAllFiles + countOfAllFolders;

            if (resultCount % countComponentsOnPage == 0) return resultCount / countComponentsOnPage;
            else return resultCount / countComponentsOnPage + 1;
        }
    }
}
