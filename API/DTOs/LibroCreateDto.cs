namespace API.DTOs
{
    public class LibroCreateDto
    {
        public required string Titulo { get; set; }
        public required string Categoria { get; set; }
        public int Anio { get; set; }
        public int Autor { get; set; }
    }
}
