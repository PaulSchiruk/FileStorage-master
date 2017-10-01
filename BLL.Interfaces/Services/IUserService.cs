using BLL.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IUserService
    {
        void RegisterNewUser(string name, string password);
        bool ValidateUser(string name, string password);
        bool UserExist(string name);
        IBllUser GetUserByName(string name);
        IBllUser GetUserById(int id);

        bool UserHaveFile(string userName, int fileId);

        bool UserHaveFolder(string userName, int folderId);
    }
}
