using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Interfaces.ModelInterfaces
{
    public interface IDalAppFile : IEntity
    {
        string Name { get; set; }

        string ContentType { get; set; }

        DateTime DateUploaded { get; set; }
         
        byte[] Content { get; set; }

        int Size { get; set; }

        int UserId { get; set; }

        int? FolderId { get; set; }
    }
}
