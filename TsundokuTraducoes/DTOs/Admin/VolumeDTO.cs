using Microsoft.AspNetCore.Http;
using System;
using TsundokuTraducoes.Api.Utilidades;

namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class VolumeDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Numero { get; set; }
        public string DescritivoVolume { get; set; }
        public string Sinopse { get; set; }
        public string Slug { get { return TratamentoDeStrings.RetornaStringSlug($"volume {Numero}"); } }
        public string UsuarioCadastro { get; set; }
        public string UsuarioAlteracao { get; set; }
        public int ObraId { get; set; }
        public string ImagemCapaVolume { get; set; }
        public IFormFile ImagemCapaVolumeFile { get; set; }
    }
}
