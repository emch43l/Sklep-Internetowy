using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_Internetowy.Models
{
    public class CartItem
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }

        [Key, Column(Order = 1)]
        public int CartId { get; set; }

        public Product Product { get; set; }
        public Cart Cart { get; set; }

        public int Count { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
