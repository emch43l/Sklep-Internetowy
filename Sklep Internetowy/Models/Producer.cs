using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Sklep_Internetowy.Models.Interfaces;

namespace Sklep_Internetowy.Models
{
    public class Producer : IEntity
    {
        public int Id { get; set; }

        public Guid Guid { get; private set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }

        public string Description { get; set; }

        public Producer()
        {
            Guid = Guid.NewGuid();
        }
    }
}
