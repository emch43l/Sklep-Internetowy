using Microsoft.AspNetCore.Identity;

namespace Sklep_Internetowy.Models
{
    public class ProductRating
    {
        public int Id { get; set; }

        public int Rating { get; set; } 

        public Product Product { get; set; }

        public IdentityUser User { get; set; }
    }
}
