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
    public class Logic
    {
        public class UnitOfWork : Interfaces.IUnitOfWork
        {
            private readonly Context _ctx;
            public Repositories.Members Members { get; set; }
            public Repositories.Coaches Coaches { get; set; }
            public Repositories.Sessions Sessions { get; set; }
            public Repositories.Gears Gears { get; set; }
            public Repositories.GearLoans GearLoans { get; set; }

            public UnitOfWork(Context context)
            {
                _ctx = context;
                Members = new Repositories.Members(_ctx);
                Coaches = new Repositories.Coaches(_ctx);
                Sessions = new Repositories.Sessions(_ctx);
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
                public Member Login(string email, string password) => _ctx.Members.FirstOrDefault(coach => coach.mailAddress == email && coach.pinCode == password);
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

                public void Delete(Entities.Member entity)
                {
                    try
                    {
                        _uow.Members.Delete(entity);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                public void Update(Entities.Member entity, Action<Entities.Member> changes) => _uow.Members.Update(entity, changes);
                public IEnumerable<Entities.Member> GetMembers() => _uow.Members.GetAll();
                public Entities.Member GetMemberById(int id) => _uow.Members.GetById(id);
                public Entities.Member Login(string email, string password) => _uow.Members.Login(email, password);
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
                public IEnumerable<Entities.Coach> GetCoaches() => _uow.Coaches.GetAll();
                public Entities.Coach GetCoachById(int id) => _uow.Coaches.GetById(id);

                public Entities.Coach Login(string email, string password) => _uow.Coaches.Login(email, password);

                public int Complete() => _uow.Complete();
            }

            public class Gears
            {
                private readonly IUnitOfWork _uow;

                public Gears(IUnitOfWork UnitOfWork)
                {
                    _uow = UnitOfWork;
                }

                public void Delete(Entities.Gear entity) => _uow.Gears.Delete(entity);
                public void Update(Entities.Gear entity, Action<Gear> changes) => _uow.Gears.Update(entity, changes);
                public void Register(Entities.Gear entity)
                {
                    if (entity == null) throw new ArgumentNullException(nameof(entity));

                    _uow.Gears.Add(entity);
                }
                public IEnumerable<Entities.Gear> GetGear() => _uow.Gears.GetAll();
                public int Complete() => _uow.Complete();
            }

            public class GearLoans
            {
                private readonly IUnitOfWork _uow;

                public GearLoans(IUnitOfWork UnitOfWork)
                {
                    _uow = UnitOfWork;
                }

                public void Delete(Entities.GearLoan entity) => _uow.GearLoans.Delete(entity);
                public void Update(Entities.GearLoan entity, Action<GearLoan> changes) => _uow.GearLoans.Update(entity, changes);
                public void Register(Entities.GearLoan entity)
                {
                    if (entity == null) throw new ArgumentNullException(nameof(entity));

                    _uow.GearLoans.Add(entity);
                }
                public IEnumerable<Entities.GearLoan> GetGearLoans() => _uow.GearLoans.GetAll();
                public int Complete() => _uow.Complete();
            }
            public class Sessions
            {
                private readonly IUnitOfWork _uow;

                public Sessions(IUnitOfWork UnitOfWork)
                {
                    _uow = UnitOfWork;
                }

                public void Delete(Entities.Session entity) => _uow.Sessions.Delete(entity);
                public void Update(Entities.Session entity, Action<Entities.Session> changes) => _uow.Sessions.Update(entity, changes);
                public void Register(Entities.Session entity)
                {
                    if (entity == null) throw new ArgumentNullException(nameof(entity));

                    _uow.Sessions.Add(entity);
                }
                public IEnumerable<Entities.Session> GetSessions() => _uow.Sessions.GetAll();
                public IEnumerable<Entities.Member> GetMembers(int id) => _uow.Sessions.GetById(id).members;
                public int Complete() => _uow.Complete();
            }
        }
    }
}
