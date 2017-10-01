using BLL.Interfaces.Models.FilesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IFolderService
    {
        void CreateFolder(int? rootFolderId, string folderName, string userName);

        IQueryable<IBllFolder> GetFoldersOfUser(string userName, int? rootFolderId);

        int? GetRootFolderId(int folderId);

        void DeleteFolder(int folderId);
    }
}
