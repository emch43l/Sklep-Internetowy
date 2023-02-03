using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Utils.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels.Models
{
    public class ProductEditViewModel : IValidatableObject
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 49999.99)]
        public double? Price { get; set; }

        [Required]
        [CollectionLength(10, ErrorMessage = "Maksymalna ilość dodatkowych informacji nie może być większa niż 10 !")]
        public List<string> AditionalInformations { get; set; }

        [Required]
        [CollectionLength(5, ErrorMessage = "Maksymalna ilość przypisanych kategorii to 5 !")]
        public List<Guid> CategoryId { get; set; }

        [Required]
        public Guid ProducerId { get; set; }
        [ValidateNever]
        public List<ProductCategory> Categories { get; set; }

        [ValidateNever]
        public List<SelectListItem> Producers { get; set; }

        [ValidateNever]
        public List<Image> Files { get; set; }

        public ProductEditViewModel()
        {
            CategoryId = new List<Guid>();
            AditionalInformations = new List<string>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            List<ValidationAttribute> attributes = new List<ValidationAttribute>()
            {
                new StringLengthAttribute(100)
                {
                    MinimumLength = 2,
                    ErrorMessage = "Maksymalna długość informacji to 100 znaków !"
                },
                new GuidTypeAttribute()
                {

                }
            };

            foreach (string value in AditionalInformations)
            {
                Validator.TryValidateValue(value, validationContext, validationResults, attributes);
            }

            return validationResults;
        }
    }
}
