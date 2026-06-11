using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class Experience
    {
        public int Id { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
