namespace TsundokuTraducoes.Helpers.DTOs.Admin.Request
{
    public class RequestCapitulo
    {
        public Guid? volumeId { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
