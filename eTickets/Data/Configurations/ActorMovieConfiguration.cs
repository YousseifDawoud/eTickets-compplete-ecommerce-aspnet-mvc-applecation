using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eTickets.Data.Configurations
{
    public class ActorMovieConfiguration : IEntityTypeConfiguration<Actor_Movie>
    {
        public void Configure(EntityTypeBuilder<Actor_Movie> builder)
        {
            // Table
            builder.ToTable("Actors_Movies");

            // Composite Primary Key
            builder.HasKey(am => new { am.MovieId, am.ActorId });

            // Relationship: Actor_Movie → Movie (Many-to-One)
            builder.HasOne(m => m.Movie)
                   .WithMany(am => am.Actors_Movies)
                   .HasForeignKey(m => m.MovieId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Actor_Movie → Actor (Many-to-One)
            builder.HasOne(a => a.Actor)
                   .WithMany(am => am.Actors_Movies)
                   .HasForeignKey(a => a.ActorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
