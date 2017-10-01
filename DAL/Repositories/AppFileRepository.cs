using DAL.Interfaces.Interfaces.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.Interfaces.ModelInterfaces;
using System.Data.Entity;
using ORM.Models;
using DAL.Mappers;

namespace DAL.Repositories
{
    public class AppFileRepository : IFileRepository
    {
        private readonly DbContext _context;

        public AppFileRepository(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            this._context = context;
        }

        public IQueryable<IDalAppFile> GetAll()
        {
            return _context.Set<AppFile>().Include("User").Include("Folder").Select(DalMappersExpressions.ToDalAppFileExpression());
        }

        public IDalAppFile GetById(int id)
        {
            var file = _context.Set<AppFile>().Include("User").Include("Folder").Where(f => f.Id == id).FirstOrDefault();
            if (file == null) return null;
            return file.ToDalAppFile();
        }

        public IQueryable<IDalAppFile> GetByPredicate(System.Linq.Expressions.Expression<Func<IDalAppFile, bool>> f)
        {
            return _context.Set<AppFile>().Select(DalMappersExpressions.ToDalAppFileExpression()).Where(f);
        }

        public void Add(IDalAppFile entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var file = new AppFile()
            {
                Id = entity.Id,
                Content = entity.Content,
                DateUploaded = entity.DateUploaded,
                Name = entity.Name,
                ContentType = entity.ContentType,
                Size = entity.Size
            };

            file.User = _context.Set<User>().Find(entity.UserId);

            if (entity.FolderId != null)
                file.Folder = _context.Set<AppFolder>().Find(entity.FolderId);

            _context.Set<AppFile>().Add(file);
        }

        public void Delete(IDalAppFile entity)
        {
            if (entity != null)
            {
                var file = _context.Set<AppFile>().Find(entity.Id);

                if (file != null)
                    _context.Set<AppFile>().Remove(file);
            }
        }

        public void UpdateFileFolder(IDalAppFile file, IDalAppFolder folder)
        {
            var fileToUpdate = _context.Set<AppFile>().Find(file.Id);
            if (folder == null) fileToUpdate.Folder = null;
            else
            {
                fileToUpdate.Folder = _context.Set<AppFolder>().Find(folder.Id);
            }
        }

    }
}
