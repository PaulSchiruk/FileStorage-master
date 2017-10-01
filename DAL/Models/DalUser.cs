using DAL.Interfaces.Interfaces.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DalUser : IDalUser
    {
        public DalUser()
        {
            FilesIds = new HashSet<int>();
            FolderIds = new HashSet<int>();
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string PasswordHash
        {
            get;
            set;
        }

        public int RoleId
        {
            get;
            set;
        }

        public IEnumerable<int> FilesIds
        {
            get;
            set;
        }

        public IEnumerable<int> FolderIds { get; set; }
    }
}
