namespace TsundokuTraducoes.Api.DTOs.Admin.Request
{
    public class RequestObras
    {
        public string Pesquisar {  get; set; }
        public string Nacionalidade { get; set; }
        public string Status { get; set; }
        public string Tipo { get; set; }
        public string Genero { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string IdObra { get; set; }
        public string IdCapitulo { get; set; }
    }
}
