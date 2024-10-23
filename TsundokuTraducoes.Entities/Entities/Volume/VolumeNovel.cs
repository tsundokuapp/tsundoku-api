using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Obra;

namespace TsundokuTraducoes.Entities.Entities.Volume
{
    public class VolumeNovel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string ImagemVolume { get; set; }
        public string Slug { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string DiretorioImagemVolume { get; set; }
        public Guid NovelId { get; set; }
        public virtual Novel Novel { get; set; }
        public List<CapituloNovel> ListaCapitulo { get; set; }
                
        public VolumeNovel()
        {            
            ListaCapitulo = new List<CapituloNovel>();
        }

        public void AdicionaVolume(Guid id, string numero, string imagemVolume, string slug, string titulo, string sinopse, string usuarioInclusao,
            string usuarioAlteracao, DateTime dataInclusao, DateTime dataAlteracao, string diretorioImagemVolume, Guid novelId)
        {
            Id = id;
            Numero = numero;
            ImagemVolume = imagemVolume;
            Slug = slug;
            Titulo = titulo;
            Sinopse = sinopse;
            UsuarioInclusao = usuarioInclusao;
            UsuarioAlteracao = usuarioAlteracao;
            DataInclusao = dataInclusao;
            DataAlteracao = dataAlteracao;
            DiretorioImagemVolume = diretorioImagemVolume;
            NovelId = novelId;
        }
    }
}