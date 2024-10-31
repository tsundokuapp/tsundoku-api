using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Volume;

namespace TsundokuTraducoes.Entities.Entities.Obra
{
    public class Comic
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }
        public string Titulo { get; set; }        
        public string TituloAlternativo { get; set; }        
        public string Alias { get; set; }        
        public string Autor { get; set; }        
        public string Artista { get; set; }
        public string Ano { get; set; }        
        public string Slug { get; set; }
        public int Visualizacoes { get; set; }        
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }        
        public string ImagemCapaPrincipal { get; set; }        
        public string Sinopse { get; set; }        
        public DateTime DataInclusao { get; set; }        
        public DateTime DataAlteracao { get; set; }        
        public bool EhObraMaiorIdade { get; set; }        
        public bool EhRecomendacao { get; set; }        
        public string CodigoCorHexaObra { get; set; }        
        public string ImagemBanner { get; set; }        
        public string CargoObraDiscord { get; set; }
        public string DiretorioImagemObra { get; set; }        
        public string StatusObraSlug { get; set; }        
        public string TipoObraSlug { get; set; }        
        public string NacionalidadeSlug { get; set; }
        public string ImagemCapaUltimoVolume { get; set; }
        public string NumeroUltimoVolume { get; set; }
        public string SlugUltimoVolume { get; set; }
        public string NumeroUltimoCapitulo { get; set; }
        public string SlugUltimoCapitulo { get; set; }
        public DateTime? DataAtualizacaoUltimoCapitulo { get; set; }
        public string Observacao { get; set; }
        public virtual List<VolumeComic> Volumes { get; set; }
        public virtual List<GeneroComic> GenerosComic { get; set; }

        public Comic()
        {
            Volumes = new List<VolumeComic>();
            GenerosComic = new List<GeneroComic>();
        }

        public void AdicionaComic(Guid id, string titulo, string tituloAlternativo, string alias, string autor, string artista, string ano, string slug,
            string usuarioInclusao, string usuarioAlteracao, string imagemCapaPrincipal, string sinopse, DateTime dataInclusao, DateTime dataAlteracao,
            bool ehObraMaiorIdade, bool ehRecomendacao, string codigoCorHexaObra, string imagemBanner, string cargoObraDiscord,
            string diretorioImagemObra, string statusObraSlug, string tipoObraSlug, string nacionalidadeSlug, string observacao)
        {
            Id = id;
            Titulo = titulo;
            TituloAlternativo = tituloAlternativo;
            Alias = alias;
            Autor = autor;
            Artista = artista;
            Ano = ano;
            Slug = slug;
            UsuarioInclusao = usuarioInclusao;
            UsuarioAlteracao = usuarioAlteracao;
            ImagemCapaPrincipal = imagemCapaPrincipal;
            Sinopse = sinopse;
            DataInclusao = dataInclusao;
            DataAlteracao = dataAlteracao;
            EhObraMaiorIdade = ehObraMaiorIdade;
            EhRecomendacao = ehRecomendacao;
            CodigoCorHexaObra = codigoCorHexaObra;
            ImagemBanner = imagemBanner;
            CargoObraDiscord = cargoObraDiscord;
            DiretorioImagemObra = diretorioImagemObra;
            StatusObraSlug = statusObraSlug;
            TipoObraSlug = tipoObraSlug;
            NacionalidadeSlug = nacionalidadeSlug;
            Observacao = observacao;
        }

       
        public void AtualizaDadosUltimoVolume(string imagemCapaUltimoVolume, string numeroUltimoVolume, string slugUltimoVolume)
        {
            ImagemCapaUltimoVolume = imagemCapaUltimoVolume;
            NumeroUltimoVolume = numeroUltimoVolume;
            SlugUltimoVolume = slugUltimoVolume;
        }

        public void AtualizaDadosUltimoCapitulo(string numeroUltimoCapitulo, string slugUltimoCapitulo, DateTime? dataAtualizacaoUltimoCapitulo)
        {
            NumeroUltimoCapitulo = numeroUltimoCapitulo;
            SlugUltimoCapitulo = slugUltimoCapitulo;
            DataAtualizacaoUltimoCapitulo = dataAtualizacaoUltimoCapitulo;
        }
    }
}
