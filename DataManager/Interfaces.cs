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
            Handlers.Repositories.Members Members { get; }
            Handlers.Repositories.Coaches Coaches { get; }
            Handlers.Repositories.Sessions Sessions { get; }
            Handlers.Repositories.Gears Gears { get; }
            Handlers.Repositories.GearLoans GearLoans { get; }


            int Complete();
        }
        public interface IRepository<T> where T : class // not having global definitions is good since we can now have specific validations, etc for each entity in each respective repo
        {
            T GetById(int id);
            IEnumerable<T> GetAll();
            void Add(T entity);
            void Update(T entity, Action<T> changes);
            void Delete(T entity);
        }
    }
}
