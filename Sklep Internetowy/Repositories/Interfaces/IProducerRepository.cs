using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface IProducerRepository
    {
        public IEnumerable<Product>? GetProducerProducts(string id);

        public Producer? GetProducerByGuid(string id);

        public Producer? RemoveProducer(string id);

        public Producer UpdateProducer(Producer producer);

        public Producer AddProducer(Producer producer);

        public IEnumerable<Producer> GetProducers();

        public void Save();
    }
}
