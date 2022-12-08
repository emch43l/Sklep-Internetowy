using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface ICartRepository
    {
        public Cart GetUserCart(AppUser user);

        public void Save();

    }
}
