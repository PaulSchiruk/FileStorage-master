using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Models.FilesModels
{
    public interface IBllFile: IBllComponent
    {
        byte[] Content { get; set; }
    }
}
