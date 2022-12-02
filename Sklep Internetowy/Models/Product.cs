using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_Internetowy.Models
{
    public class Product
    {

        public int Id { get; set; }

        public Guid Guid { get; private set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ICollection<ProductRating> Rating { get; set; }

        public Producer Producer { get; set; }

        public ProductDetail ProductDetail { get; set; }

        public ICollection<ProductCategory> Categories { get; set; }

        public Product() 
        {
            Guid = Guid.NewGuid();
        }

        public double GetRating()
        {
            return (Rating.Count() == 0) ? 0 
                : (double)Rating.Sum(r => r.Rating) / Rating.Count();
        }

    }
}
