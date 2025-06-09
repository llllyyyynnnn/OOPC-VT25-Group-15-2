using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class Interfaces
    {
        public interface IUnitOfWork : IDisposable
        {

        }
        public interface IRepository<T> where T : class
        {
            T GetById(int id);
            IEnumerable<T> GetAll();
            IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
            void Add(T entity);
            void Update(T entity);
            void Delete(T entity);
        }
    }
}
