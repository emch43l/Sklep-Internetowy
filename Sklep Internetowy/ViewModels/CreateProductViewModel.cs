﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Services;
using Sklep_Internetowy.Utils.Validation;
using Sklep_Internetowy.ViewModels.Models;

namespace Sklep_Internetowy.ViewModels
{
    public class CreateProductViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Nazwa produktu jest wymagana !")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Cena produktu jest wymagana!")]
        [Range(0.01, 49999.99, ErrorMessage = $"Niepoprawny zakres ceny ! Cena powinna mieścić się w zakresie od 0.01 do 49999.99")]
        [RegularExpression(@"^\d{0,6}(\.\d{1,2})?$", ErrorMessage = "Niepoprawna cena !")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Opis produktu jest wymagany !")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Dłgość opisu produktu powinna mieścić się pomiędzy 2 a 500 znakami !")]
        public string Description { get; set; }

        public ImageModel? Images { get; set; }

        [Required(ErrorMessage = "Dodatkowe informacje są wymagane !")]
        [CollectionLength(10, ErrorMessage = "Maksymalna ilość dodatkowych informacji nie może być większa niż 10 !")]
        public List<string> AditionalInformations { get; set; }

        [Required]
        public Guid ProducerId { get; set; }

        [Required]
        [CollectionLength(5, ErrorMessage = "Maksymalna ilość przypisanych kategorii to 5 !")]
        public List<Guid> CategoryId { get; set; }

        [ValidateNever]
        public List<SelectListItem> Producers { get; set; }

        [ValidateNever]
        public List<ProductCategory> Categories { get; set; }

        public CreateProductViewModel()
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