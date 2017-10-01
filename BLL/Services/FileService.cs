using BLL.Interfaces.Models.FilesModels;
using BLL.Interfaces.Services;
using DAL.Interfaces.Interfaces.ModelInterfaces;
using DAL.Interfaces.Interfaces.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Mappers;
using DAL.Interfaces.Interfaces;

namespace BLL.Services
{
    public class FileService : IFileService
    {
        private IFileRepository _fileRepository;
        private IFolderRepository _folderRepository;
        private IUserService _userService;
        private IUnitOfWork _unitOfWork;
        
        public FileService(IFileRepository fileRepository, IFolderRepository folderRepository, IUserService userService, IUnitOfWork unitOfWork)
        {
            if (fileRepository == null) throw new ArgumentNullException("fileRepository");
            this._fileRepository = fileRepository;

            if (folderRepository == null) throw new ArgumentNullException("folderRepository");
            this._folderRepository = folderRepository;

            if (userService == null) throw new ArgumentNullException("userService");
            this._userService = userService;

            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            this._unitOfWork = unitOfWork;
        }

        public void UploadFile(IBllFile file, string userName)
        {
            file.DateUploaded = DateTime.Now;
            IDalAppFile dalFile = file.ToDalAppFile();
            dalFile.UserId = _userService.GetUserByName(userName).Id;
            _fileRepository.Add(dalFile);
            _unitOfWork.Commit();
        }

        public IQueryable<IBllFile> GetFilesOfUser(string userName, int? rootFolderId)
        {
            var userId = _userService.GetUserByName(userName).Id;
            return _fileRepository.GetByPredicate(f => f.UserId == userId && f.FolderId == rootFolderId).Select(BllMappers.ToBllFileExpression());
        }

        public IBllFile GetFileWithContent(int fileId)
        {
            var dalFile = _fileRepository.GetById(fileId);
            var bllFile = dalFile.ToBllFile();
            bllFile.Content = dalFile.Content;
            return bllFile;
        }


        public void DeleteFile(int fileId)
        {
            _fileRepository.Delete(
                _fileRepository.GetById(fileId)
                );
            _unitOfWork.Commit();
        }

        public void MoveFile(int fileId, int? destinationFolderId)
        {
            var file = _fileRepository.GetById(fileId);
            _fileRepository.UpdateFileFolder(file, destinationFolderId == null ? null : _folderRepository.GetById(destinationFolderId.Value));
            _unitOfWork.Commit();
        }

        public int GetCountOfFilePages(int countFilesOnPage, string userName)
        {
            if (countFilesOnPage <= 0) throw new ArgumentException("Can't be 0 or less files on the page");
            var userId = _userService.GetUserByName(userName).Id;
            var countOfAllFiles = _fileRepository.GetByPredicate(f => f.UserId == userId).Count();

            if (countOfAllFiles % countFilesOnPage == 0) return countOfAllFiles / countFilesOnPage;
            else return countOfAllFiles / countFilesOnPage + 1;
        }

    }
}
