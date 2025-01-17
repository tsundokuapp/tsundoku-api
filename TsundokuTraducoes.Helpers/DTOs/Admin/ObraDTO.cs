using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TsundokuTraducoes.Helpers.DTOs.Admin
{
    public class ObraDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Alias { get; set; }
        [Required]
        public string TituloAlternativo { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public string Artista { get; set; }
        [Required]
        public string Ano { get; set; }
        public string Slug { get { return TratamentoDeStrings.RetornaStringSlug(Titulo); } }
        [Required]         
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }        
        public string Sinopse { get; set; }
        public bool EhObraMaiorIdade { get; set; }
        public bool EhRecomendacao { get; set; }
        [Required]
        public List<string> ListaGeneros { get; set; }
        public string CodigoCorHexaObra { get; set; }
        public string StatusObra { get; set; }
        public string TipoObra { get; set; }
        public string Nacionalidade { get; set; }
        public string CargoObraDiscord { get { return string.Concat("@", Titulo); } }
        public string DiretorioImagemObra { get; set; }
        public string ImagemCapaPrincipal { get; set; }
        public string ImagemBanner { get; set; }
        public IFormFile ImagemCapaPrincipalFile { get; set; }
        public IFormFile ImagemBannerFile { get; set; }
        public string ImagemCapaUltimoVolume { get; set; }
        public string NumeroUltimoVolume { get; set; }
        public string SlugUltimoVolume { get; set; }
        public string NumeroUltimoCapitulo { get; set; }
        public string SlugUltimoCapitulo { get; set; }
        public string Observacao { get; set; }
        public DateTime? DataAtualizacaoUltimoCapitulo { get; set; }
        [Required]
        public bool OtimizarImagem {  get; set; }
        public bool SalvarLocal { get; set; }
    }
}
