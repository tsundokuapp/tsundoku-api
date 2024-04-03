namespace TsundokuTraducoes.Helpers.DTOs.Admin
{
    public class GeneroDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Slug { get { return TratamentoDeStrings.RetornaStringSlug(Descricao); } }
    }
}