using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Interfaces.ModelInterfaces
{
    public interface IDalUser : IEntity
    {
        int Id { get; set; }

        string Name { get; set; }

        string PasswordHash
        {
            get;
            set;
        }

        int RoleId
        {
            get;
            set;
        }

        IEnumerable<int> FilesIds { get; set; }

        IEnumerable<int> FolderIds { get; set; }
    }
}
