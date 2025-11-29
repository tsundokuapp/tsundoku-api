using System.Net;

namespace TsundokuTraducoes.Api.Helpers;

public class ErrorResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
}
