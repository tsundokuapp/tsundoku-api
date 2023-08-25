using System;
using System.ComponentModel.DataAnnotations;
using TsundokuTraducoes.Api.Models.Volume;

namespace TsundokuTraducoes.Api.Models.Capitulo
{
    public class CapituloNovel
    {
        [Key]
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Parte { get; set; }
        public string Titulo { get; set; }
        public string DescritivoCapitulo { get; set; }
        public string ConteudoNovel { get; set; }
        public string Slug { get; set; }
        public string UsuarioCadastro { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTime DataInclusao { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; }
        public string DiretorioImagemCapitulo { get; set; }
        public int OrdemCapitulo { get; set; }
        public bool EhIlustracoesNovel { get; set; }
        public virtual VolumeNovel Volume { get; set; }
        public int VolumeId { get; set; }
        public string Tradutor { get; set; }
        public string Revisor { get; set; }
        public string QC { get; set; }
    }
}
