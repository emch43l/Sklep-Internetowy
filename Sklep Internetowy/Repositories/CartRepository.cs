using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Contexts;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;

namespace Sklep_Internetowy.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;
        public CartRepository(DataContext context) 
        {
            _context = context;
        }

        public async Task<Cart?> GetCartByUser(AppUser user)
        {
            return await _context.Carts
                .Where(c => c.User == user)
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync();
        }

        public async Task SaveChanges(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
