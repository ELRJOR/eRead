using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly AppDbContext _context;

        public AutorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Autor>> GetAllAsync()
        {
            return await _context.Autores
                .Include(a => a.Libros)
                .ToListAsync();
        }

        public async Task<Autor?> GetByIdAsync(int id)
        {
            return await _context.Autores
                .Include(a => a.Libros)
                .FirstOrDefaultAsync(a => a.Id_Autor == id);
        }

        public async Task<Autor> AddAsync(Autor autor)
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return autor;
        }

        public async Task<bool> UpdateAsync(Autor autor)
        {
            var exists = await _context.Autores.AnyAsync(a => a.Id_Autor == autor.Id_Autor);
            if (!exists) return false;

            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null) return false;

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
