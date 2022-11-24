using Microsoft.AspNetCore.Identity;
using Sklep_Internetowy.Interfaces;

namespace Sklep_Internetowy.Models
{
    public class Image : IFile
    {
        public Image() 
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public ProductDetail ProductDetails { get; set; }
        public string GetFileName()
            => Name;

        public string GetFileTitle()
            => Title;

    }
}
