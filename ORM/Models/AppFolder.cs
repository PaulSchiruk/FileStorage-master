using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Models
{
    public class AppFolder
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateUploaded { get; set; }

        public ICollection<AppFile> Files { get; private set; }

        public User User { get; set; }

        public AppFolder RootFolder { get; set; }

        public ICollection<AppFolder> InternalFolders { get; set; }
    }
}
