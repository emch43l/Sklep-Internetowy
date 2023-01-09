using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using NuGet.Packaging.Rules;
using Sklep_Internetowy.Interfaces;
using Sklep_Internetowy.Models;
using System.Collections;

namespace Sklep_Internetowy.ViewModels
{
    public class ProductEditViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double? Price { get; set; }

        [Required]
        public List<string> AditionalInformations { get; set; }

        [Required]
        public List<string> CategoryId { get; set; }

        [ValidateNever]
        public List<ProductCategory> Categories { get; set; }

        [ValidateNever]
        public List<SelectListItem> Producers { get; set; }

        [Required]
        public string ProducerId { get; set; }

        [ValidateNever]
        public List<Image> Files { get; set; }

        public ProductEditViewModel()
        {
            CategoryId = new List<string>();
            AditionalInformations = new List<string>();
        }

    }
}
