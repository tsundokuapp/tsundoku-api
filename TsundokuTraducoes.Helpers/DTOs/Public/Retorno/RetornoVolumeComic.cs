namespace TsundokuTraducoes.Helpers.DTOs.Public.Retorno
{
    public class RetornoVolumeComic
    {
        public Guid IdObra { get; set; }
        public Guid Id { get; set; }
        public string NumeroVolume { get; set; }
        public string UrlCapaVolume { get; set; }
        public string SlugVolume { get; set; }
        public string Sinopse { get; set; }
        public int OrdemVolume { get; set; }
        public bool Publicado { get; set; }
        public string Titulo { get; set; }
        public List<RetornoCapituloComic> ListaRetornoCapitulosComic { get; set; }
    }
}
