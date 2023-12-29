using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Api.Utilidades;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Api.Services
{
    public class ObraServiceOld : IObraServiceOld
    {
        private readonly IMapper _mapper;
        private readonly IObraRepositoryOld _repository;
        private readonly IGeneroRepositoryOld _generoRepository;
        private readonly IImagemServiceOld _imagemService;

        public ObraServiceOld(IObraRepositoryOld repository, IGeneroRepositoryOld generoRepository, IMapper mapper, IImagemServiceOld imagemService)
        {
            _repository = repository;
            _generoRepository = generoRepository;
            _mapper = mapper;
            _imagemService = imagemService;
        }

        public async Task<Result<List<RetornoObra>>> RetornaListaObras()
        {
            var listaRetornoObras = new List<RetornoObra>();
            var listaNovels = await _repository.RetornaListaNovels();
            var listaComics = await _repository.RetornaListaComics();

            if (listaNovels.Count > 0)
            {
                foreach (var obra in listaNovels)
                {
                    listaRetornoObras.Add(await TrataRetornoNovel(obra));
                }
            }

            if (listaComics.Count > 0)
            {
                foreach (var comic in listaComics)
                {
                    listaRetornoObras.Add(await TrataRetornoComic(comic));
                }
            }

            return Result.Ok(listaRetornoObras);
        }


        public async Task<Result<RetornoObra>> RetornaNovelPorId(Guid id)
        {
            var novel = await _repository.RetornaNovelPorId(id);
            if (novel == null)
                return Result.Fail("Novel não encontrada!");

            var retornoNovel = await TrataRetornoNovel(novel);
            return Result.Ok().ToResult(retornoNovel);
        }

        public async Task<Result<RetornoObra>> RetornaComicPorId(Guid id)
        {
            var comic = await _repository.RetornaComicPorId(id);
            if (comic == null)
                return Result.Fail("Comic não encontrada!");

            var retornoComic = await TrataRetornoComic(comic);
            return Result.Ok().ToResult(retornoComic);
        }


        public async Task<Result<RetornoObra>> AdicionaNovel(ObraDTO obraDTO)
        {
            var novel = _mapper.Map<Novel>(obraDTO);
            var novelExistente = await _repository.RetornaNovelExistente(obraDTO.Titulo);

            if (novelExistente != null)
                return Result.Fail("Novel já postada!");

            if (!TratamentoDeStrings.ValidaCorHexaDecimal(obraDTO.CodigoCorHexaObra))
                return Result.Fail("Erro ao adicionar a Novel, código hexadecimal informada fora do padrão!");

            if (obraDTO.ImagemCapaPrincipalFile != null)
            {
                var result = _imagemService.ProcessaUploadCapaObra(obraDTO);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);
            }
            else
            {
                return Result.Fail("Erro ao adicionar a Obra, imagem da capa não enviada!");
            }

            if (obraDTO.ImagemBannerFile != null)
            {
                var result = _imagemService.ProcessaUploadBannerObra(obraDTO);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);
            }

            novel.DataAlteracao = novel.DataInclusao;
            novel.DiretorioImagemObra = obraDTO.DiretorioImagemObra;
            novel.ImagemCapaPrincipal = obraDTO.ImagemCapaPrincipal;
            novel.ImagemBanner = obraDTO.ImagemBanner;

            await _repository.AdicionaNovel(novel);
            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao adicionar a Novel!");

            await _repository.InsereGenerosNovel(obraDTO, novel, true);

            var retornoObra = await TrataRetornoNovel(novel);
            return Result.Ok().ToResult(retornoObra);
        }

        public async Task<Result<RetornoObra>> AdicionaComic(ObraDTO obraDTO)
        {
            var comic = _mapper.Map<Comic>(obraDTO);
            var comicExistente = await _repository.RetornaComicExistente(obraDTO.Titulo);

            if (comicExistente != null)
                return Result.Fail("Comic já postada!");

            if (!TratamentoDeStrings.ValidaCorHexaDecimal(obraDTO.CodigoCorHexaObra))
                return Result.Fail("Erro ao adicionar a Comic, código hexadecimal informada fora do padrão!");

            if (obraDTO.ImagemCapaPrincipalFile != null)
            {   
                var result = _imagemService.ProcessaUploadCapaObra(obraDTO);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);
            }
            else
            {
                return Result.Fail("Erro ao adicionar a Comic, imagem da capa não enviada!");
            }

            if (obraDTO.ImagemBannerFile != null)
            {                
                var result = _imagemService.ProcessaUploadBannerObra(obraDTO);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);
            }

            comic.DataAlteracao = comic.DataInclusao;
            comic.DiretorioImagemObra = obraDTO.DiretorioImagemObra;
            comic.ImagemCapaPrincipal = obraDTO.ImagemCapaPrincipal;
            comic.ImagemBanner = obraDTO.ImagemBanner;

            await _repository.AdicionaComic(comic);
            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao adicionar a Comic!");

            await _repository.InsereGenerosComic(obraDTO, comic, true);

            var retornoObra = await TrataRetornoComic(comic);
            return Result.Ok().ToResult(retornoObra);
        }


        public async Task<Result<RetornoObra>> AtualizaNovel(ObraDTO obraDTO)
        {
            var novelEncontrada = await _repository.RetornaNovelPorId(obraDTO.Id);
            if (novelEncontrada == null)
                return Result.Fail("Novel não encontrada!");

            if (!TratamentoDeStrings.ValidaCorHexaDecimal(obraDTO.CodigoCorHexaObra))
                return Result.Fail("Erro ao atualizar a Novel, código hexadecimal informada fora do padrão!");

            if (obraDTO.ImagemCapaPrincipalFile != null)
            {
                var result = _imagemService.ProcessaUploadCapaObra(obraDTO);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);
            }
            else
            {
                obraDTO.ImagemCapaPrincipal = novelEncontrada.ImagemCapaPrincipal;
            }

            if (obraDTO.ImagemBannerFile != null)
            {
                var result = _imagemService.ProcessaUploadBannerObra(obraDTO);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);
            }
            else
            {
                obraDTO.ImagemBanner = novelEncontrada.ImagemBanner;
            }

            novelEncontrada.ImagemCapaPrincipal = obraDTO.ImagemCapaPrincipal;
            novelEncontrada.ImagemBanner = obraDTO.ImagemBanner;
            novelEncontrada = _repository.AtualizaNovel(obraDTO);

            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao atualizar a Novel!");

            await _repository.InsereGenerosNovel(obraDTO, novelEncontrada, false);

            var retornoObra = await TrataRetornoNovel(novelEncontrada);
            return Result.Ok().ToResult(retornoObra);
        }

        public async Task<Result<RetornoObra>> AtualizaComic(ObraDTO obraDTO)
        {
            var comicEncontrada = await _repository.RetornaComicPorId(obraDTO.Id);
            if (comicEncontrada == null)
                return Result.Fail("Obra não encontrada!");

            if (!TratamentoDeStrings.ValidaCorHexaDecimal(obraDTO.CodigoCorHexaObra))
                return Result.Fail("Erro ao atualizar a Obra, código hexadecimal informada fora do padrão!");

            if (obraDTO.ImagemCapaPrincipalFile != null)
            {
                var result = _imagemService.ProcessaUploadCapaObra(obraDTO);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);
            }
            else
            {
                obraDTO.ImagemCapaPrincipal = comicEncontrada.ImagemCapaPrincipal;
            }

            if (obraDTO.ImagemBannerFile != null)
            {
                var result = _imagemService.ProcessaUploadBannerObra(obraDTO);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);
            }
            else
            {
                obraDTO.ImagemBanner = comicEncontrada.ImagemBanner;
            }

            comicEncontrada.ImagemCapaPrincipal = obraDTO.ImagemCapaPrincipal;
            comicEncontrada.ImagemBanner = obraDTO.ImagemBanner;
            comicEncontrada = _repository.AtualizaComic(obraDTO);

            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao atualizar a obra!");

            await _repository.InsereGenerosComic(obraDTO, comicEncontrada, false);

            var retornoObra = await TrataRetornoComic(comicEncontrada);
            return Result.Ok().ToResult(retornoObra);
        }


        public async Task<Result<bool>> ExcluiNovel(Guid idObra)
        {
            var novelEncontrada = await _repository.RetornaNovelPorId(idObra);
            if (novelEncontrada == null)
                return Result.Fail("Novel não encontrada!");

            _repository.ExcluiNovel(novelEncontrada);
            _imagemService.ExcluiDiretorioImagens(novelEncontrada.DiretorioImagemObra);

            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao excluir a Novel!");

            return Result.Ok().WithSuccess("Novel excluída com sucesso!");
        }

        public async Task<Result<bool>> ExcluiComic(Guid idObra)
        {
            var comicEncontrada = await _repository.RetornaComicPorId(idObra);
            if (comicEncontrada == null)
                return Result.Fail("Comic não encontrada!");

            _repository.ExcluiComic(comicEncontrada);
            _imagemService.ExcluiDiretorioImagens(comicEncontrada.DiretorioImagemObra);

            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao excluir a Comic!");

            return Result.Ok().WithSuccess("Comic excluída com sucesso!");
        }


        private async Task<RetornoObra> TrataRetornoNovel(Novel novel)
        {
            var retornoNovel = _mapper.Map<RetornoObra>(novel);
            retornoNovel.DataInclusao = novel.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoNovel.UsuarioAlteracao = !string.IsNullOrEmpty(novel.UsuarioAlteracao) ? novel.UsuarioAlteracao : "";
            retornoNovel.DataAlteracao = novel.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoNovel.Generos = await _generoRepository.CarregaListaGenerosNovel(novel.GenerosNovel);
            return retornoNovel;
        }

        private async Task<RetornoObra> TrataRetornoComic(Comic comic)
        {
            var retornoObra = _mapper.Map<RetornoObra>(comic);
            retornoObra.DataInclusao = comic.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoObra.UsuarioAlteracao = !string.IsNullOrEmpty(comic.UsuarioAlteracao) ? comic.UsuarioAlteracao : "";
            retornoObra.DataAlteracao = comic.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoObra.Generos = await _generoRepository.CarregaListaGenerosComic(comic.GenerosComic);
            return retornoObra;
        }

        
        // TODO - Talvez seja desconsiderado
        public async Task<Result<InformacaoObraDTO>> RetornaInformacaoObraDTO(Guid? idObra = null)
        {
            var informacaoObraDTO = new InformacaoObraDTO();
            var generos = await _generoRepository.RetornaListaGeneros();

            var retornoOk = generos != null;
            if (!retornoOk)
                return Result.Fail("Erro ao carregar os gêneros!");

            informacaoObraDTO.ListaGeneros.AddRange(generos);

            if (idObra != null)
            {
                var obra = await _repository.RetornaNovelPorId(idObra.Value);
                if (obra == null)
                    return Result.Fail("Obra não encontrada!");

                informacaoObraDTO.Novel = obra;
            }

            return Result.Ok(informacaoObraDTO);
        }
    }
}
