using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TsundokuTraducoes.Api.Models
{
    public class Volume
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Numero { get; set; }
        public string DescritivoVolume { get; set; }
        public string Sinopse { get; set; }
        public string ImagemVolume { get; set; }
        public string Slug { get; set; }
        public string UsuarioCadastro { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; }
        public string DiretorioImagemVolume { get; set; }
        public int ObraId { get; set; }
        public virtual Obra Obra { get; set; }
        public List<CapituloNovel> ListaCapituloNovel { get; set; }
        public List<CapituloComic> ListaCapituloComic { get; set; }
        public Volume()
        {
            ListaCapituloNovel = new List<CapituloNovel>();
            ListaCapituloComic = new List<CapituloComic>();
        }
    }
}