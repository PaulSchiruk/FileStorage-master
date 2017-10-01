using DAL.Models;
using ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class DalMappers
    {
        public static DalUser ToDalUser(this User user)
        {
            if (user == null) return null;

            DalUser dalUser = (DalUser)DalMappersExpressions.ToDalUserExpression().Compile()(user);
            
            return dalUser;
        }

        public static DalAppFile ToDalAppFile(this AppFile file)
        {
            if (file == null) return null;
            return (DalAppFile)DalMappersExpressions.ToDalAppFileExpression().Compile()(file);
        }

        public static DalAppFolder ToDalAppFolder(this AppFolder folder)
        {
            if (folder == null) return null;
            return (DalAppFolder)DalMappersExpressions.ToDalAppFolderExpression().Compile()(folder);
        }

    }
}
