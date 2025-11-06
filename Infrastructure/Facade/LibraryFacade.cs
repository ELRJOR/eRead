using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Facade
{
    public class LibraryFacade
    {
        private readonly IAutorRepository _autores;
        private readonly ILibroRepository _libros;

        public LibraryFacade(IAutorRepository autores, ILibroRepository libros)
        {
            _autores = autores;
            _libros = libros;
        }

        public Task<IEnumerable<Autor>> GetAutores()
            => _autores.GetAllAsync();

        public Task<Autor?> GetAutorById(int id)
            => _autores.GetByIdAsync(id);

        public async Task<int> AddAutor(Autor autor)
        {
            var created = await _autores.AddAsync(autor); 
            return created.Id_Autor;                     
        }

        public Task<bool> UpdateAutor(Autor autor)
            => _autores.UpdateAsync(autor);

        public Task<bool> DeleteAutor(int id)
            => _autores.DeleteAsync(id);


        public Task<IEnumerable<Libro>> GetLibros()
            => _libros.GetAllAsync();

        public Task<Libro?> GetLibroById(int id)
            => _libros.GetByIdAsync(id);


        public async Task<int> AddLibro(Libro libro)
        {
            var created = await _libros.AddAsync(libro); 
            return created.Id_Libro;                     
        }

        public Task<bool> UpdateLibro(Libro libro)
            => _libros.UpdateAsync(libro);

        public Task<bool> DeleteLibro(int id)
            => _libros.DeleteAsync(id);
    }
}
