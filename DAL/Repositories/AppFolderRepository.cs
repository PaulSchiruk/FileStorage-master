using DAL.Interfaces.Interfaces.ModelInterfaces;
using DAL.Interfaces.Interfaces.RepositoryInterfaces;
using ORM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Mappers;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class AppFolderRepository : IFolderRepository
    {
        private readonly DbContext _context;

        public AppFolderRepository(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            this._context = context;
        }

        public IQueryable<IDalAppFolder> GetAll()
        {
            return _context.Set<AppFolder>().Include("Files").Include("User").Select(DalMappersExpressions.ToDalAppFolderExpression());
        }

        public IDalAppFolder GetById(int id)
        {
            var folder = _context.Set<AppFolder>()
                .Include("Files")
                .Include("User")
                .Include("RootFolder")
                .Include("InternalFolders")
                .Where(f => f.Id == id).FirstOrDefault();
            if (folder == null) return null;
            return folder.ToDalAppFolder();
        }

        public IQueryable<IDalAppFolder> GetByPredicate(Expression<Func<IDalAppFolder, bool>> f)
        {
            return _context.Set<AppFolder>().Select(DalMappersExpressions.ToDalAppFolderExpression()).Where(f);
        }

        public void Add(IDalAppFolder entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var folder = new AppFolder()
            {
                Id = entity.Id,
                DateUploaded = entity.DateUploaded,
                Name = entity.Name,
                
            };
            folder.User = _context.Set<User>().Find(entity.UserId);

            if (entity.RootFolderId != null)
                folder.RootFolder = _context.Set<AppFolder>().Find(entity.RootFolderId);

            _context.Set<AppFolder>().Add(folder);
        }

        public void Delete(IDalAppFolder entity)
        {
            if (entity != null)
            {
                var folder = _context.Set<AppFolder>().Find(entity.Id);

                if (folder != null)
                    _context.Set<AppFolder>().Remove(folder);
            }
        }
    }
}
