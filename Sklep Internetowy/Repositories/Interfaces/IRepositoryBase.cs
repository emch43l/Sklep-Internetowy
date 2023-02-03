using Sklep_Internetowy.Models.Interfaces;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class, IEntity
    {
        Task<List<T>> GetAll();
        Task<T?> GetOneByGuid(Guid id);
        Task<T?> Remove(Guid id);
        Task<T> Update(T entity);
        Task<T> Add(T entity);
        Task SaveChanges(CancellationToken cancellationToken = default);
    }
}
