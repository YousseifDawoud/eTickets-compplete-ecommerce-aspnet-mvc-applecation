using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eTickets.Data.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Table
            builder.ToTable("Movies");

            // Primary Key
            builder.HasKey(m => m.Id);

            // Name
            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Description
            builder.Property(m => m.Description)
                   .IsRequired()
                   .HasMaxLength(2000);

            // Price
            builder.Property(m => m.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            // Image URL
            builder.Property(m => m.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            // Start Date
            builder.Property(m => m.StartDate)
                   .IsRequired();

            // End Date
            builder.Property(m => m.EndDate)
                   .IsRequired();

            // Movie Category (Enum)
            builder.Property(m => m.MovieCategory)
                   .IsRequired();

            // Relationship: Movie 1 → Many Actor_Movie (Many-to-Many)
            builder.HasMany(am => am.Actors_Movies)
                   .WithOne(m => m.Movie)
                   .HasForeignKey(m => m.MovieId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Movie Many → One Cinema
            builder.HasOne(m => m.Cinema)
                   .WithMany(c => c.Movies)
                   .HasForeignKey(m => m.CinemaId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Movie Many → One Producer
            builder.HasOne(m => m.Producer)
                   .WithMany(p => p.Movies)
                   .HasForeignKey(m => m.ProducerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
