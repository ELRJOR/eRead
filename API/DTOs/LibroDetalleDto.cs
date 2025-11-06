namespace API.DTOs
{
    public class LibroDetalleDto
    {
        public int Id_Libro { get; set; }
        public string Titulo { get; set; } = "";
        public string Categoria { get; set; } = "";
        public int Anio { get; set; }

        public required AutorSimpleDto Autor { get; set; }
    }


    public class AutorSimpleDto
    {
        public int Id_Autor { get; set; }
        public string Nombre { get; set; } = "";
    }
}
