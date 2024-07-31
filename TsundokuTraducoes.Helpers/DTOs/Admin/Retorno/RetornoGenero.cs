namespace TsundokuTraducoes.Helpers.DTOs.Admin.Retorno
{
    public class RetornoGenero
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Slug { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public string DataInclusao { get; set; }
        public string DataAlteracao { get; set; }
    }

    public class RetornoGeneroNovel : RetornoGenero { };
    public class RetornoGeneroComic : RetornoGenero { };
}
