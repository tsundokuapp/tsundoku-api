using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TsundokuTraducoes.Api.Models.Capitulo;
using TsundokuTraducoes.Api.Models.Obra;

namespace TsundokuTraducoes.Api.Models.Volume
{
    public class VolumeComic
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Numero { get; set; }
        public string Sinopse { get; set; }
        public string ImagemVolume { get; set; }
        public string Slug { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTime DataInclusao { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; }
        public string DiretorioImagemVolume { get; set; }
        public Guid ComicId { get; set; }
        public virtual Comic Comic { get; set; }
        public List<CapituloComic> ListaCapitulo { get; set; }
        public VolumeComic()
        {
            ListaCapitulo = new List<CapituloComic>();
        }
    }
}