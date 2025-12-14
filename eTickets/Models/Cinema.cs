using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        public string Logo { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        // Relationships
        public List<Movie> Movies { get; set; } = new();
    }
}
