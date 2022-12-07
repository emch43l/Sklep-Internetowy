using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep_Internetowy.Models
{
    public class ProductCategory
    {
        [ValidateNever]
        public int Id { get; set; }

        [ValidateNever]
        public Guid Guid { get; private set; }
        public string Name { get; set; }

        [ValidateNever]
        public ICollection<Product> Products { get; set; }

        public ProductCategory()
        {
            Guid = Guid.NewGuid();
        }
    }
}
