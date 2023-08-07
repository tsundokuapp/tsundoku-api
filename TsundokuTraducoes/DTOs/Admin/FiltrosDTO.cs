using System.Collections.Generic;

namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class FiltrosDTO
    {
        public List<string> ListaSlugGenero { get; set; }
        public List<string> ListaSlugNtatus { get; set; }
        public List<string> ListaSlugNacionalidade { get; set; }
    }
}
