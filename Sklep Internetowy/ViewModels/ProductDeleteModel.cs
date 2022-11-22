using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels
{
    public class ProductDeleteModel
    {
        [Required]
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [HiddenInput]
        public bool DeleteImagesOnRemoval { get; set; }
    }
}
