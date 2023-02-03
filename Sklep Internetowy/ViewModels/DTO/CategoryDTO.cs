using Sklep_Internetowy.Models;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels.DTO
{
    public class CategoryDTO
    {
        [Required(ErrorMessage = "To pole jest wymagane !")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Długość nazwy kategorii musi znajdować się w przedziale od 2 do 30 znaków !")]
        public string Name { get; set; }

        public ProductCategory MapTo()
        {
            return new ProductCategory
            { 
                Name = Name 
            };
        }
    }
}
