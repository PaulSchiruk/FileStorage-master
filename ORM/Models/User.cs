using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Models
{
    public partial class User
    {
        public User()
        {
            Files = new List<AppFile>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }
        
       // public Role Role { get; set; }

        public ICollection<AppFolder> Folders { get; private set; }

        public ICollection<AppFile> Files { get; private set; }
    }
}
