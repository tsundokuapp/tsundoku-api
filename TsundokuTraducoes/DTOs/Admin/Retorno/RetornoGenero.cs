using System;

namespace TsundokuTraducoes.Api.DTOs.Admin.Retorno
{
    public class RetornoGenero
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Slug { get; set; }
    }

    public class RetornoGeneroNovel : RetornoGenero { };
    public class RetornoGeneroComic : RetornoGenero { };
}
