using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private readonly AppDbContext _context;

        public LibroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Libro>> GetAllAsync()
        {
            return await _context.Libros
                .Include(l => l.AutorNavigation)
                .ToListAsync();
        }

        public async Task<Libro?> GetByIdAsync(int id)
        {
            return await _context.Libros
                .Include(l => l.AutorNavigation)
                .FirstOrDefaultAsync(l => l.Id_Libro == id);
        }

        public async Task<Libro> AddAsync(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return libro;
        }

        public async Task<bool> UpdateAsync(Libro libro)
        {
            var exists = await _context.Libros.AnyAsync(l => l.Id_Libro == libro.Id_Libro);
            if (!exists) return false;

            _context.Libros.Update(libro);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return false;

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
