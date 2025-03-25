using FluentResults;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Helpers.Services.Interfaces;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{
    public class VolumeComicAppService(IVolumeComicService service, IObraService obraservice) 
        : IVolumeComicAppService, IBaseService<IVolumeComicService, IObraService>
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
                UrlCapaVolume = volumeComic.ImagemVolume
            };
        }

        public Result ValidaExisteListaVolume(List<VolumeComic> listaVolumeComic)
        {
            if (listaVolumeComic.Count == 0)
                return Result.Fail("Lista de volumes não encontrado para essa obra!");

            return Result.Ok();
        }
    }
}
