using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Models
{
    public class AppFile
    {
        public AppFile()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public DateTime DateUploaded { get; set; }

        public byte[] Content { get; set; }

        public int Size { get; set; }

        public User User { get; set; }

        public AppFolder Folder { get; set; }
    }
}
