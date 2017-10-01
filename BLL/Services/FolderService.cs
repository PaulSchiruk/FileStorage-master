using BLL.Interfaces.Models.FilesModels;
using BLL.Interfaces.Services;
using BLL.Models.FilesModels;
using DAL.Interfaces.Interfaces;
using DAL.Interfaces.Interfaces.ModelInterfaces;
using DAL.Interfaces.Interfaces.RepositoryInterfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Mappers;

namespace BLL.Services
{
    public class FolderService : IFolderService
    {
        private IFolderRepository _folderRepository;
        private IFileRepository _fileRepository;
        private IUserService _userService;
        private IUnitOfWork _unitOfWork;

        public FolderService(IFolderRepository folderRepository, IUserService userService, IUnitOfWork unitOfWork, IFileRepository fileRepository)
        {
            if (folderRepository == null) throw new ArgumentNullException("folderRepository");
            this._folderRepository = folderRepository;

            if (fileRepository == null) throw new ArgumentNullException("fileRepository");
            this._fileRepository = fileRepository;

            if (userService == null) throw new ArgumentNullException("userService");
            this._userService = userService;

            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            this._unitOfWork = unitOfWork;
        }

        public void CreateFolder(int? rootFolderId, string folderName, string userName)
        {
            var user = _userService.GetUserByName(userName);
            if (rootFolderId != null) VerifyUserHaveFolder(user.Id, rootFolderId);
            var dalFolder = new DalAppFolder()
            {
                Name = folderName,
                DateUploaded = DateTime.Now,
                RootFolderId = rootFolderId,
                UserId = user.Id
            };
            _folderRepository.Add(dalFolder);
            _unitOfWork.Commit();
        }

        public IQueryable<IBllFolder> GetFoldersOfUser(string userName, int? rootFolderId)
        {
            var userId = _userService.GetUserByName(userName).Id;
            return _folderRepository.GetByPredicate(f => f.UserId == userId && f.RootFolderId == rootFolderId).Select(BllMappers.ToBllFolderExpression());
        }

        public int? GetRootFolderId(int folderId)
        {
            var folder = _folderRepository.GetById(folderId);

            if(folder == null) return null;

            return folder.RootFolderId;
        }

        private void VerifyUserHaveFolder(int userId, int? rootFolderId)
        {
            IDalAppFolder folder = _folderRepository.GetById(rootFolderId.Value);
            if (folder.UserId != userId) throw new InvalidOperationException("Illegal folder adding attempted");
        }

        public void DeleteFolder(int folderId)
        {
            IDalAppFolder folder = _folderRepository.GetById(folderId);
            DeleteFoldersTree(folder);
            _unitOfWork.Commit();
        }

        private void DeleteFoldersTree(IDalAppFolder folder) 
        {            
            foreach (var fileId in folder.InternalFoldersIds.ToList())
            {
                _fileRepository.Delete(_fileRepository.GetById(fileId));
            }
            foreach (var internalFolderId in folder.InternalFoldersIds.ToList())
            {
                DeleteFoldersTree(_folderRepository.GetById(internalFolderId));
            }
            _folderRepository.Delete(folder);
        }
    }
}
