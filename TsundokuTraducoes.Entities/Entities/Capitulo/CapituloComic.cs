using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TsundokuTraducoes.Entities.Entities.Volume;

namespace TsundokuTraducoes.Entities.Entities.Capitulo
{
    public class CapituloComic
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public int OrdemCapitulo { get; set; }
        public string Parte { get; set; }
        public string Titulo { get; set; }
        public string ListaImagensJson { get; set; }
        public string Slug { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTime DataInclusao { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; }
        public string DiretorioImagemCapitulo { get; set; }
        public Guid VolumeId { get; set; }
        public virtual VolumeComic Volume { get; set; }

        public CapituloComic()
        {
            
        }

        public void AdicionaCapitulo(Guid id, string numero, int ordemCapitulo, string parte, string titulo, string listaImagens, string slug, string usuarioInclusao, 
            string usuarioAlteracao, DateTime dataInclusao, DateTime dataAlteracao, string diretorioImagemCapitulo, Guid volumeId)
        {
            Id = id;
            Numero = numero;
            OrdemCapitulo = ordemCapitulo;
            Parte = parte;
            Titulo = titulo;
            ListaImagensJson = listaImagens;
            Slug = slug;
            UsuarioInclusao = usuarioInclusao;
            UsuarioAlteracao = usuarioAlteracao;
            DataInclusao = dataInclusao;
            DataAlteracao = dataAlteracao;
            DiretorioImagemCapitulo = diretorioImagemCapitulo;
            VolumeId = volumeId;
        }
    }
}
