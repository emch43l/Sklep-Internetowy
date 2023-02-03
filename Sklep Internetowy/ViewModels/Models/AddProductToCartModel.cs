using Sklep_Internetowy.Utils.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels.Models
{
    public class CartViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Count { get; set; }

    }
}
