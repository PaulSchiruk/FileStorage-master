using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        IQueryable<T> GetByPredicate(Expression<Func<T, bool>> f);
        void Add(T entity);
        void Delete(T entity);
    }
}
