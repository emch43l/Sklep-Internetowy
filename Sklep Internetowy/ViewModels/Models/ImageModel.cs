using Sklep_Internetowy.Utils.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels.Models
{
    public class ImageModel
    {
        [AllowedFileExtensions(".jpg", ".png", ".jpeg", ".gif", ErrorMessage = "Niedopuszczalne rozszerzenie pliku ! Dozwolone typy plików to: .jpg, .png, .jpeg, .gif")]
        [MaxFileSize(200, Services.Type.KB, ErrorMessage = "Maksymalny rozmiar pojedynczego pliku to 200KB !")]
        [MaxFilesSize(100, Services.Type.MB, ErrorMessage = "Maksymalny rozmiar wszystkich plików to 100MB !")]
        [MaxFiles(5, ErrorMessage = "Maksymalna ilość plików to 5 !")]
        public ICollection<IFormFile>? Images { get; set; }
    }
}
