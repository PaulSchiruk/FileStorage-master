using BLL.Interfaces.Models.FilesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.FilesModels
{
    public class BllFolder : BllComponent, IBllFolder
    {
        private static readonly string folderContentTypeName = "folder";

        public override string ContentType
        {
            get
            {
                return folderContentTypeName;
            }
            set
            {
                throw new InvalidOperationException("Attempt to change content type of folder");
            }
        } 

        public IEnumerable<IBllComponent> Children
        {
            get;
            set;
        }
    }
}
