using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TaskFollowUp.Data
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate, string [] relations);

        T GetById(Expression<Func<T, bool>> predicate, string [] relations, int Id);

        void Save(List<T> entities);

        void Save(T entity);

        void Delete(T entity);

        void DeleteById(int Id);
    }

    public interface IRepository
    {
    }
}
