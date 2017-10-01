using DAL.Interfaces.Interfaces.RepositoryInterfaces;
using DAL.Interfaces.Interfaces.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ORM.Models;
using DAL.Mappers;
using System.Linq.Expressions;
using DAL.Models;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            this._context = context;
        }

        public IQueryable<IDalUser> GetAll()
        {
            return _context.Set<User>().Include("Role").Include("Files").Include("Folders").Select(DalMappersExpressions.ToDalUserExpression());
        }

        public IDalUser GetById(int id)
        {
            var user = _context.Set<User>().Include("Role").Include("Files").Include("Folders").Where(u => u.Id == id).FirstOrDefault();
            if (user == null) return null;
            return user.ToDalUser();
        }

        public IQueryable<IDalUser> GetByPredicate(Expression<Func<IDalUser, bool>> f)
        {
            return _context.Set<User>().Include("Role").Include("Files").Include("Folders").Select(DalMappersExpressions.ToDalUserExpression()).Where(f);
        }

        public void Add(IDalUser entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var user = new User()
            {
                Id = entity.Id,
                Name = entity.Name,
                PasswordHash = entity.PasswordHash
            };

        //    user.Role = _context.Set<Role>().Find(entity.RoleId);
            foreach (var fileId in entity.FilesIds)
            {
                user.Files.Add(_context.Set<AppFile>().Find(fileId));
            }
            _context.Set<User>().Add(user);
        }

        public void Delete(IDalUser entity)
        {
            if (entity != null)
            {
                var user = _context.Set<User>().Find(entity.Id);

                if (user != null)
                    _context.Set<User>().Remove(user);
            }
        }
    }
}
