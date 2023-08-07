using System.Collections.Generic;

namespace TsundokuTraducoes.Api.DTOs.Public
{
    public class VolumeDTO
    {
        public string ImagemCapaVolume { get; set; }
        public string SinopseVolume { get; set; } = "";
        public string SlugVolume { get; set; }
        public string VolumeNumero { get; set; }
        public List<CapituloDTO> CapitulosDTO { get; set; }
        public VolumeDTO()
        {
            CapitulosDTO = new List<CapituloDTO>();
        }
    }
}
