using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Models.Contexts;
using Sklep_Internetowy.Repositories.Interfaces;

namespace Sklep_Internetowy.Repositories
{
    public class ProducerRepository : IProducerRepository, IDisposable
    {
        private bool disposed = false;

        private readonly DataContext _context;

        public ProducerRepository(DataContext context) 
        {
            _context = context;
        }

        public Producer AddProducer(Producer producer)
        {
            return _context.Producers.Add(producer).Entity;
        }

        public Producer? GetProducerByGuid(string id)
        {
            id = id.ToUpper();
            return _context.Producers.FirstOrDefault(p => p.Guid.ToString() == id);
        }

        public IEnumerable<Product>? GetProducerProducts(string id)
        {
            id = id.ToUpper();
            return _context.Producers
                .Include(p => p.Products)
                .FirstOrDefault(p => p.Guid.ToString() == id)?
                .Products;
        }
        public IEnumerable<Producer> GetProducers()
        {
            return _context.Producers.ToList();
        }

        public Producer? RemoveProducer(string id)
        {
            id = id.ToUpper();
            Producer? entity = _context.Producers.FirstOrDefault(e => e.Guid.ToString() == id);
            if (entity != null)
                return _context.Producers.Remove(entity).Entity;
            return null;
        }

        public Producer UpdateProducer(Producer producer)
        {
            throw new NotImplementedException();
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
