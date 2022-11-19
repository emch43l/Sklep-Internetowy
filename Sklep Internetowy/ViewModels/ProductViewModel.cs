using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [RegularExpression(@"^\d{0,6}(\.\d{1,2})?$", ErrorMessage = "Podaj poprawną cenę !")]
        [Range(0, 1000000)]
        [Required(ErrorMessage = "Podaj poprawną cenę !")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public string? Description { get; set; }

        public ICollection<IFormFile>? Images { get; set; }

    }
}