using Microsoft.AspNetCore.Identity;
using Sklep_Internetowy.Interfaces;

namespace Sklep_Internetowy.Models
{
    public class Image : IFile
    {
        public Image() 
        {
            Guid = Guid.NewGuid();
        }

        public int Id { get; set; }
        public Guid Guid { get; private set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public ProductDetail ProductDetails { get; set; }
        public string GetFileName()
            => Name;

        public string GetFileTitle()
            => Title;

    }
}
