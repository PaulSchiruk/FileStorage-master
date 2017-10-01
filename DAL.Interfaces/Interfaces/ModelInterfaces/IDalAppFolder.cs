using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Interfaces.ModelInterfaces
{
    public interface IDalAppFolder : IEntity
    {
        string Name { get; set; }

        DateTime DateUploaded { get; set; }

        IEnumerable<int> FilesIds { get; set; }

        IEnumerable<int> InternalFoldersIds { get; set; }

        int? RootFolderId { get; set; }

        int UserId { get; set; }
    }
}
