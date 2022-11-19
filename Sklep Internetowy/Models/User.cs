using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [MaxLength(9)]
        public string Phone { get; set; }

        public ICollection<ProductRating>? ProductRatings { get; set; }


    }
}
