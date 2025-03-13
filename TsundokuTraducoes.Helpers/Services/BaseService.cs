using TsundokuTraducoes.Helpers.Services.Interfaces;

namespace TsundokuTraducoes.Helpers.Services
{
    public class BaseService<TService>(TService service) : IBaseService<TService>
    {
        protected readonly TService _service = service;
    }

    public class BaseService<TService, TService2>(TService service, TService2 service2) : IBaseService<TService, TService2>
    {
        protected readonly TService _service = service;
        protected readonly TService2 _service2 = service2;
    }
}