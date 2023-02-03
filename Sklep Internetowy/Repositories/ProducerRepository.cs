using Sklep_Internetowy.Contexts;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;

namespace Sklep_Internetowy.Repositories
{
    public class ProducerRepository : RepositoryBase<Producer>, IProducerRepository
    {
        public ProducerRepository(DataContext context) : base(context) { }
    }
}
