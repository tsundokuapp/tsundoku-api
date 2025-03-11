using TsundokuTraducoes.Helpers.Services.Interfaces;

namespace TsundokuTraducoes.Helpers.Services
{
    public class BaseService<TService>(TService service) : IBaseService<TService>
    {
        protected readonly TService _service = service;
    }
}
