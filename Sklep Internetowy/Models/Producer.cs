namespace Sklep_Internetowy.Models
{
    public class Producer
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

        public string Description { get; set; }

        public Producer()
        {
            Guid = Guid.NewGuid();
        }
    }
}
