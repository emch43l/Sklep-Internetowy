namespace Sklep_Internetowy.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public AppUser User { get; set; }

        public ICollection<CartItem> Items { get; set;}

        public decimal GetTotal()
        {
            return Items.Sum(i => i.Product.Price * i.Count);
        }
    }
}
