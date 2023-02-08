using Sklep_Internetowy.Utils.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels.Models
{
    public class ImageModel
    {
        [Required(ErrorMessage = "To pole jest wymagane !")]
        [MaxFileSize(5,Services.Type.MB)]
        [MaxFilesSize(30,Services.Type.MB)]
        [CollectionLength(5)]
        public ICollection<IFormFile> Files { get; set; }
    }
}
