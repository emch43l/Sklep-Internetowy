namespace Sklep_Internetowy.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }

    }
}
