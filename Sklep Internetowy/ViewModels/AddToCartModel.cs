using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels
{
    public class AddToCartModel
    {
        [Required]
        public string ProductId { get; set; }

        [Required]
        [Range(1,5)]
        public int Count { get; set; }
    }
}
