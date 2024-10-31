namespace TsundokuTraducoes.Helpers.DTOs.Public.Retorno
{
    public class RetornoCapitulos
    {
        public Guid IdVolume { get; set; }
        public Guid Id { get; set; }
        public string NumeroCapitulo { get; set; }
        public string ParteCapitulo { get; set; }
        public string SlugCapitulo { get; set; }
        public DateTime DataInclusao { get; set; }
        public string TituloCapitulo { get; set; }
    }
}