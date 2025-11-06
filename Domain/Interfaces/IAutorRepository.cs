using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> GetAllAsync();
        Task<Autor?> GetByIdAsync(int id);
        Task<Autor> AddAsync(Autor autor);
        Task<bool> UpdateAsync(Autor autor);
        Task<bool> DeleteAsync(int id);
    }
}
