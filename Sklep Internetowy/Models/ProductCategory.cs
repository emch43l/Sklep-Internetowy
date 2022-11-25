using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_Internetowy.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public Guid Guid { get; private set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            Guid = Guid.NewGuid();
        }
    }
}
