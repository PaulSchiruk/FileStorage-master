using BLL.Interfaces.Models.FilesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Services
{
    public interface IComponentFacadeService
    {
        IEnumerable<IBllComponent> GetComponentsOfUser(string userName, int? rootFolderId, int pageNumber = 0, int countOnPage = 10);

        int GetCountOfComponentsPages(int countComponentsOnPage, int? rootFolderId, string userName);
    }
}
