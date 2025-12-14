using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }

        [Display (Name = "Movie Poster")]
        public string ImageUrl { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        // Relationships

        // With Actor_Movie => Many to Many
        public List<Actor_Movie> Actors_Movies { get; set; } = new();

        // Many OF Movies Shown IN One Cinema
        [ ForeignKey(nameof(CinemaId))]         // Foreign Key
        public int CinemaId { get; set; }
        public Cinema? Cinema { get; set; }     // Navigation Property


        // Many OF Movies Produced BY One Producer
        [ ForeignKey(nameof(ProducerId))]       
        public int ProducerId { get; set; }     // Foreign Key
        public Producer? Producer { get; set; } // Navigation Property






    }
}
