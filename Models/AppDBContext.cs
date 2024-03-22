using GlobalLogic_PizzaIncSln.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GlobalLogic_PizzaIncSln.Models
{
    public class AppDBContext :DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Projection>()
           .HasOne(p => p.Schedule)
           .WithMany(s => s.Projection)
           .HasForeignKey(p => p.ScheduleId);
        }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Projection> Projections { get; set; }

    }
}
