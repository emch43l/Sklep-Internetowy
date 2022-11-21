﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Sklep_Internetowy.Services;
using Sklep_Internetowy.ViewModels.Validation;

namespace Sklep_Internetowy.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [RegularExpression(@"^\d{0,6}(\.\d{1,2})?$", ErrorMessage = "Please provide valid price !")]
        [Range(0, 1000000)]
        [Required(ErrorMessage = "Price is required !")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public string? Description { get; set; }

        [AllowedFileExtensions(".jpg",".png",".jpeg",".gif")]
        [MaxFileSize(100,Services.Type.KB)]
        public ICollection<IFormFile>? Images { get; set; }

    }
}