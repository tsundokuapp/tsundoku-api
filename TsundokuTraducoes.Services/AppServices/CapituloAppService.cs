using AutoMapper;
using FluentResults;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{
    public class CapituloAppService : ICapituloAppService
    {
        private readonly IMapper _mapper;
        private readonly ICapituloService _capituloService;
        private readonly IVolumeService _volumeService;
        private readonly IObraService _obraService;
        private readonly IImagemAppService _imagemAppService;

        public CapituloAppService(IMapper mapper, ICapituloService capituloService, IVolumeService volumeService, IObraService obraService, IImagemAppService imagemAppService)
        {
            _mapper = mapper;
            _capituloService = capituloService;
            _volumeService = volumeService;
            _obraService = obraService;
            _imagemAppService = imagemAppService;
        }

        public Result<List<RetornoCapitulo>> RetornaListaCapitulos(Guid? volumeId = null)
        {
            var listaCapitulos = new List<RetornoCapitulo>();
            var capitulosNovel = RetornaListaCapitulosNovel(volumeId);
            var capitulosComic = RetornaListaCapitulosComic(volumeId);

            listaCapitulos.AddRange(capitulosNovel.Value);
            listaCapitulos.AddRange(capitulosComic.Value);

            return Result.Ok(listaCapitulos);
        }

        public Result<List<RetornoCapitulo>> RetornaListaCapitulosNovel(Guid? volumeId = null)
        {
            var listaCapitulos = new List<RetornoCapitulo>();
            var capitulosNovel = _capituloService.RetornaListaCapitulosNovel(volumeId);

            if (capitulosNovel.Count > 0)
            {
                foreach (var capitulo in capitulosNovel)
                {
                    listaCapitulos.Add(TrataRetornoCapituloNovel(capitulo));
                }
            }

            return Result.Ok(listaCapitulos);
        }

        public Result<List<RetornoCapitulo>> RetornaListaCapitulosComic(Guid? volumeId = null)
        {
            var listaCapitulos = new List<RetornoCapitulo>();
            var capitulosComic = _capituloService.RetornaListaCapitulosComic(volumeId);

            if (capitulosComic.Count > 0)
            {
                foreach (var capitulo in capitulosComic)
                {
                    listaCapitulos.Add(TrataRetornoCapituloComic(capitulo));
                }
            }

            return Result.Ok(listaCapitulos);
        }


        public Result<RetornoCapitulo> RetornaCapituloNovelPorId(Guid capituloId)
        {
            var capitulo = _capituloService.RetornaCapituloNovelPorId(capituloId);
            if (capitulo == null)
                return Result.Fail("Capitulo não encontrado!");

            var retornoCapitulo = TrataRetornoCapituloNovel(capitulo);
            return Result.Ok(retornoCapitulo);
        }

        public Result<RetornoCapitulo> RetornaCapituloComicPorId(Guid capituloId)
        {
            var capitulo = _capituloService.RetornaCapituloComicPorId(capituloId);
            if (capitulo == null)
                return Result.Fail("Capitulo não encontrado!");

            var retornoCapitulo = TrataRetornoCapituloComic(capitulo);
            return Result.Ok(retornoCapitulo);
        }


        public async Task<Result<RetornoCapitulo>> AdicionaCapituloNovel(CapituloDTO capituloDTO)
        {
            var volume = _volumeService.RetornaVolumeNovelPorId(capituloDTO.VolumeId);
            var novel = _obraService.RetornaNovelPorId(volume.NovelId);

            var capituloExistente = _capituloService.RetornaCapituloNovelExistente(capituloDTO);
            if (capituloExistente == null)
            {
                var capitulo = _mapper.Map<CapituloNovel>(capituloDTO);
                capitulo.DataInclusao = DateTime.Now;
                capitulo.DataAlteracao = capitulo.DataInclusao;
                
                var capituloAdicionado = await _capituloService.AdicionaCapituloNovel(capitulo);
                if (capituloAdicionado)
                {
                    if (capituloDTO.EhIlustracoesNovel)
                    {
                        if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
                        {
                            var result = _imagemAppService.ProcessaUploadListaImagensCapituloNovel(capituloDTO, volume);
                            if (result.IsFailed)
                                return Result.Fail(result.Errors[0].Message);

                            capitulo.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                            capitulo.ConteudoNovel = capituloDTO.ConteudoNovel;
                        }
                        else
                        {
                            return Result.Fail("Não foi encontrada a lista de imagens para o capítulo");
                        }
                    }

                    var obraDTO = _mapper.Map<ObraDTO>(novel);

                    obraDTO.NumeroUltimoCapitulo = TratamentoDeStrings.RetornaDescritivoCapitulo(capitulo.Numero, capitulo.Parte);
                    obraDTO.SlugUltimoCapitulo = capitulo.Slug;
                    obraDTO.DataAtualizacaoUltimoCapitulo = capitulo.DataInclusao;

                    _obraService.AtualizaNovel(obraDTO);

                    var novelAtualizada = await _obraService.AlteracoesSalvas();

                    if (!novelAtualizada)
                        return Result.Fail("Erro ao atualizar novel. Campos: NumeroUltimoCapitulo, SlugUltimoCapitulo, DataAtualizacaoUltimoCapitulo");

                    var retornoCapitulo = TrataRetornoCapituloNovel(capitulo);
                    return Result.Ok(retornoCapitulo);
                }

                return Result.Fail("Erro ao adicionar o capítulo!");
            }
            else
            {
                return Result.Fail("Capítulo já postado!");
            }
        }

        public async Task<Result<RetornoCapitulo>> AdicionaCapituloComic(CapituloDTO capituloDTO)
        {
            if (!ValidaDadosRequestCapitulo(capituloDTO))
                return Result.Fail("Verifique os campos obrigatórios e tente adicionar novamente!");

            var volume = _volumeService.RetornaVolumeComicPorId(capituloDTO.VolumeId);
            var comic = _obraService.RetornaComicPorId(volume.ComicId);

            var capituloExistente = _capituloService.RetornaCapituloComicExistente(capituloDTO);
            if (capituloExistente == null)
            {
                var capitulo = _mapper.Map<CapituloComic>(capituloDTO);
                capitulo.DataInclusao = DateTime.Now;
                capitulo.DataAlteracao = capitulo.DataInclusao;
                
                var capituloAdicionado = await _capituloService.AdicionaCapituloComic(capitulo);
                if (capituloAdicionado)
                {
                    if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
                    {
                        var result = _imagemAppService.ProcessaUploadListaImagensCapituloManga(capituloDTO, volume);
                        if (result.IsFailed)
                            return Result.Fail(result.Errors[0].Message);

                        capitulo.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                        capitulo.ListaImagens = capituloDTO.ListaImagemCapitulo;
                    }
                    else
                    {
                        return Result.Fail("Não foi encontrada a lista de imagens para o capítulo.");
                    }

                    var obraDTO = _mapper.Map<ObraDTO>(comic);

                    obraDTO.NumeroUltimoCapitulo = TratamentoDeStrings.RetornaDescritivoCapitulo(capitulo.Numero, capitulo.Parte);
                    obraDTO.SlugUltimoCapitulo = capitulo.Slug;
                    obraDTO.DataAtualizacaoUltimoCapitulo = capitulo.DataInclusao;

                    _obraService.AtualizaComic(obraDTO);

                    var novelAtualizada = await _obraService.AlteracoesSalvas();

                    if (!novelAtualizada)
                        return Result.Fail("Erro ao atualizar comic. Campos: NumeroUltimoCapitulo, SlugUltimoCapitulo, DataAtualizacaoUltimoCapitulo");

                    var retornoCapitulo = TrataRetornoCapituloComic(capitulo);
                    return Result.Ok(retornoCapitulo);
                }

                return Result.Fail("Erro ao adicionar o capítulo!");
            }
            else
            {
                return Result.Fail("Capítulo já postado!");
            }
        }


        public async Task<Result<RetornoCapitulo>> AtualizaCapituloNovel(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = _capituloService.RetornaCapituloNovelPorId(capituloDTO.Id);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            var volume = _volumeService.RetornaVolumeNovelPorId(capituloDTO.VolumeId);

            if (capituloDTO.EhIlustracoesNovel)
            {
                if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
                {
                    _imagemAppService.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
                    var result = _imagemAppService.ProcessaUploadListaImagensCapituloNovel(capituloDTO, volume);

                    if (result.IsFailed)
                        return Result.Fail(result.Errors[0].Message);

                    capituloEncontrado.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                    capituloEncontrado.ConteudoNovel = capituloDTO.ConteudoNovel;
                }
            }

            capituloEncontrado = _capituloService.AtualizaCapituloNovel(capituloDTO);

            var capituloAtualizado = await _capituloService.AlteracoesSalvas();
            if (!capituloAtualizado)
                return Result.Fail("Erro ao atualizar o capítulo!");

            var retornoCapitulo = TrataRetornoCapituloNovel(capituloEncontrado);
            return Result.Ok(retornoCapitulo);
        }

        public async Task<Result<RetornoCapitulo>> AtualizaCapituloComic(CapituloDTO capituloDTO)
        {
            if (!ValidaDadosRequestEdicaoCapitulo(capituloDTO))
                return Result.Fail("Verifique os campos obrigatórios e tente adicionar novamente!");

            var capituloEncontrado = _capituloService.RetornaCapituloComicPorId(capituloDTO.Id);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            var volume = _volumeService.RetornaVolumeComicPorId(capituloDTO.VolumeId);

            if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
            {
                _imagemAppService.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
                var result = _imagemAppService.ProcessaUploadListaImagensCapituloManga(capituloDTO, volume);

                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                capituloEncontrado.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                capituloEncontrado.ListaImagens = capituloDTO.ListaImagemCapitulo;
            }

            capituloEncontrado = _capituloService.AtualizaCapituloComic(capituloDTO);

            var capituloAtualizado = await _capituloService.AlteracoesSalvas();
            if (!capituloAtualizado)
                return Result.Fail("Erro ao atualizar o capítulo!");

            var retornoCapitulo = TrataRetornoCapituloComic(capituloEncontrado);
            return Result.Ok(retornoCapitulo);
        }


        public async Task<Result<bool>> ExcluiCapituloNovel(Guid capituloId)
        {
            var capituloEncontrado = _capituloService.RetornaCapituloNovelPorId(capituloId);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            var capituloExcluido = await _capituloService.ExcluiCapituloNovel(capituloEncontrado);

            if (capituloEncontrado.EhIlustracoesNovel)
            {
                _imagemAppService.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
            }

            if (!capituloExcluido)
                return Result.Fail("Erro ao excluir o capítulo!");

            return Result.Ok().WithSuccess("Capítulo excluído com sucesso!");
        }

        public async Task<Result<bool>> ExcluiCapituloComic(Guid capituloId)
        {
            var capituloEncontrado = _capituloService.RetornaCapituloComicPorId(capituloId);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            var capituloExcluido = await  _capituloService.ExcluiCapituloComic(capituloEncontrado);
            _imagemAppService.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);

            if (!capituloExcluido)
                return Result.Fail("Erro ao excluir o capítulo!");

            return Result.Ok().WithSuccess("Capítulo excluído com sucesso!");
        }


        private RetornoCapitulo TrataRetornoCapituloNovel(CapituloNovel capituloNovel)
        {
            var retornoCapitulo = _mapper.Map<RetornoCapitulo>(capituloNovel);
            retornoCapitulo.DataInclusao = capituloNovel.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.DataAlteracao = capituloNovel.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.UsuarioAlteracao = !string.IsNullOrEmpty(capituloNovel.UsuarioAlteracao) ? capituloNovel.UsuarioAlteracao : null;
            return retornoCapitulo;
        }

        private RetornoCapitulo TrataRetornoCapituloComic(CapituloComic CapituloComic)
        {
            var retornoCapitulo = _mapper.Map<RetornoCapitulo>(CapituloComic);
            retornoCapitulo.DataInclusao = CapituloComic.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.DataAlteracao = CapituloComic.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.UsuarioAlteracao = !string.IsNullOrEmpty(CapituloComic.UsuarioAlteracao) ? CapituloComic.UsuarioAlteracao : null;
            return retornoCapitulo;
        }

        private bool ValidaDadosRequestCapitulo(CapituloDTO capituloDTO)
        {
            var resquestValido =
                VerificaString(capituloDTO.Numero) &&
                VerificaString(capituloDTO.UsuarioInclusao) &&
                capituloDTO.OrdemCapitulo > 0 &&
                capituloDTO.VolumeId.ToString() != "00000000-0000-0000-0000-000000000000" &&
                capituloDTO.ListaImagensForm != null &&
                capituloDTO.ListaImagensForm.Count > 0;

            return resquestValido;
        }

        private bool ValidaDadosRequestEdicaoCapitulo(CapituloDTO capituloDTO)
        {
            var resquestValido =
                VerificaString(capituloDTO.Numero) &&
                VerificaString(capituloDTO.UsuarioInclusao) &&
                capituloDTO.OrdemCapitulo > 0 &&
                capituloDTO.VolumeId.ToString() != "00000000-0000-0000-0000-000000000000" &&
                VerificaString(capituloDTO.UsuarioAlteracao);

            return resquestValido;
        }

        private static bool VerificaString(string valor)
        {
            return !string.IsNullOrEmpty(valor) && !valor.Contains("\"\""); ;
        }
    }
}
