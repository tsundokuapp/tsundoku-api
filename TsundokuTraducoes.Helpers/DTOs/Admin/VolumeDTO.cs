using Microsoft.AspNetCore.Http;

namespace TsundokuTraducoes.Helpers.DTOs.Admin
{
    public class VolumeDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Numero { get; set; }
        public string Sinopse { get; set; }
        public string Slug { get { return TratamentoDeStrings.RetornaStringSlug($"volume {Numero}"); } }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public Guid ObraId { get; set; }
        public string DiretorioImagemVolume { get; set; }
        public string ImagemVolume { get; set; }
        public IFormFile ImagemVolumeFile { get; set; }
        public Guid NovelId { get { return ObraId; } }
        public Guid ComicId { get { return ObraId; } }
        public bool OtimizarImagem { get; set; }
        public bool SalvarLocal { get; set; }
    }
}
