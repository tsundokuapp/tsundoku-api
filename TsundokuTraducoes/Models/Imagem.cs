using System.ComponentModel.DataAnnotations;

namespace TsundokuTraducoes.Api.Models
{
    public class Imagem
    {
        [Key]
        public int Id { get; set; }
        public int? IdObra { get; set; }
        public int? IdCapitulo { get; set; }
        public int? IdVolume { get; set; }
        public int? IdPost { get; set; }
        public string UrlImagem { get; set; }
    }
}
