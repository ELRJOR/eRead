using API.DTOs;
using Domain.Entities;
using Infrastructure.Facade;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly LibraryFacade _facade;

        public LibroController(LibraryFacade facade)
        {
            _facade = facade;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var libros = await _facade.GetLibros();

            var dto = libros.Select(l => new LibroDetalleDto
            {
                Id_Libro = l.Id_Libro,
                Titulo = l.Titulo,
                Categoria = l.Categoria,
                Anio = l.Anio,

                Autor = new AutorSimpleDto
                {
                    Id_Autor = l.AutorNavigation.Id_Autor,
                    Nombre = l.AutorNavigation.Nombre
                }
            });

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var libro = await _facade.GetLibroById(id);
            if (libro == null) return NotFound();

            var dto = new LibroDetalleDto
            {
                Id_Libro = libro.Id_Libro,
                Titulo = libro.Titulo,
                Categoria = libro.Categoria,
                Anio = libro.Anio,
                Autor = new AutorSimpleDto
                {
                    Id_Autor = libro.AutorNavigation.Id_Autor,
                    Nombre = libro.AutorNavigation.Nombre
                }
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LibroCreateDto dto)
        {
            var libro = new Libro
            {
                Titulo = dto.Titulo,
                Categoria = dto.Categoria,
                Anio = dto.Anio,
                Autor = dto.Autor
            };

            var id = await _facade.AddLibro(libro);

            return CreatedAtAction(nameof(GetById), new { id }, libro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LibroCreateDto dto)
        {
            var libro = await _facade.GetLibroById(id);
            if (libro == null) return NotFound();

            libro.Titulo = dto.Titulo;
            libro.Categoria = dto.Categoria;
            libro.Anio = dto.Anio;
            libro.Autor = dto.Autor;

            await _facade.UpdateLibro(libro);

            return Ok(libro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _facade.DeleteLibro(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
