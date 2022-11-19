using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_Internetowy.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [RegularExpression(@"^\d{0,6}(\.\d{1,2})?$", ErrorMessage = "Podaj poprawną cenę !")]
        [Range(0, 1000000)]
        [Required(ErrorMessage ="Podaj poprawną cenę !")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Creation_Date { get; set; }

        public string? Description { get; set; }

        public ICollection<Image>? Images { get; set; }

    }
}
