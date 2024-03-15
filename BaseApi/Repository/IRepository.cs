using BaseApi.Models;

namespace BaseApi.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<T> Create(T newT);
        Task<bool> Update(Guid id, T updatedT);
        Task<bool> Delete(Guid id);
    }
}
