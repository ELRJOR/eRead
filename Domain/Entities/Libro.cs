using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Libro
    {
        public int Id_Libro { get; set; }
        public required string Titulo { get; set; }
        public required string Categoria { get; set; }
        public int Anio { get; set; }

        public int Autor { get; set; }
        public Autor? AutorNavigation { get; set; }
    }
}
