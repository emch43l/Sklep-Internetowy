using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_Internetowy.Models
{
    public class ProductDetail
    {
        public int Id { get; set; } 

        public string Description { get; set; }

        public DateTime Creation_Date { get; set; }

        public ICollection<Image>? Images { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}
