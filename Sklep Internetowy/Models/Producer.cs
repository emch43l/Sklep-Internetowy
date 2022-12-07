using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Sklep_Internetowy.Models
{
    public class Producer
    {
        [ValidateNever]
        public int Id { get; set; }

        [ValidateNever]
        public Guid Guid { get; set; }

        public string Name { get; set; }

        [ValidateNever]
        public ICollection<Product> Products { get; set; }

        public string Description { get; set; }

        public Producer()
        {
            Guid = Guid.NewGuid();
        }
    }
}
