public class Autor
{
    public int Id_Autor { get; set; }  
    public required string Nombre { get; set; } = null!;

    public ICollection<Libro> Libros { get; set; } = new List<Libro>();
}

public class Libro
{
    public int Id_Libro { get; set; }  
    public string Titulo { get; set; } = null!;
    public string Categoria { get; set; } = null!;
    public int Anio { get; set; }

    public int Autor { get; set; }  
    public Autor? AutorNavigation { get; set; }
}
