using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TsundokuTraducoes.Entities.Entities.Volume;

namespace TsundokuTraducoes.Entities.Entities.Capitulo
{
    public class CapituloNovel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string Parte { get; set; }
        public int OrdemCapitulo { get; set; }
        public string Titulo { get; set; }
        public string ConteudoNovel { get; set; }
        public string Slug { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string DiretorioImagemCapitulo { get; set; }
        public bool EhIlustracoesNovel { get; set; }        
        public string Tradutor { get; set; }
        public string Revisor { get; set; }
        public string QC { get; set; }
        public Guid VolumeId { get; set; }
        public virtual VolumeNovel Volume { get; set; }
        public string ListaImagensJson { get; set; }

        public CapituloNovel()
        {
            
        }

        public void AdicionaCapitulo(Guid id, string numero, string parte, int ordemCapitulo, string titulo, string conteudoNovel, string slug,
            string usuarioInclusao, string usuarioAlteracao, DateTime dataInclusao, DateTime dataAlteracao, string diretorioImagemCapitulo,
            bool ehIlustracoesNovel, string tradutor, string revisor, string qC, Guid volumeId, string listaImagensJson)
        {
            Id = id;
            Numero = numero;
            Parte = parte;
            OrdemCapitulo = ordemCapitulo;
            Titulo = titulo;
            ConteudoNovel = conteudoNovel;
            Slug = slug;
            UsuarioInclusao = usuarioInclusao;
            UsuarioAlteracao = usuarioAlteracao;
            DataInclusao = dataInclusao;
            DataAlteracao = dataAlteracao;
            DiretorioImagemCapitulo = diretorioImagemCapitulo;
            EhIlustracoesNovel = ehIlustracoesNovel;
            Tradutor = tradutor;
            Revisor = revisor;
            QC = qC;
            VolumeId = volumeId;
            ListaImagensJson = listaImagensJson;
        }
    }
}
