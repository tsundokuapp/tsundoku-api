using Newtonsoft.Json;

namespace TsundokuTraducoes.Helpers.DTOs.Admin.Retorno
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class RetornoVolume
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Numero { get; set; }
        public string Sinopse { get; set; }
        public string ImagemVolume { get; set; }
        public string Slug { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public string DataInclusao { get; set; }
        public string DataAlteracao { get; set; }
        public Guid? NovelId { get; set; }
        public Guid? ComicId { get; set; }
        public string DescritivoTituloNumeroVolume => TratamentoDeStrings.RetornaDescritivoVolume(Numero);
    }
 }