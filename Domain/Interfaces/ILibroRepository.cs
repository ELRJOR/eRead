using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILibroRepository
    {
        Task<IEnumerable<Libro>> GetAllAsync();
        Task<Libro?> GetByIdAsync(int id);
        Task<Libro> AddAsync(Libro libro);
        Task<bool> UpdateAsync(Libro libro);
        Task<bool> DeleteAsync(int id);
    }
}
