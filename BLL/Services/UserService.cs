using BLL.Interfaces.Models;
using BLL.Interfaces.Services;
using BLL.Models;
using DAL.Interfaces.Interfaces;
using DAL.Interfaces.Interfaces.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Mappers;
using DAL.Models;
using DAL.Interfaces.Interfaces.ModelInterfaces;
using DevOne.Security.Cryptography.BCrypt;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        #region Fields

        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;
        private int a;
        #endregion

        #region Constructors

        public UserService(IUserRepository userRepository, IUnitOfWork uow)
        {
            if (userRepository == null) throw new ArgumentNullException("userRepository");
            if (uow == null) throw new ArgumentNullException("uow");

            this._userRepository = userRepository;
            this._unitOfWork = uow;

            a = new Random().Next(100);
        }
        #endregion

        #region Public methods

        public void RegisterNewUser(string name, string password)
        {
            var user = GenerateNewUser(name, password);
            var dalUser = user.ToDalUser();
            _userRepository.Add(dalUser);
            _unitOfWork.Commit();
        }

        public bool ValidateUser(string name, string password)
        {
            var dalUser = GetDalUser(name);
            if (dalUser == null) return false;
            var bllUser = dalUser.ToBllUser();
            return BCryptHelper.CheckPassword(
                password,
                bllUser.PasswordHash
                );
        }

        public bool UserExist(string name)
        {
            return GetDalUser(name) != null;
        }

        public IBllUser GetUserByName(string name)
        {
            var user = _userRepository.GetByPredicate(u => u.Name == name).FirstOrDefault();
            if (user == null) return null;
            return user.ToBllUser();
        }

        public IBllUser GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return null;
            return user.ToBllUser();
        }

        public bool UserHaveFile(string userName, int fileId)
        {
            return GetDalUser(userName).FilesIds.Contains(fileId);
        }

        public bool UserHaveFolder(string userName, int folderId)
        {
            return GetDalUser(userName).FolderIds.Contains(folderId);
        }
        #endregion

        #region Private methods

        private BllUser GenerateNewUser(string name, string password)
        {
            var user = new BllUser();
            user.Name = name;
            user.PasswordHash = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt());
            return user;
        }

        private static string GeneratePasswordHash(string name, string password)
        {
            return string.Concat("FileStore", name, password).GetHashCode().ToString();
        }

        private IDalUser GetDalUser(string name)
        {
            return _userRepository.GetByPredicate(u => u.Name == name).FirstOrDefault();
        }
        #endregion

    }
}
