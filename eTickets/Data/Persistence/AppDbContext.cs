using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }

        // DbSets (Tables)
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many: Movie <-> Actor
            modelBuilder.Entity<Actor_Movie>() // Composite Primary Key
                .HasKey(am => new { am.MovieId, am.ActorId });

            modelBuilder.Entity<Actor_Movie>() // Movie With Actors_Movies
                .HasOne(am => am.Movie)
                .WithMany(m => m.Actors_Movies)
                .HasForeignKey(am => am.MovieId);

            modelBuilder.Entity<Actor_Movie>() // Actor With Actors_Movies
                .HasOne(am => am.Actor)
                .WithMany(a => a.Actors_Movies)
                .HasForeignKey(am => am.ActorId);
        }
    }
}
