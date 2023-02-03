using Sklep_Internetowy.Utils.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Sklep_Internetowy.ViewModels.Models
{
    public class AddToCartViewModel
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Count { get; set; }
    }
}
