﻿using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IObrasRepository
    {
        Task<List<RetornoObra>> ObterListaNovels(RequestObras requestObras);
        Task<List<RetornoObra>> ObterListaNovelsRecentes();
        Task<RetornoObra> ObterNovelPorId(RequestObras requestObras);
    }
}
