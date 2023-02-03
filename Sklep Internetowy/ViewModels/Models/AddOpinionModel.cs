using Sklep_Internetowy.Utils.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels.Models
{
    public class AddOpinionModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane !")]
        [Range(1, 5, ErrorMessage = "Proszę wybrać zares od 1 do 5 gwiazdek !")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane !")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Dopuszczalna długość recenzji to od 2 do 500 znaków !")]
        public string Text { get; set; }
    }
}
