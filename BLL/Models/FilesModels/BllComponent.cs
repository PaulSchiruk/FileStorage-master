using BLL.Interfaces.Models.FilesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.FilesModels
{
    public class BllComponent : IBllComponent
    {
        private int _size;
        private string _name = String.Empty;
        private string _contentType = String.Empty;

        public int Id { get; set; }

        public DateTime DateUploaded { get; set; }

        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (value < 0) throw new InvalidOperationException("Content size can't be less than 0");
                _size = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null) _name = value;
            }
        }

        public virtual string ContentType
        {
            get { return _contentType; }
            set { if (value != null) _contentType = value; }
        }

    }
}
