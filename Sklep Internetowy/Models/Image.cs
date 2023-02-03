using Microsoft.AspNetCore.Identity;
using Sklep_Internetowy.Interfaces;
using Sklep_Internetowy.Models.Interfaces;

namespace Sklep_Internetowy.Models
{
    public class Image : IFile, IEntity
    {
        public int Id { get; set; }

        public Guid Guid { get; private set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public ProductDetail ProductDetails { get; set; }

        public string GetFileName()
            => Name;

        public string GetFileTitle()
            => Title;
        public Image() 
        {
            Guid = Guid.NewGuid();
        }

    }
}
