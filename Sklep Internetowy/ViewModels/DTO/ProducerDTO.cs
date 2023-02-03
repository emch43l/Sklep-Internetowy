using Sklep_Internetowy.Models;
using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.ViewModels.DTO
{
    public class ProducerDTO
    {
        [Required(ErrorMessage = "To pole jest wymagane !")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Długość pola musi się znajdować w przedziale między 2 a 30 znakami")]
        public string Name { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane !")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Długość opisu producenta musi się znajdować w przedziale między 2 a 500 znakami")]
        public string Description { get; set; }

        public Producer MapTo()
        {
            return new Producer
            {
                Name = Name,
                Description = Description
            };
        }
    }
}
