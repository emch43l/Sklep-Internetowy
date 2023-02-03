using Microsoft.AspNetCore.Identity;
using Sklep_Internetowy.Models.Interfaces;
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

        public AppUser User { get; set; }

        public ProductRating()
        {
            CreationDate = DateTime.Now;
        }
    }
}
