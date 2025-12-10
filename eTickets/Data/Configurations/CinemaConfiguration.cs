using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eTickets.Data.Configurations
{
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            // Table name (optional)
            builder.ToTable("Cinemas");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Logo
            builder.Property(c => c.Logo)
                   .IsRequired()
                   .HasMaxLength(500); // optional

            // Name
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100); // recommended for names

            // Description
            builder.Property(c => c.Description)
                   .IsRequired()
                   .HasMaxLength(2000); // optional

            // Relationships: Cinema 1 → Many Movies
            builder.HasMany(m => m.Movies)
                   .WithOne(c => c.Cinema)
                   .HasForeignKey(c => c.CinemaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
