namespace Sklep_Internetowy.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Creation_Date { get; set; }
        public string Description { get; set; }
        public ICollection<Image> Images { get; set; }

    }
}
