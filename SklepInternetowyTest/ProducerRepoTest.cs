using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;

namespace SklepInternetowyTest
{
    public class ProducerRepoTest : IProducerRepository
    {
        private readonly ISet<Producer> _producers;

        public ProducerRepoTest()
        {
            _producers = new HashSet<Producer>();
        }
        public Producer AddProducer(Producer producer)
        {
            _producers.Add(producer);
            return producer;
        }

        public Producer? GetProducerByGuid(string id)
        {
            return _producers.Where(p => p.Guid.ToString() == id).FirstOrDefault();
        }

        public IEnumerable<Product>? GetProducerProducts(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producer> GetProducers()
        {
            return _producers;
        }

        public Producer? RemoveProducer(string id)
        {
            Producer? producer = _producers.Where(p => p.Guid.ToString() == id).FirstOrDefault();
            if (_producers.Remove(producer))
                return producer;
            return null;
        }

        public void Save()
        {

        }

        public Producer UpdateProducer(Producer producer)
        {
            throw new NotImplementedException();
        }
    }
}
