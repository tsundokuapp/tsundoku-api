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
        private readonly IGeneroDeParaAppService _generoDeParaAppService;

        public ObrasAppService(IObrasService obrasService, IMapper mapper, IGeneroDeParaAppService generoDeParaAppService)
        {
            _obrasService = obrasService;
            _mapper = mapper;
            _generoDeParaAppService = generoDeParaAppService;
        }

        public async Task<List<RetornoObras>> ObterListaNovels(RequestObras requestObras)
        {
            var listaRetornoObra = await _obrasService.ObterListaNovels(requestObras);
            return listaRetornoObra;
        }

        public async Task<List<RetornoObras>> ObterListaComics(RequestObras requestObras)
        {
            var listaRetornoObra = await _obrasService.ObterListaComics(requestObras);
            return listaRetornoObra;
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


        public async Task<RetornoNovel> ObterNovelPorId(Guid id)
        {
            var retornoNovel = await _obrasService.ObterNovelPorId(id);
            return retornoNovel;
        }
        
        public async Task<Result<RetornoAppNovel>> ObterNovelPorSlug(string slug)
        {
            var novel = await _obrasService.ObterNovelPorSlug(slug);
            if (novel == null)
                return Result.Fail<RetornoAppNovel>("Novel não encontrado");
            
            var retornoNovel = await TrataGenerosNovel(novel);
            return Result.Ok().ToResult(retornoNovel);
        }

        public async Task<RetornoComic> ObterComicPorId(Guid id)
        {
            var comic = await _obrasService.ObterComicPorId(id);
            return comic;
        }
        
        public async Task<RetornoComic> ObterComicPorSlug(string slug)
        {
            var comic = await _obrasService.ObterComicPorSlug(slug);
            return comic;
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
        
        private async Task<RetornoAppNovel> TrataGenerosNovel(RetornoNovel novel)
        {
            var retornoNovel = _mapper.Map<RetornoAppNovel>(novel);
            retornoNovel.ListaGeneros = await _generoDeParaAppService.CarregaListaGenerosNovel(novel.Generos);
            return retornoNovel;
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

        public RetornoObras TrataRetornoNovel(Novel obra)
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
                ListaGeneros = TrataRetornoListaGeneros(obra.GenerosNovel),
                Publicado = obra.Publicado
            };
        }

        public static List<string> TrataRetornoListaGeneros(List<GeneroNovel> generosNovel)
        {
            var listaGeneros = new List<string>();

            generosNovel.ForEach((genero) =>
            {
                listaGeneros.Add(genero.Genero.Descricao);
            });

            return listaGeneros;
        }
    }
}
