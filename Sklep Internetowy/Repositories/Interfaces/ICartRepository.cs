using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUser(AppUser user);

        Task SaveChanges(CancellationToken cancellation = default);
    }
}
