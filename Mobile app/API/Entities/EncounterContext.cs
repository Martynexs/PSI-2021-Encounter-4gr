using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EncounterAPI.Models
{
    public class EncounterContext : DbContext
    {
        public DbSet<RouteModel> Routes { get; set; }
        public DbSet<Waypoint> Waypoints { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Quiz> Ouizzes { get; set; }
        public DbSet<QuizAnswers> QuizzAnswers { get; set; }
        public DbSet<RouteCompletion> RouteCompletions { get; set; }
        public DbSet<WaypointCompletion> WaypointCompletions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\Users\Juste\source\repos\EncounterDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasIndex(t => t.Username).IsUnique();
            modelBuilder.Entity<Rating>().HasKey(t => new { t.RouteId, t.UserId });
            modelBuilder.Entity<Quiz>().HasKey(t => new { t.Id });
            modelBuilder.Entity<QuizAnswers>().HasKey(t => new { t.Id });
            modelBuilder.Entity<RouteCompletion>().HasKey(t => new { t.RouteId, t.UserId });
            modelBuilder.Entity<WaypointCompletion>().HasKey(t => new { t.RouteCompletionRouteId, t.RouteCompletionUserId, t.WaypointId });
        }

    }
}
