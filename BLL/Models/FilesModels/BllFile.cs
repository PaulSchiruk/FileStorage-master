using BLL.Interfaces.Models.FilesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.FilesModels
{
    public class BllFile: BllComponent, IBllFile
    {
        public byte[] Content
        {
            get;
            set;
        }
    }
}
