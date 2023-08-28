using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TsundokuTraducoes.Api.Models.Capitulo;
using TsundokuTraducoes.Api.Models.Obra;

namespace TsundokuTraducoes.Api.Models.Volume
{
    public class VolumeNovel
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
        public virtual Novel Obra { get; set; }
        public List<CapituloNovel> ListaCapitulo { get; set; }
        public VolumeNovel()
        {
            ListaCapitulo = new List<CapituloNovel>();
        }
    }
}