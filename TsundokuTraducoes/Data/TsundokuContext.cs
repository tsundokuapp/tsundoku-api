using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Data
{
    public class TsundokuContext : DbContext
    {
        public DbSet<CapituloComic> CapituloManga { get; set; }
        public DbSet<CapituloNovel> CapituloNovel { get; set; }
        public DbSet<ComentarioObraRecomendada> ComentarioObraRecomendada { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<GeneroObra> GeneroObra { get; set; }
        public DbSet<Obra> Obra { get; set; }
        public DbSet<ObraRecomendada> ObraRecomendada { get; set; }
        public DbSet<Volume> Volume { get; set; }
        public DbSet<Imagem> Imagem { get; set; }
        public TsundokuContext(DbContextOptions<TsundokuContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genero>()
               .HasData(new List<Genero>()
               {
                        new Genero { Id = 1, Descricao = "Ação", Slug = "acao"},
                        new Genero { Id = 2, Descricao = "Aventura", Slug = "aventura"},
                        new Genero { Id = 3, Descricao = "Comédia", Slug = "comedia"},
                        new Genero { Id = 4, Descricao = "Drama", Slug = "drama"},
                        new Genero { Id = 5, Descricao = "Slice of Life", Slug = "slice-of-life"},
                        new Genero { Id = 6, Descricao = "Isekai", Slug = "isekai"},
                        new Genero { Id = 7, Descricao = "Harém", Slug = "harem"},
                        new Genero { Id = 8, Descricao = "Horror", Slug = "horror"},
                        new Genero { Id = 9, Descricao = "Fantasia", Slug = "fantasia"},
                        new Genero { Id = 10, Descricao = "Seinen", Slug = "seinen"},
               });

            modelBuilder.Entity<GeneroObra>()
            .HasKey(go => new { go.ObraId, go.GeneroId });

            modelBuilder.Entity<GeneroObra>()
                .HasOne(go => go.Obra)
                .WithMany(cinema => cinema.GenerosObra)
                .HasForeignKey(go => go.ObraId);

            modelBuilder.Entity<GeneroObra>()
                .HasOne(go => go.Genero)
                .WithMany(filme => filme.GenerosObra)
                .HasForeignKey(go => go.GeneroId);
        }
    }
}
