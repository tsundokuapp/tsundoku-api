using System.Collections.Generic;

namespace TsundokuTraducoes.Api.DTOs.Public
{
    public class ConteudoCapituloComicDTO
    {
        public int Id { get; set; }
        public string TituloCapitulo { get; set; }
        public string ListaImagensComic { get; set; }        
        public string SlugCapitulo { get; set; }
        public List<string> ListaSlugCapitulos { get; set; }
    }
}
