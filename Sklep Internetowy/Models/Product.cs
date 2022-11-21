using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_Internetowy.Models
{
    public class Product
    {
        public Product() 
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime Creation_Date { get; set; }

        public string? Description { get; set; }

        public ICollection<Image>? Images { get; set; }

        public IdentityUser User { get; set; }

    }
}
