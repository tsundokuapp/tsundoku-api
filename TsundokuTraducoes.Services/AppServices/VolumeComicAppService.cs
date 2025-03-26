using AutoMapper;
using FluentResults;
using Newtonsoft.Json;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Helpers.Services.Interfaces;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{
    public class VolumeComicAppService(IVolumeComicService service, IObraService obraservice, IMapper mapper) 
        : IVolumeComicAppService, IBaseService<IVolumeComicService, IObraService, IMapper>
    {
        public async Task<Result<List<RetornoVolumeComic>>> ObterVolumesComicPorIdObra(Guid idObra)
        {
            var obra = obraservice.RetornaComicPorId(idObra);
            if (obra == null)
                return Result.Fail("Obra não encontrada!");

            var listaVolumeComic = await service.ObterVolumesComicPorIdObra(idObra);

            var resultExisteListaVolume = ValidaExisteListaVolume(listaVolumeComic);
            if (resultExisteListaVolume.IsFailed)
                return Result.Fail(resultExisteListaVolume.Errors[0].Message);

            var listaRetornoVolumeComic = new List<RetornoVolumeComic>();

            foreach (var volumeComic in listaVolumeComic)
            {
                listaRetornoVolumeComic.Add(TrataRetornoVolumeComic(volumeComic));
            }

            return Result.Ok(listaRetornoVolumeComic);
        }

        public async Task<Result<List<RetornoVolumeComic>>> ObterVolumesComicPorSlugObra(string slugObra)
        {
            var obra = obraservice.RetornaComicPorSlug(slugObra);
            if (obra == null)
                return Result.Fail("Obra não encontrada!");

            var listaVolumeComic = await service.ObterVolumesComicPorSlugObra(slugObra);

            var resultExisteListaVolume = ValidaExisteListaVolume(listaVolumeComic);
            if (resultExisteListaVolume.IsFailed)
                return Result.Fail(resultExisteListaVolume.Errors[0].Message);

            var listaRetornoVolumeComic = new List<RetornoVolumeComic>();
            
            foreach (var volumeComic in listaVolumeComic)
            {
                listaRetornoVolumeComic.Add(TrataRetornoVolumeComic(volumeComic));
            }

            return Result.Ok(listaRetornoVolumeComic);
        }

        public RetornoVolumeComic TrataRetornoVolumeComic(VolumeComic volumeComic)
        {
            return new RetornoVolumeComic
            {
                Id = volumeComic.Id,
                IdObra = volumeComic.ComicId,
                NumeroVolume = volumeComic.Numero,
                Sinopse = volumeComic.Sinopse,
                SlugVolume = volumeComic.Slug,
                UrlCapaVolume = volumeComic.ImagemVolume,
                OrdemVolume = volumeComic.OrdemVolume,
                Publicado = volumeComic.Publicado,
                Titulo = volumeComic.Titulo,
                ListaRetornoCapitulosComic = TrataListaRetornoCapituloComic(volumeComic.ListaCapitulo)
            };
        }

        public Result ValidaExisteListaVolume(List<VolumeComic> listaVolumeComic)
        {
            if (listaVolumeComic.Count == 0)
                return Result.Fail("Lista de volumes não encontrado para essa obra!");
            return Result.Ok();
        }

        public List<RetornoCapituloComic> TrataListaRetornoCapituloComic(List<CapituloComic> listaCapituloComic)
        {
            var listaRetornoCapituloComic = new List<RetornoCapituloComic>();

            foreach (var capituloComic in listaCapituloComic.OrderBy(x => x.OrdemCapitulo))
            {
                var retornoCapitulo = mapper.Map<RetornoCapituloComic>(capituloComic);
                retornoCapitulo.DataInclusao = capituloComic.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
                retornoCapitulo.DataAlteracao = capituloComic.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
                retornoCapitulo.UsuarioAlteracao = !string.IsNullOrEmpty(capituloComic.UsuarioAlteracao) ? capituloComic.UsuarioAlteracao : null;

                if (!string.IsNullOrEmpty(capituloComic.ListaImagensJson))
                {
                    retornoCapitulo.ListaImagens = JsonConvert.DeserializeObject<List<EnderecoImagemDTO>>(capituloComic.ListaImagensJson);
                }

                listaRetornoCapituloComic.Add(retornoCapitulo);
            }

            return listaRetornoCapituloComic;
        }
    }
}