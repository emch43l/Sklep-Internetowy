using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Models.Contexts;
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
        public Cart GetUserCart(AppUser user)
        {
            return _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.ProductDetail)
                .FirstOrDefault(c => c.User == user) ?? new Cart() { User = user };
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
