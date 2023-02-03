using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Contexts;
using Sklep_Internetowy.Models.Interfaces;
using Sklep_Internetowy.Repositories.Interfaces;

namespace Sklep_Internetowy.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        public readonly DataContext _context;

        public RepositoryBase(DataContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetOneByGuid(Guid id)
        {
            return await _context.Set<TEntity>().Where(e => e.Guid == id).FirstOrDefaultAsync();
        }

        public async Task<TEntity?> Remove(Guid id)
        {
            TEntity? entity = await _context.Set<TEntity>().Where(e => e.Guid == id).FirstOrDefaultAsync();

            if (entity == null)
                return entity;

            _context.Set<TEntity>().Remove(entity);

            return entity;
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);

            return entity;
        }

        public async Task SaveChanges(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
