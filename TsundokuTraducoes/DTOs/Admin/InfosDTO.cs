using System.Collections.Generic;

namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class InfosDTO
    {
        public List<InfosObraDTO> ListaInfosObraDTO { get; set; }
        public List<InfosObraDTO> ListaFiltrosDTO { get; set; }
    }
}
