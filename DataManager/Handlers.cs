using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static DataManager.Entities;
using static DataManager.Interfaces;

namespace DataManager
{
    public class Handlers
    {
        public class UnitOfWork : Interfaces.IUnitOfWork
        {
            private readonly Context _ctx;
            public Repositories.Members Members { get; set; }
            public Repositories.Coaches Coaches { get; set; }
            public Repositories.Sessions Sessions { get; set; }
            public Repositories.Categories Categories { get; set; }
            public Repositories.Gears Gears { get; set; }
            public Repositories.GearLoans GearLoans { get; set; }

            public UnitOfWork(Context context)
            {
                _ctx = context;
                Members = new Repositories.Members(_ctx);
                Coaches = new Repositories.Coaches(_ctx);
                Sessions = new Repositories.Sessions(_ctx);
                Categories = new Repositories.Categories(_ctx);
                Gears = new Repositories.Gears(_ctx);
                GearLoans = new Repositories.GearLoans(_ctx);
            }

            public int Complete() => _ctx.SaveChanges();
            public void Dispose() => _ctx.Dispose();
        }

        public class Repositories
        {
            public class Members : Interfaces.IRepository<Entities.Member>
            {
                private readonly Context _ctx;
                public Members(Context context) => _ctx = context;

                public Member GetById(int id) => _ctx.Members.Find(id);

                public IEnumerable<Member> GetAll() => _ctx.Members.ToList();

                public void Add(Member entity) => _ctx.Members.Add(entity);

                public void Update(Member entity, Action<Member> changes)
                {
                    changes(entity);
                    _ctx.Members.Update(entity);
                }

                public void Delete(Member entity) => _ctx.Members.Remove(entity);
            }

            public class Coaches : Interfaces.IRepository<Entities.Coach>
            {
                private readonly Context _ctx;
                public Coaches(Context context) => _ctx = context;

                public Coach GetById(int id) => _ctx.Coaches.Find(id);

                public IEnumerable<Coach> GetAll() => _ctx.Coaches.ToList();

                public void Add(Coach entity) => _ctx.Coaches.Add(entity);

                public void Update(Coach entity, Action<Coach> changes)
                {
                    changes(entity);
                    _ctx.Coaches.Update(entity);
                }
                public Coach Login(string email, string password) => _ctx.Coaches.FirstOrDefault(coach => coach.mailAddress == email && coach.pinCode == password);

                public void Delete(Coach entity) => _ctx.Coaches.Remove(entity);
            }
            public class Sessions : Interfaces.IRepository<Entities.Session>
            {
                private readonly Context _ctx;
                public Sessions(Context context) => _ctx = context;

                public Session GetById(int id) => _ctx.Sessions.Find(id);

                public IEnumerable<Session> GetAll() => _ctx.Sessions.ToList();

                public void Add(Session entity) => _ctx.Sessions.Add(entity);

                public void Update(Session entity, Action<Session> changes)
                {
                    changes(entity);
                    _ctx.Sessions.Update(entity);
                }

                public void Delete(Session entity) => _ctx.Sessions.Remove(entity);
            }
            public class Categories : Interfaces.IRepository<Entities.Category>
            {
                private readonly Context _ctx;
                public Categories(Context context) => _ctx = context;

                public Category GetById(int id) => _ctx.Categories.Find(id);

                public IEnumerable<Category> GetAll() => _ctx.Categories.ToList();

                public void Add(Category entity) => _ctx.Categories.Add(entity);

                public void Update(Category entity, Action<Category> changes)
                {
                    changes(entity);
                    _ctx.Categories.Update(entity);
                }

                public void Delete(Category entity) => _ctx.Categories.Remove(entity);
            }
            public class Gears : Interfaces.IRepository<Entities.Gear>
            {
                private readonly Context _ctx;
                public Gears(Context context) => _ctx = context;

                public Gear GetById(int id) => _ctx.Gears.Find(id);

                public IEnumerable<Gear> GetAll() => _ctx.Gears.ToList();

                public void Add(Gear entity) => _ctx.Gears.Add(entity);

                public void Update(Gear entity, Action<Gear> changes)
                {
                    changes(entity);
                    _ctx.Gears.Update(entity);
                }

                public void Delete(Gear entity) => _ctx.Gears.Remove(entity);
            }
            public class GearLoans : Interfaces.IRepository<Entities.GearLoan>
            {
                private readonly Context _ctx;
                public GearLoans(Context context) => _ctx = context;

                public GearLoan GetById(int id) => _ctx.GearLoans.Find(id);

                public IEnumerable<GearLoan> GetAll() => _ctx.GearLoans.ToList();

                public void Add(GearLoan entity) => _ctx.GearLoans.Add(entity);

                public void Update(GearLoan entity, Action<GearLoan> changes)
                {
                    changes(entity);
                    _ctx.GearLoans.Update(entity);
                }

                public void Delete(GearLoan entity) => _ctx.GearLoans.Remove(entity);
            }
        }

        public class Controllers
        {
            public class Members
            {
                private readonly IUnitOfWork _uow;

                public Members(IUnitOfWork UnitOfWork)
                {
                    _uow = UnitOfWork;
                }

                public void Register(Entities.Member entity)
                {
                    if (entity == null) throw new ArgumentNullException(nameof(entity));

                    _uow.Members.Add(entity);
                }

                public int Complete() => _uow.Complete();
            }

            public class Coaches
            {
                private readonly IUnitOfWork _uow;

                public Coaches(IUnitOfWork UnitOfWork)
                {
                    _uow = UnitOfWork;
                }

                public void Register(Entities.Coach entity)
                {
                    if (entity == null) throw new ArgumentNullException(nameof(entity));

                    _uow.Coaches.Add(entity);
                }

                public Coach Login(string email, string password) => _uow.Coaches.Login(email, password);

                public int Complete() => _uow.Complete();
            }
        }
    }
}
