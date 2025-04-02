using AutoMapper;
using FluentResults;
using Newtonsoft.Json;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{
    public class ObrasAppService : IObrasAppService
    {
        private readonly IObrasService _obrasService;
        private readonly IMapper _mapper;

        public ObrasAppService(IObrasService obrasService, IMapper mapper, IGeneroDeParaAppService generoDeParaAppService)
        {
            _obrasService = obrasService;
            _mapper = mapper;
        }

        public async Task<List<RetornoNovel>> ObterListaNovels(RequestObras requestObras)
        {
            var listaRetornoNovel = new List<RetornoNovel>();
            var listaNovel = await _obrasService.ObterListaNovels(requestObras);

            foreach (var novel in listaNovel)
            {
                listaRetornoNovel.Add(TrataRetornoNovelUnica(novel));
            }

            return listaRetornoNovel;
        }

        public async Task<List<RetornoComic>> ObterListaComics(RequestObras requestObras)
        {
            var listaRetornoComic = new List<RetornoComic>();
            var listaComic = await _obrasService.ObterListaComics(requestObras);

            foreach (var comic in listaComic)
            {
                listaRetornoComic.Add(TrataRetornoComicUnica(comic));
            }

            return listaRetornoComic;
        }


        public async Task<List<RetornoObras>> ObterListaNovelsRecentes()
        {
            var listaRetornoObra = await _obrasService.ObterListaNovelsRecentes();
            return listaRetornoObra;
        }

        public async Task<List<RetornoObras>> ObterListaComicsRecentes()
        {
            var listaRetornoObra = await _obrasService.ObterListaComicsRecentes();
            return listaRetornoObra;
        }


        public async Task<Result<RetornoNovel>> ObterNovelPorId(Guid id)
        {
            var novel = await _obrasService.ObterNovelPorId(id);
            if (novel is null)
                return Result.Fail<RetornoNovel>("Novel não encontrada");

            return Result.Ok().ToResult(TrataRetornoNovelUnica(novel));
        }
        
        public async Task<Result<RetornoNovel>> ObterNovelPorSlug(string slug)
        {
            var novel = await _obrasService.ObterNovelPorSlug(slug);
            if (novel is null)
                return Result.Fail<RetornoNovel>("Novel não encontrada");
                        
            return Result.Ok().ToResult(TrataRetornoNovelUnica(novel));
        }

        public async Task<Result<RetornoComic>> ObterComicPorId(Guid id)
        {
            var comic = await _obrasService.ObterComicPorId(id);
            if (comic is null)
                return Result.Fail<RetornoComic>("Comic não encontrada");

            return Result.Ok().ToResult(TrataRetornoComicUnica(comic)); 
        }
        
        public async Task<Result<RetornoComic>> ObterComicPorSlug(string slug)
        {
            var comic = await _obrasService.ObterComicPorSlug(slug);
            if (comic is null)
                return Result.Fail<RetornoComic>("Comic não encontrada");

            return Result.Ok().ToResult(TrataRetornoComicUnica(comic));
        }

        public async Task<List<RetornoCapitulosHome>> ObterCapitulosHome()
        {
            return await _obrasService.ObterCapitulosHome();
        }

        public async Task<List<RetornoObrasRecomendadas>> ObterObrasRecomendadas()
        {
            var listaNovelsRecomendadas = await _obrasService.ObterListaNovelsRecomendadas();
            var listaComicsRecomendadas = await _obrasService.ObterListaComicsRecomendadas();

            var listaObrasRecomendadasTratadas = TratamentoRetornoObrasRecomendadas(listaNovelsRecomendadas, listaComicsRecomendadas);

            return listaObrasRecomendadasTratadas;
        }

        public List<RetornoVolumes> ObterListaVolumeCapitulos(RequestObras requestObras)
        {
            return _obrasService.ObterListaVolumeCapitulos(requestObras.IdObra);
        }

        public async Task<RetornoCapituloComic> ObterCapituloComicPorId(Guid id)
        {
            var capituloComic = await _obrasService.ObterCapituloComicPorId(id);
            if (capituloComic == null)
                return null;

            return TrataRetornoCapituloComic(capituloComic);
        }

        public async Task<RetornoCapituloNovel> ObterCapituloNovelPorId(Guid id)
        {
            var capituloNovel = await _obrasService.ObterCapituloNovelPorId(id);
            if (capituloNovel == null)
                return null;

            return TrataRetornoCapituloNovel(capituloNovel);
        }


        private RetornoCapituloComic TrataRetornoCapituloComic(CapituloComic CapituloComic)
        {
            var retornoCapitulo = _mapper.Map<RetornoCapituloComic>(CapituloComic);
            retornoCapitulo.DataInclusao = CapituloComic.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.DataAlteracao = CapituloComic.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.UsuarioAlteracao = !string.IsNullOrEmpty(CapituloComic.UsuarioAlteracao) ? CapituloComic.UsuarioAlteracao : null;

            if (!string.IsNullOrEmpty(CapituloComic.ListaImagensJson))
            {
                retornoCapitulo.ListaImagens = JsonConvert.DeserializeObject<List<EnderecoImagemDTO>>(CapituloComic.ListaImagensJson);
            }

            return retornoCapitulo;
        }

        private RetornoCapituloNovel TrataRetornoCapituloNovel(CapituloNovel capituloNovel)
        {
            var retornoCapitulo = _mapper.Map<RetornoCapituloNovel>(capituloNovel);
            retornoCapitulo.DataInclusao = capituloNovel.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.DataAlteracao = capituloNovel.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.UsuarioAlteracao = !string.IsNullOrEmpty(capituloNovel.UsuarioAlteracao) ? capituloNovel.UsuarioAlteracao : null;

            if (capituloNovel.EhIlustracoesNovel && !string.IsNullOrEmpty(capituloNovel.ListaImagensJson))
            {
                retornoCapitulo.ListaImagens = JsonConvert.DeserializeObject<List<EnderecoImagemDTO>>(capituloNovel.ListaImagensJson);
            }

            return retornoCapitulo;
        }
       
        public List<RetornoObrasRecomendadas> TratamentoRetornoObrasRecomendadas(List<Novel> listaNovelsRecomendadas, List<Comic> listaComicsRecomendadas)
        {
            var query = (from comics in listaComicsRecomendadas
                         select new
                         {
                             Titulo = comics.Alias,
                             Capa = !string.IsNullOrEmpty(comics.ImagemCapaUltimoVolume) ? comics.ImagemCapaUltimoVolume : comics.ImagemCapaPrincipal,
                             SlugObra = comics.Slug,
                             comics.Sinopse,
                             comics.TipoObra
                         })
                        .Union(from novels in listaNovelsRecomendadas
                               select new
                               {
                                   Titulo = novels.Alias,
                                   Capa = !string.IsNullOrEmpty(novels.ImagemCapaUltimoVolume) ? novels.ImagemCapaUltimoVolume : novels.ImagemCapaPrincipal,
                                   SlugObra = novels.Slug,
                                   novels.Sinopse,
                                   novels.TipoObra
                               }
                        );

            var listaRetornoObrasRecomendadas = query
                .Select(ror => new RetornoObrasRecomendadas
                {
                    Titulo = ror.Titulo,
                    Capa = ror.Capa,
                    SlugObra = ror.SlugObra,
                    Sinopse = ror.Sinopse,
                    TipoObra = ror.TipoObra
                })
                .Take(6)
                .ToList();

            return listaRetornoObrasRecomendadas;
        }

        public List<RetornoObras> TrataListaRetornoNovel(List<Novel> listaNovels)
        {
            var listaRetornoObra = new List<RetornoObras>();

            foreach (var obra in listaNovels)
            {
                listaRetornoObra.Add(TrataRetornoNovel(obra));
            }

            return listaRetornoObra;
        }

        public static RetornoObras TrataRetornoNovel(Novel obra)
        {
            return new RetornoObras
            {
                UrlCapa = !string.IsNullOrEmpty(obra.ImagemCapaUltimoVolume)
                ? obra.ImagemCapaUltimoVolume
                : obra.ImagemCapaPrincipal,

                Titulo = obra.Titulo,
                TipoObra = obra.TipoObra,
                Alias = obra.Alias,
                Autor = obra.Autor,
                DescritivoVolume = obra.NumeroUltimoVolume,
                Slug = obra.Slug,
                Id = obra.Id,
                TituloAlternativo = obra.TituloAlternativo,
                StatusObra = obra.StatusObra,
                ListaGeneros = TrataRetornoListaGenerosNovel(obra.GenerosNovel),
                Publicado = obra.Publicado
            };
        }        

        public RetornoNovel TrataRetornoNovelUnica(Novel obra)
        {
            return new RetornoNovel()
            {
                UrlCapa = !string.IsNullOrEmpty(obra.ImagemCapaUltimoVolume)
                    ? obra.ImagemCapaUltimoVolume
                    : obra.ImagemCapaPrincipal,

                UrlBanner = !string.IsNullOrEmpty(obra.ImagemBanner) ? obra.ImagemBanner : !string.IsNullOrEmpty(obra.ImagemCapaUltimoVolume)
                                                                                            ? obra.ImagemCapaUltimoVolume
                                                                                            : obra.ImagemCapaPrincipal,

                Titulo = obra.Titulo,
                TituloAlternativo = obra.TituloAlternativo,
                TipoObra = obra.TipoObra,
                Alias = obra.Alias,
                Autor = obra.Autor,
                Artista = obra.Artista,
                Ano = obra.Ano,
                Visualizacoes = obra.Visualizacoes.ToString(),
                Sinopse = obra.Sinopse,
                EhRecomdacao = obra.EhRecomendacao,
                EhObraMaiorIdade = obra.EhObraMaiorIdade,
                DescritivoVolume = obra.NumeroUltimoVolume,
                Slug = obra.Slug,
                Id = obra.Id,
                Nacionalidade = obra.Nacionalidade,
                StatusObra = obra.StatusObra,
                Observacao = obra.Observacao,
                ListaGeneros = TrataRetornoListaGenerosNovel(obra.GenerosNovel),
            };
        }

        public RetornoComic TrataRetornoComicUnica(Comic obra)
        {
            return new RetornoComic()
            {
                UrlCapa = !string.IsNullOrEmpty(obra.ImagemCapaUltimoVolume)
                    ? obra.ImagemCapaUltimoVolume
                    : obra.ImagemCapaPrincipal,

                UrlBanner = !string.IsNullOrEmpty(obra.ImagemBanner) ? obra.ImagemBanner : !string.IsNullOrEmpty(obra.ImagemCapaUltimoVolume)
                                                                                            ? obra.ImagemCapaUltimoVolume
                                                                                            : obra.ImagemCapaPrincipal,

                Titulo = obra.Titulo,
                TituloAlternativo = obra.TituloAlternativo,
                TipoObra = obra.TipoObra,
                Alias = obra.Alias,
                Autor = obra.Autor,
                Artista = obra.Artista,
                Ano = obra.Ano,
                Visualizacoes = obra.Visualizacoes.ToString(),
                Sinopse = obra.Sinopse,
                EhRecomdacao = obra.EhRecomendacao,
                EhObraMaiorIdade = obra.EhObraMaiorIdade,
                DescritivoVolume = obra.NumeroUltimoVolume,
                Slug = obra.Slug,
                Id = obra.Id,
                Nacionalidade = obra.Nacionalidade,
                StatusObra = obra.StatusObra,
                Observacao = obra.Observacao,
                ListaGeneros = TrataRetornoListaGenerosNovel(obra.GenerosComic)
            };
        }

        public static List<string> TrataRetornoListaGenerosNovel(List<GeneroNovel> generosNovel)
        {
            var listaGeneros = new List<string>();

            generosNovel.ForEach((genero) =>
            {
                listaGeneros.Add(genero.Genero.Descricao);
            });

            return listaGeneros;
        }

        public static List<string> TrataRetornoListaGenerosNovel(List<GeneroComic> generosComic)
        {
            var listaGeneros = new List<string>();

            generosComic.ForEach((genero) =>
            {
                listaGeneros.Add(genero.Genero.Descricao);
            });

            return listaGeneros;
        }
    }
}
