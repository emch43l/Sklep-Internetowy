using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Services;
using Sklep_Internetowy.ViewModels.Validation;

namespace Sklep_Internetowy.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [RegularExpression(@"^\d{0,6}(\.\d{1,2})?$", ErrorMessage = "Please provide valid price !")]
        [Range(0, 1000000)]
        [Required(ErrorMessage = "Price is required !")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public string Description { get; set; }

        [AllowedFileExtensions(".jpg",".png",".jpeg",".gif")]
        [MaxFileSize(100,Services.Type.KB)]
        public ICollection<IFormFile>? Images { get; set; }

        [Required]
        public ICollection<string> AditionalInformations { get; set; }

        [ValidateNever]
        public List<SelectListItem> Producers { get; set; }

        [ValidateNever]
        public List<ProductCategory> Categories { get; set; }

        [Display(Name = "Producent")]
        [Required]
        public string ProducerId { get; set; }

        [Display(Name = "Kategorie")]
        public List<string>? CategoryId { get; set; }

    }
}