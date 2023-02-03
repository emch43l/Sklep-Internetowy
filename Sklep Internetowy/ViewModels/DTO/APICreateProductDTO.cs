using Sklep_Internetowy.Models;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels.DTO
{
    public class APICreateProductDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IEnumerable<string> Informations { get; set; }

        [Required]
        [Range(0, 100000)]
        public double? Price { get; set; }

        [Required]
        public Guid Producer { get; set; }

        [Required]
        public ICollection<string> Categories { get; set; }

        public Product MapTo()
        {
            ISet<ProductCategory> productCategories = Categories.Select(c => new ProductCategory() { Name = c }).ToHashSet();
            return new Product
            {
                Name = Name,
                Price = (decimal)Price,
            };
        }
    }
}
