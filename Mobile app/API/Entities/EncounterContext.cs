using Microsoft.EntityFrameworkCore;

namespace EncounterAPI.Models
{
    public class EncounterContext : DbContext
    {
        public DbSet<RouteModel> Routes { get; set; }
        public DbSet<Waypoint> Waypoints { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionChoice> QuestionChoices { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=C:\Users\Juste\source\repos\EncounterDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasIndex(t => t.Username).IsUnique();
            modelBuilder.Entity<Rating>().HasKey(t => new { t.RouteId, t.UserId });
        }

    }
}
