using API.DTOs;
using Domain.Entities;
using Infrastructure.Facade;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly LibraryFacade _facade;

        public AutorController(LibraryFacade facade)
        {
            _facade = facade;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _facade.GetAutores());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var autor = await _facade.GetAutorById(id);
            return autor == null ? NotFound() : Ok(autor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AutorCreateDto dto)
        {
            var autor = new Autor { Nombre = dto.Nombre };

            var id = await _facade.AddAutor(autor);

            return CreatedAtAction(nameof(GetById), new { id }, autor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AutorCreateDto dto)
        {
            var autor = await _facade.GetAutorById(id);
            if (autor == null) return NotFound();

            autor.Nombre = dto.Nombre;

            await _facade.UpdateAutor(autor);
            return Ok(autor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _facade.DeleteAutor(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
