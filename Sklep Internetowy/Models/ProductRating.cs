using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Models
{
    public class ProductRating
    {
        public int Id { get; set; }

        [Range(0, 5)]
        [Required]
        public double Rating { get; set; }

        [DataType(DataType.Date)]
        public DateTime Creation_Date { get; set; }

        public User User { get; set; }
    }
}
