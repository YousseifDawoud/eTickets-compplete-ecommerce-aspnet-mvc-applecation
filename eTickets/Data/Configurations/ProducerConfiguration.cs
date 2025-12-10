using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eTickets.Data.Configurations
{
    public class ProducerConfiguration : IEntityTypeConfiguration<Producer>
    {
        public void Configure(EntityTypeBuilder<Producer> builder)
        {
            // Table name (optional)
            builder.ToTable("Producers");

            // Primary Key
            builder.HasKey(p => p.Id);

            // ProfilePictureURL
            builder.Property(p => p.ProfilePictureURL)
                   .IsRequired()
                   .HasMaxLength(500); // optional, you may remove/adjust

            // FullName
            builder.Property(p => p.FullName)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasAnnotation("MinLength", 3);
            // EF does not enforce MinLength; UI/service should validate

            // Bio
            builder.Property(p => p.Bio)
                   .IsRequired()
                   .HasMaxLength(2000); // optional length

            // Relationships: Producer 1 → Many Movies
            builder.HasMany(p => p.Movies)
                   .WithOne(m => m.Producer)
                   .HasForeignKey(m => m.ProducerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
