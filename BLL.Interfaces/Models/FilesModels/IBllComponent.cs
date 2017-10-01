using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Models.FilesModels
{
    public interface IBllComponent
    {
        int Id { get; set; }

        int Size { get; set; }

        string Name { get; set; }

        string ContentType { get; set; }

        DateTime DateUploaded { get; set; }

    }
}
