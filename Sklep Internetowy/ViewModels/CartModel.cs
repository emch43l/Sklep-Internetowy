using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels
{
    public class CartModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Range(1,5)]
        public int Count { get; set; }

    }
}
