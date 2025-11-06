using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Autor> Autores => Set<Autor>();
        public DbSet<Libro> Libros => Set<Libro>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable("autor");
                entity.HasKey(a => a.Id_Autor);

                entity.Property(a => a.Id_Autor)
                      .HasColumnName("id_autor");

                entity.Property(a => a.Nombre)
                      .HasColumnName("nombre");
            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.ToTable("libros");
                entity.HasKey(l => l.Id_Libro);

                entity.Property(l => l.Id_Libro)
                      .HasColumnName("id_libro");

                entity.Property(l => l.Titulo)
                      .HasColumnName("titulo");

                entity.Property(l => l.Categoria)
                      .HasColumnName("categoria");

                entity.Property(l => l.Anio)
                      .HasColumnName("anio");

                entity.Property(l => l.Autor)
                      .HasColumnName("autor");

                entity.HasOne(l => l.AutorNavigation)
                      .WithMany(a => a.Libros)
                      .HasForeignKey(l => l.Autor)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
