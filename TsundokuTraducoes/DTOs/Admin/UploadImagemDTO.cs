using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class UploadImagemDTO
    {
        public IFormFile ImagemCapa { get; set; }
        public List<IFormFile> ListaImagensCapitulo { get; set; }
    }
}
