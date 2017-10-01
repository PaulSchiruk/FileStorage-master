using BLL.Interfaces.Models;
using BLL.Interfaces.Models.FilesModels;
using BLL.Models;
using BLL.Models.FilesModels;
using DAL.Interfaces.Interfaces.ModelInterfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class BllMappers
    {
        public static IDalAppFile ToDalAppFile(this IBllFile file)
        {
            return new DalAppFile()
            {
                Id = file.Id,
                Content = file.Content,
                ContentType = file.ContentType,
                DateUploaded = file.DateUploaded,
                Name = file.Name,
                Size = file.Size
            };
        }

        public static IBllFile ToBllFile(this IDalAppFile file)
        {
            return ToBllFileExpression().Compile()(file);
        }

        public static IDalUser ToDalUser(this BllUser user)
        {
            return new DalUser()
            {
                Id = user.Id,
                Name = user.Name,
                PasswordHash = user.PasswordHash
            };
        }

        public static IBllUser ToBllUser(this IDalUser user)
        {
            return ToBllUserExpression().Compile()(user);
        }

        public static IBllFolder ToBllFolder(this IDalAppFolder folder)
        {
            return ToBllFolderExpression().Compile()(folder);
        }

        public static Expression<Func<IDalAppFile, IBllFile>> ToBllFileExpression()
        {
            return file => new BllFile()
            {
                Id = file.Id,
                ContentType = file.ContentType,
                DateUploaded = file.DateUploaded,
                Name = file.Name,
                Size = file.Size
            };
        }

        public static Expression<Func<IDalUser, IBllUser>> ToBllUserExpression()
        {
            return user => new BllUser()
            {
                Id = user.Id,
                Name = user.Name,
                PasswordHash = user.PasswordHash
            };
        }

        public static Expression<Func<IDalAppFolder, IBllFolder>> ToBllFolderExpression()
        {
            return folder => new BllFolder()
            {
                Id = folder.Id,
                DateUploaded = folder.DateUploaded,
                Name = folder.Name
            };
        }
    }
}
