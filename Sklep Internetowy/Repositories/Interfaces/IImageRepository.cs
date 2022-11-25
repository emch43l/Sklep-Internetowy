using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface IImageRepository
    {
        public Image? GetImageByGuid(Guid id);

        public IEnumerable<Image>? GetImages();

        public Product? GetProductByImageGuid(Guid id);

        public Image AddImage(Image image);

        public Image? RemoveImage(Guid id);

        public void Save();
    }
}
