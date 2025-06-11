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
        public DbSet<Entities.Coach> Coaches { get; set; }
        public DbSet<Entities.Session> Sessions { get; set; }
        public DbSet<Entities.Category> Categories { get; set; }
        public DbSet<Entities.Gear> Gears { get; set; }
        public DbSet<Entities.GearLoan> GearLoans { get; set; }

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
}
