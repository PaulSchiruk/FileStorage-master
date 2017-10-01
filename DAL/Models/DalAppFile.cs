using DAL.Interfaces.Interfaces.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DalAppFile : IDalAppFile
    {
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

        public byte[] Content
        {
            get;
            set;
        }

        public int Size { get; set; }

        public int UserId
        {
            get;
            set;
        }

        public int? FolderId
        {
            get;
            set;
        }


        public string ContentType
        {
            get;
            set;
        }

    }
}
