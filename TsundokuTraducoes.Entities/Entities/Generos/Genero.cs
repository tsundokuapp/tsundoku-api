using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TsundokuTraducoes.Entities.Entities.DePara;

namespace TsundokuTraducoes.Entities.Entities.Generos
{
    public class Genero
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Slug { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public virtual List<GeneroNovel> GenerosNovel { get; set; }
        public virtual List<GeneroComic> GenerosComic { get; set; }

        public Genero()
        {
            GenerosNovel = new List<GeneroNovel>();
            GenerosComic = new List<GeneroComic>();
        }

        public void AdicionaGenero(Guid id, string descricao, string slug, string usuarioAlteracao, string usuarioInclusao, 
            DateTime dataInclusao, DateTime dataAlteracao)
        {
            Id = id;
            Descricao = descricao;
            Slug = slug;
            UsuarioAlteracao = usuarioAlteracao;
            UsuarioInclusao = usuarioInclusao;
            DataInclusao = dataInclusao; 
            DataAlteracao = dataAlteracao;
        }
    }
}
