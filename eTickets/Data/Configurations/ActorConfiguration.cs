using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eTickets.Data.Configurations
{
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            // Table name (optional)
            builder.ToTable("Actors");

            // Primary Key
            builder.HasKey(a => a.Id);

            // ProfilePictureURL
            builder.Property(a => a.ProfilePictureURL)
                   .IsRequired()
                   .HasMaxLength(500); // optional constraint, you may remove it

            // FullName
            builder.Property(a => a.FullName)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasAnnotation("MinLength", 3);
            // EF does not enforce MinLength, but you document it or validate in service/UI

            // Bio
            builder.Property(a => a.Bio)
                   .IsRequired()
                   .HasMaxLength(2000); // optional limit

            // Relationships (Actor ↔ Movies via join table Actor_Movie)
            builder.HasMany(a => a.Actors_Movies)
                   .WithOne(am => am.Actor)
                   .HasForeignKey(am => am.ActorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
