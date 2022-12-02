using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels
{
    public class AddOpinionModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Range(1,5)]
        public int Rating { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
