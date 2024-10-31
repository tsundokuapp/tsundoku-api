using Newtonsoft.Json;

namespace TsundokuTraducoes.Helpers.DTOs.Public.Retorno
{
    public class RetornoCapitulosHome
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UrlCapaVolume { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UrlCapaPrincipal { get; set; }

        public string NumeroCapitulo { get; set; }
        public string ParteCapitulo { get; set; }
        public string SlugCapitulo { get; set; }
        public DateTime DataInclusao { get; set; }
        public string NumeroVolume { get; set; }
        public string UrlCapa { get; set; }
        public string AliasObra { get; set; }
        public string AutorObra { get; set; }
    }
}
