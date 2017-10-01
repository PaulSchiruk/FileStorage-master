using DAL.Interfaces.Interfaces.ModelInterfaces;
using DAL.Models;
using ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class DalMappersExpressions
    {
        public static Expression<Func<User, IDalUser>> ToDalUserExpression()
        {
            return user => new DalUser()
            {
                Id = user.Id,
                Name = user.Name,
                PasswordHash = user.PasswordHash,
                FilesIds = user.Files.Select(f => f.Id),
                FolderIds = user.Folders.Select(f => f.Id)
                //  RoleId = user.Role.Id
            };
        }

        public static Expression<Func<AppFile, IDalAppFile>> ToDalAppFileExpression()
        {
            return file => new DalAppFile()
            {
                Id = file.Id,
                Content = file.Content,
                ContentType = file.ContentType,
                Size = file.Size,
                DateUploaded = file.DateUploaded,
                FolderId = (file.Folder == null) ? (int?)null : file.Folder.Id,
                Name = file.Name,
                UserId = file.User.Id
            };
        }

        public static Expression<Func<AppFolder, IDalAppFolder>> ToDalAppFolderExpression()
        {
            return folder => new DalAppFolder()
            {
               Id = folder.Id,
               DateUploaded = folder.DateUploaded,
               Name = folder.Name,
               FilesIds = folder.Files.Select(f => f.Id),
               UserId = folder.User.Id,
               RootFolderId = (folder.RootFolder != null ? folder.RootFolder.Id : (int?)null),
               InternalFoldersIds = folder.InternalFolders.Select(f => f.Id)
            };
        }
    }
}
