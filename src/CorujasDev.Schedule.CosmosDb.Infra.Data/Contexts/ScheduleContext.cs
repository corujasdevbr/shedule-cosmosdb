using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CorujasDev.Schedule.CosmosDb.Infra.Data.Contexts
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Represent Model Contact in database
        /// </summary>
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        
        /// <summary>
        /// Configure Database Model in create
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Create container contacts to model Contact
            modelBuilder.Entity<Contact>(x => { x.ToContainer("contacts"); });
            modelBuilder.Entity<TodoItem>(x => { x.ToContainer("todoitems");});

            //Define Id generated guid
            modelBuilder.Entity<TodoItem>().Property(p => p.ID).HasValueGenerator<GuidValueGenerator>();
            modelBuilder.Entity<Contact>().Property(p => p.ID).HasValueGenerator<GuidValueGenerator>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
