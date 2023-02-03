using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Utils.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels.Models
{
    public class ProductDeleteModel
    {
        [Required]
        [HiddenInput]
        [GuidType]
        public Guid Id { get; set; }

        [Required]
        [HiddenInput]
        public bool DeleteImagesOnRemoval { get; set; }
    }
}
