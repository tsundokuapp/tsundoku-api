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
    public class VolumeNovelAppService(IVolumeNovelService service, IObraService obraservice, IMapper mapper)
        : IVolumeNovelAppService, IBaseService<IVolumeNovelService, IObraService, IMapper>
    {
        public async Task<Result<List<RetornoVolumeNovel>>> ObterVolumesNovelPorIdObra(Guid idObra)
        {
            var obra = obraservice.RetornaNovelPorId(idObra);
            if (obra == null)
                return Result.Fail("Obra não encontrada!");

            var listaVolumeNovel = await service.ObterVolumesNovelPorIdObra(idObra);

            var resultExisteListaVolume = ValidaExisteListaVolume(listaVolumeNovel);
            if (resultExisteListaVolume.IsFailed)
                return Result.Fail(resultExisteListaVolume.Errors[0].Message);

            var listaRetornoVolumeNovel = new List<RetornoVolumeNovel>();

            foreach (var volumeNovel in listaVolumeNovel)
            {
                listaRetornoVolumeNovel.Add(TrataRetornoVolumeNovel(volumeNovel));
            }

            return Result.Ok(listaRetornoVolumeNovel);
        }

        public async Task<Result<List<RetornoVolumeNovel>>> ObterVolumesNovelPorSlugObra(string slugObra)
        {
            var obra = obraservice.RetornaNovelPorSlug(slugObra);
            if (obra == null)
                return Result.Fail("Obra não encontrada!");

            var listaVolumeNovel = await service.ObterVolumesNovelPorSlugObra(slugObra);

            var resultExisteListaVolume = ValidaExisteListaVolume(listaVolumeNovel);
            if (resultExisteListaVolume.IsFailed)
                return Result.Fail(resultExisteListaVolume.Errors[0].Message);

            var listaRetornoVolumeNovel = new List<RetornoVolumeNovel>();

            foreach (var volumeNovel in listaVolumeNovel)
            {
                listaRetornoVolumeNovel.Add(TrataRetornoVolumeNovel(volumeNovel));
            }

            return Result.Ok(listaRetornoVolumeNovel);
        }

        public RetornoVolumeNovel TrataRetornoVolumeNovel(VolumeNovel volumeNovel)
        {
            return new RetornoVolumeNovel
            {
                Id = volumeNovel.Id,
                IdObra = volumeNovel.NovelId,
                NumeroVolume = volumeNovel.Numero,
                Sinopse = volumeNovel.Sinopse,
                SlugVolume = volumeNovel.Slug,
                UrlCapaVolume = volumeNovel.ImagemVolume,
                OrdemVolume = volumeNovel.OrdemVolume,
                Publicado = volumeNovel.Publicado,
                Titulo = volumeNovel.Titulo,
                ListaRetornoCapitulosNovel = TrataListaRetornoCapituloNovel(volumeNovel.ListaCapitulo)
            };
        }

        public Result ValidaExisteListaVolume(List<VolumeNovel> listaVolumeNovel)
        {
            if (listaVolumeNovel.Count == 0)
                return Result.Fail("Lista de volumes não encontrado para essa obra!");
            return Result.Ok();
        }

        public List<RetornoCapituloNovel> TrataListaRetornoCapituloNovel(List<CapituloNovel> listaCapituloNovel)
        {
            var listaRetornoCapituloNovel = new List<RetornoCapituloNovel>();

            foreach (var capituloNovel in listaCapituloNovel.OrderBy(x => x.OrdemCapitulo))
            {
                var retornoCapitulo = mapper.Map<RetornoCapituloNovel>(capituloNovel);
                retornoCapitulo.DataInclusao = capituloNovel.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
                retornoCapitulo.DataAlteracao = capituloNovel.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
                retornoCapitulo.UsuarioAlteracao = !string.IsNullOrEmpty(capituloNovel.UsuarioAlteracao) ? capituloNovel.UsuarioAlteracao : null;

                if (!string.IsNullOrEmpty(capituloNovel.ListaImagensJson))
                {
                    retornoCapitulo.ListaImagens = JsonConvert.DeserializeObject<List<EnderecoImagemDTO>>(capituloNovel.ListaImagensJson);
                }

                listaRetornoCapituloNovel.Add(retornoCapitulo);
            }

            return listaRetornoCapituloNovel;
        }
    }
}
