using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TsundokuTraducoes.Api.Models.Volume;

namespace TsundokuTraducoes.Api.Models.Capitulo
{
    public class CapituloComic
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string Parte { get; set; }
        public string Titulo { get; set; }
        public string ListaImagens { get; set; }
        public string Slug { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTime DataInclusao { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; }
        public string DiretorioImagemCapitulo { get; set; }
        public int OrdemCapitulo { get; set; }
        public Guid VolumeId { get; set; }
        public virtual VolumeComic Volume { get; set; }
    }
}
