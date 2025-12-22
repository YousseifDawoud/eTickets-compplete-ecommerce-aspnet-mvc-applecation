using eTickets.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }



        [Required(ErrorMessage = "Profile picture is required")]
        [Display(Name = "Profile Picture")]
        public string ProfilePictureURL { get; set; } = null!;



        [Required(ErrorMessage = "Full name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;


        
        [Display(Name = "Biography")]
        [StringLength(500)]
        [Required(ErrorMessage = "Biography is required")]
        public string? Bio { get; set; }



        // Relationships
        public List<Actor_Movie> Actors_Movies { get; set; } = new();
    }
}
