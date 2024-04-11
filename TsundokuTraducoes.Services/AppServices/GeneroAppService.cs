using AutoMapper;
using FluentResults;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{
    public class GeneroAppService : IGeneroAppService
    {
        private readonly IMapper _mapper;
        private readonly IGeneroService _generoService;

        public GeneroAppService(IGeneroService generoService, IMapper mapper)
        {
            _generoService = generoService;
            _mapper = mapper;
        }

        public async Task<Result<List<RetornoGenero>>> RetornaListaGeneros()
        {
            var listaRetornoGenero = new List<RetornoGenero>();
            var listaGenero = await _generoService.RetornaListaGeneros();

            foreach (var genero in listaGenero)
            {
                var retornoGenero = TrataRetornoGenero(genero);
                listaRetornoGenero.Add(retornoGenero);
            }

            return Result.Ok(listaRetornoGenero);
        }

        public async Task<Result<RetornoGenero>> RetornaGeneroPorId(Guid id)
        {
            var genero = await _generoService.RetornaGeneroPorId(id);
            if (genero == null)
                return Result.Fail("Gênero não encontrado!");

            var retornoGenero = TrataRetornoGenero(genero);
            return Result.Ok(retornoGenero);
        }

        public async Task<Result<RetornoGenero>> AdicionaGenero(GeneroDTO generoDTO)
        {
            var generoExistente = await _generoService.RetornaGeneroExistente(generoDTO.Slug);
            if (generoExistente != null)
                return Result.Fail("Gênero já postado!");

            var genero = _mapper.Map<Genero>(generoDTO);
            genero.DataInclusao = DateTime.Now;
            genero.DataAlteracao = genero.DataInclusao;

            var generoCriado = await _generoService.AdicionaGenero(genero);
            if (!generoCriado)
                return Result.Fail("Erro ao adicionar o Gênero!");

            var retornoGenero = TrataRetornoGenero(genero);
            return Result.Ok().ToResult(retornoGenero);
        }

        public async Task<Result<RetornoGenero>> AtualizaGenero(GeneroDTO generoDTO)
        {
            var generoEncontrada = await _generoService.RetornaGeneroPorId(generoDTO.Id);
            if (generoEncontrada == null)
                return Result.Fail("Gênero não encontrado!");

            generoEncontrada = _generoService.AtualizaGenero(generoDTO);

            var generoAtualizado = await _generoService.AlteracoesSalvas();
            if (!generoAtualizado)
                return Result.Fail("Erro ao atualizar o Gênero!");

            var retornoGenero = TrataRetornoGenero(generoEncontrada);
            return Result.Ok(retornoGenero);
        }

        public async Task<Result<bool>> ExcluiGenero(Guid id)
        {
            var generoEncontrado = await _generoService.RetornaGeneroPorId(id);
            if (generoEncontrado == null)
                return Result.Fail("Gênero não encontrado!");

            if (generoEncontrado.GenerosComic.Count > 0 || generoEncontrado.GenerosNovel.Count > 0)
                return Result.Fail("Erro ao excluir o Gênero, já relacionado a alguma obra!");

            var generoExcluido = await _generoService.ExcluiGenero(generoEncontrado);
            if (!generoExcluido)
                return Result.Fail("Erro ao excluir o Gênero!");

            return Result.Ok().WithSuccess("Gênero excluído com sucesso!");
        }

        private RetornoGenero TrataRetornoGenero(Genero genero)
        {
            var retornoGenero = _mapper.Map<RetornoGenero>(genero);
            retornoGenero.DataInclusao = genero.DataInclusao?.ToString("dd/MM/yyyy HH:mm:ss");
            retornoGenero.UsuarioAlteracao = !string.IsNullOrEmpty(genero.UsuarioAlteracao) ? genero.UsuarioAlteracao : "";
            retornoGenero.DataAlteracao = genero.DataAlteracao?.ToString("dd/MM/yyyy HH:mm:ss");            
            return retornoGenero;
        }
    }
}