using Sklep_Internetowy.ViewModels.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels
{
    public class ImageModel
    {
        [Required]
        [AllowedFileExtensions(".jpg", ".png", ".jpeg", ".gif")]
        [MaxFileSize(100, Services.Type.KB)]
        public List<IFormFile> Images { get; set; }
    }
}
