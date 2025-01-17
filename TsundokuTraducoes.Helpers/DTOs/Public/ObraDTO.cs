namespace TsundokuTraducoes.Helpers.DTOs.Public
{
    public class ObraDTO
    {   
        public string ImagemCapaPrincipal { get; set; }
        public string Titulo { get; set; }
        public string TituloAlternativo { get; set; }
        public string SinopseObra { get; set; }
        public string Autor { get; set; }
        public string Ilustrador { get; set; }
        public string SlugObra { get; set; }
        public string SlugStatus { get; set; }
        public string StatusObra { get; set; }
        public string SlugTipoObra { get; set; }
        public string TipoObra { get; set; }

        public List<GeneroDTO> Generos { get; set; }
        public List<VolumeDTO> Volumes { get; set; }

        public ObraDTO()
        {
            Generos = new List<GeneroDTO>();
            Volumes = new List<VolumeDTO>();
        }
    }
}
