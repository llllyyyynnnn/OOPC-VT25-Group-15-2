using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using static DataManager.Entities;
using static DataManager.Interfaces;

/*
DbSet is defined in Context, which is then referred to as _ctx in repositories and then retrieved. Allows changes
We set relevant keys and rules in ModelCreating, to have a structure.
UnitOfWork handles transactions by having references to instances of the repositories, which in turn have an instance to _ctx, where we access DB'
We then commit only when all changes are done. No partial transactions.

This is a great way of ensuring specific validations for specific repos, where we need to check different values and such.
 */

namespace DataManager
{
    public class Context : DbContext
    {
        public DbSet<Entities.Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Gear Loan must have a reference to specific gear. It must also have one owner, and one owner can have multiple loans.
            /*
                         modelBuilder.Entity<GearLoan>()
                        .HasOne(gl => gl.gear)
                        .WithOne()
                        .HasForeignKey<GearLoan>(gl => gl.gear.id)
                        .IsRequired();

                        modelBuilder.Entity<GearLoan>()
                        .HasOne(gl => gl.loanOwner)
                        .WithMany()
                        .HasForeignKey(gl => gl.gear.id)
                        .IsRequired();
             */
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ApplicationDBSample;Trusted_Connection=True;");
    }
    public class UnitOfWork : Interfaces.IUnitOfWork
    {
        private readonly Context _ctx;
        public Repositories.Members Members { get; set; }
        public UnitOfWork(Context context)
        {
            _ctx = context;
            Members = new Repositories.Members(_ctx);
        }

        public int Complete() => _ctx.SaveChanges();
        public void Dispose() => _ctx.Dispose();
    }

    public class Repositories
    {
        public class Members : Interfaces.IRepository<Entities.Member>
        {
            private readonly Context _ctx;
            public Members(Context context)
            {
                _ctx = context;
            }

            public Member GetById(int id) => _ctx.Members.Find(id);

            public IEnumerable<Member> GetAll() => _ctx.Members.ToList();

            public void Add(Member entity) => _ctx.Members.Add(entity);

            public void Update(Member entity, Action<Member> changes) {
                changes(entity);
                _ctx.Members.Update(entity);
            }

            public void Delete(Member entity) => _ctx.Members.Remove(entity);
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
                if(entity == null) throw new ArgumentNullException(nameof(entity));

                _uow.Members.Add(entity);
            }

            public int Complete() => _uow.Complete();
        }
    }
}
