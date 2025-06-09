using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using static DataManager.Entities;

namespace DataManager
{
    public class Context : DbContext
    {
        public DbSet<Entities.Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Gear Loan must have a reference to specific gear. It must also have one owner, and one owner can have multiple loans.
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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ApplicationDBSample;Trusted_Connection=True;");
    }

    public class Repositories
    {
        public class MemberRepo : DataManager.Interfaces.IRepository<DataManager.Entities.Member>
        {
            public Member GetById(int id)
            {
                return null;
            }

            public IEnumerable<Member> GetAll()
            {
                return new List<Member>();
            }

            public IEnumerable<Member> Find(Expression<Func<Member, bool>> predicate)
            {
                return new List<Member>();
            }

            public void Add(Member entity)
            {
            }

            public void Update(Member entity)
            {
            }

            public void Delete(Member entity)
            {
            }
        }
    }
}
