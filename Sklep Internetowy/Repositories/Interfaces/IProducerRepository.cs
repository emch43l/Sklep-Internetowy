using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface IProducerRepository
    {
        public IEnumerable<Product>? GetProducerProducts(Guid id);

        public Producer? GetProducerByGuid(Guid id);

        public Producer? RemoveProducer(Guid id);

        public Producer UpdateProducer(Producer producer);

        public Producer AddProducer(Producer producer);

        public IEnumerable<Producer>? GetProducers();
    }
}
