using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Models
{
    public class ProductRating
    {
        public int Id { get; set; }

        public int Rating { get; set; } 

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        public Product Product { get; set; }

        public IdentityUser User { get; set; }

        public ProductRating()
        {
            CreationDate = DateTime.Now;
        }
    }
}
