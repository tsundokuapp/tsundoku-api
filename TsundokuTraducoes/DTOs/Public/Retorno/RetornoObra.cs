using System;

namespace TsundokuTraducoes.Api.DTOs.Public.Retorno
{
    public class RetornoObra
    {
        public string ImagemCapaVolume { get; set; }
        public string AliasObra { get; set; }        
        public string AutorObra { get; set; }
        public string DescritivoVolume { get; set; }
        public string SlugObra { get; set; }
        public Guid Id { get; set; }
    }
}
