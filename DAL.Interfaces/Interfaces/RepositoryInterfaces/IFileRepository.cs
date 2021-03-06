﻿using DAL.Interfaces.Interfaces.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Interfaces.RepositoryInterfaces
{
    public interface IFileRepository : IRepository<IDalAppFile>
    {
        void UpdateFileFolder(IDalAppFile file, IDalAppFolder folder);
    }
}
