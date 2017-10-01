using BLL.Interfaces.Models.FilesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IFileService
    {
        void UploadFile(IBllFile file, string userName);

        IQueryable<IBllFile> GetFilesOfUser(string userName, int? rootFolderId);

        IBllFile GetFileWithContent(int fileId);

        void DeleteFile(int fileId);

        int GetCountOfFilePages(int countFilesOnPage, string userName);

        void MoveFile(int fileId, int? destinationFolderId);

    }
}
