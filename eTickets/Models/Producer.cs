using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }

        public string ProfilePictureURL { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? Bio { get; set; }
    }
}
