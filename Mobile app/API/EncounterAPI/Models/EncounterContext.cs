using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncounterAPI.Models
{
    public class EncounterContext : DbContext
    {
        public DbSet<RouteModel> Routes { get; set; }
        public DbSet<Waypoint> Waypoints { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=D:\Database\EncounterDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(t => new { t.Username });
            modelBuilder.Entity<User>().HasIndex(t => t.Username).IsUnique();
            modelBuilder.Entity<Rating>().HasKey(t => new { t.RouteId, t.Username });
        }

    }
}
