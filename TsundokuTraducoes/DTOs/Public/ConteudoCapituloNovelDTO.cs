using System.Collections.Generic;

namespace TsundokuTraducoes.Api.DTOs.Public
{
    public class ConteudoCapituloNovelDTO
    {
        public int Id { get; set; }
        public string TituloCapitulo { get; set; }
        public string ConteudoCapitulo { get; set; }
        public string Tradutor { get; set; }
        public string Revisor { get; set; }
        public string Qc { get; set; }
        public string Editores { get; set; }
        public string SlugCapitulo { get; set; }
        public List<string> ListaSlugCapitulos { get; set; }
    }
}
