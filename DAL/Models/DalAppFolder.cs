using DAL.Interfaces.Interfaces.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DalAppFolder : IDalAppFolder
    {
        public DalAppFolder()
        {
            FilesIds = new List<int>();
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

        public DateTime DateUploaded
        {
            get;
            set;
        }

        public IEnumerable<int> FilesIds
        {
            get;
            set;
        }

        public int UserId
        {
            get;
            set;
        }

        public int? RootFolderId { get; set; }

        public IEnumerable<int> InternalFoldersIds { get; set; }
    }
}
