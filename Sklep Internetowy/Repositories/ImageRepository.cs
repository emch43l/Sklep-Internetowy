using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Models.Contexts;
using Sklep_Internetowy.Repositories.Interfaces;

namespace Sklep_Internetowy.Repositories
{
    public class ImageRepository : IImageRepository, IDisposable
    {
        private bool disposed = false;

        private readonly DataContext _context;

        public ImageRepository(DataContext context)
        {
            _context = context;
        }
        public Image? GetImageByGuid(Guid id)
        {
            return _context.Images
                .FirstOrDefault(i => i.Guid == id);
        }

        public IEnumerable<Image>? GetImages()
        {
            return _context.Images.ToList();
        }

        public Product? GetProductByImageGuid(Guid id)
        {
            return _context.Images
                .FirstOrDefault(i => i.Guid == id)?
                .ProductDetails?.Product;
        }

        public Image AddImage(Image image)
        {
            throw new NotImplementedException();
        }

        public Image? RemoveImage(Guid id)
        {
            Image? entity = _context.Images.FirstOrDefault(e => e.Guid == id);
            if(entity != null)
                return _context.Images.Remove(entity).Entity;
            return null;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
