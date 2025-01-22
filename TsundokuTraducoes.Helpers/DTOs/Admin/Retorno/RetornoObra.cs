using Newtonsoft.Json;

namespace TsundokuTraducoes.Helpers.DTOs.Admin.Retorno
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class RetornoObra
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string TituloAlternativo { get; set; }
        public string Alias { get; set; }
        public string Autor { get; set; }
        public string Artista { get; set; }
        public string Ano { get; set; }
        public string Slug { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public string ImagemCapaPrincipal { get; set; }
        public string Sinopse { get; set; }
        public string DataInclusao { get; set; }
        public string DataAlteracao { get; set; }
        public bool EhObraMaiorIdade { get; set; }
        public bool EhRecomendacao { get; set; }
        public string CodigoCorHexaObra { get; set; }
        public string CargoObraDiscord { get; set; }
        public string ImagemBanner { get; set; }
        public string StatusObra { get; set; }
        public string TipoObra { get; set; }
        public string Nacionalidade { get; set; }
        public List<RetornoGenero> Generos { get; set; }
        public string Observacao { get; set; }
        public string DiretorioImagemObra { get; set; }

        public RetornoObra()
        {
            Generos = new List<RetornoGenero>();
        }
    }
}


