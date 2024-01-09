using AutoMapper;
using FluentResults;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;
using TsundokuTraducoes.Services.AppServices.Interfaces;

#nullable disable

namespace TsundokuTraducoes.Services.AppServices
{
    public class VolumeAppService : IVolumeAppService
    {
        private readonly IMapper _mapper;
        private readonly IVolumeService _volumeService;
        private readonly IObraService _obraService;
        private readonly IImagemAppService _imagemAppService;

        public VolumeAppService(IMapper mapper, IVolumeService volumeService, IObraService obraService, IImagemAppService imagemAppService)
        {
            _mapper = mapper;
            _volumeService = volumeService;
            _obraService = obraService;
            _imagemAppService = imagemAppService;
        }

        public async Task<Result<List<RetornoVolume>>> RetornaListaVolumes(Guid? idObra)
        {
            var listaVolumes = new List<RetornoVolume>();
            var listaVolumesNovel = await RetornaListaVolumesNovel(idObra);
            var listaVolumesComic = await RetornaListaVolumesComic(idObra);

            listaVolumes.AddRange(listaVolumesNovel.Value);
            listaVolumes.AddRange(listaVolumesComic.Value);           

            return Result.Ok(listaVolumes);
        }

        public async Task<Result<List<RetornoVolume>>> RetornaListaVolumesNovel(Guid? idObra)
        {
            var listaRetornoVolume = new List<RetornoVolume>();
            var listaVolumesNovel = await _volumeService.RetornaListaVolumesNovel(idObra);

            if (listaVolumesNovel.Count > 0)
            {
                foreach (var volume in listaVolumesNovel)
                {
                    listaRetornoVolume.Add(TrataRetornoVolumeNovel(volume));
                }
            }

            return Result.Ok( listaRetornoVolume);
        }

        public async Task<Result<List<RetornoVolume>>> RetornaListaVolumesComic(Guid? idObra)
        {
            var listaRetornoVolume = new List<RetornoVolume>();
            var listaVolumesComic = await _volumeService.RetornaListaVolumesComic(idObra);

            if (listaVolumesComic.Count > 0)
            {
                foreach (var volume in listaVolumesComic)
                {
                    listaRetornoVolume.Add(TrataRetornoVolumeComic(volume));
                }
            }

            return Result.Ok(listaRetornoVolume);
        }


        public async Task<Result<RetornoVolume>> RetornaVolumeNovelPorId(Guid id)
        {
            var volume = await _volumeService.RetornaVolumeNovelPorId(id);
            if (volume == null)
                return Result.Fail("Volume não encontrado!");

            var retornoVolume = TrataRetornoVolumeNovel(volume);
            return Result.Ok(retornoVolume);
        }

        public async Task<Result<RetornoVolume>> RetornaVolumeComicPorId(Guid id)
        {
            var volume = await _volumeService.RetornaVolumeComicPorId(id);
            if (volume == null)
                return Result.Fail("Volume não encontrado!");

            var retornoVolume = TrataRetornoVolumeComic(volume);
            return Result.Ok(retornoVolume);
        }


        public async Task<Result<RetornoVolume>> AdicionaVolumeNovel(VolumeDTO volumeDTO)
        {
            var volume = _mapper.Map<VolumeNovel>(volumeDTO);
            var novel = await _obraService.RetornaNovelPorId(volumeDTO.ObraId);
            var volumeExistente = await _volumeService.RetornaVolumeNovelExistente(volumeDTO);

            if (novel == null)
                return Result.Fail("Não foi encontrada a obra informada");

            if (volumeExistente != null)
                return Result.Fail("Volume já postado!");

            if (volumeDTO.ImagemVolumeFile != null)
            {
                var result = _imagemAppService.ProcessaUploadCapaVolume(volumeDTO, volumeDTO.Numero, novel.DiretorioImagemObra);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                volume.DiretorioImagemVolume = volumeDTO.DiretorioImagemVolume;
                volume.ImagemVolume = volumeDTO.ImagemVolume;
            }
            else
            {
                return Result.Fail("Erro ao adicionar volume da obra, capa do volume não enviada!");
            }

            volume.DataInclusao = DateTime.Now;
            volume.DataAlteracao = volume.DataInclusao;
            var volumeAdicionado = _volumeService.AdicionaVolumeNovel(volume);
            if (volumeAdicionado)
            {
                _volumeService.AtualizaNovelPorVolume(novel, volume);
                var retornoVolume = TrataRetornoVolumeNovel(volume);
                return Result.Ok(retornoVolume);
            }

            return Result.Fail("Erro ao adicionar volume da obra!");
        }

        public async Task<Result<RetornoVolume>> AdicionaVolumeComic(VolumeDTO volumeDTO)
        {
            var volume = _mapper.Map<VolumeComic>(volumeDTO);
            var comic = await _obraService.RetornaComicPorId(volumeDTO.ObraId);
            var volumeExistente = await _volumeService.RetornaVolumeComicExistente(volumeDTO);

            if (comic == null)
                return Result.Fail("Não foi encontrada a obra informada");

            if (volumeExistente != null)
                return Result.Fail("Volume já postado!");

            if (volumeDTO.ImagemVolumeFile != null)
            {
                var result = _imagemAppService.ProcessaUploadCapaVolume(volumeDTO, volumeDTO.Numero, comic.DiretorioImagemObra);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                volume.DiretorioImagemVolume = volumeDTO.DiretorioImagemVolume;
                volume.ImagemVolume = volumeDTO.ImagemVolume;
            }
            else
            {
                return Result.Fail("Erro ao adicionar volume da obra, capa do volume não enviada!");
            }

            volume.DataInclusao = DateTime.Now;
            volume.DataAlteracao = volume.DataInclusao;
            var volumeComicAdicionado = _volumeService.AdicionaVolumeComic(volume);
            if (volumeComicAdicionado)
            {
                _volumeService.AtualizaComicPorVolume(comic, volume);
                var retornoVolume = TrataRetornoVolumeComic(volume);
                return Result.Ok(retornoVolume);
            }

            return Result.Fail("Erro ao adicionar volume da obra!");
        }


        public async Task<Result<RetornoVolume>> AtualizaVolumeNovel(VolumeDTO volumeDTO)
        {
            var volumeEncontrado = await _volumeService.RetornaVolumeNovelPorId(volumeDTO.Id);
            if (volumeEncontrado == null)
                return Result.Fail("Volume não encontrado!");

            if (volumeDTO.ImagemVolumeFile != null)
            {
                var novel = await _obraService.RetornaNovelPorId(volumeDTO.ObraId);
                if (novel == null)
                    return Result.Fail("Não foi encontrada a obra informada");

                var result = _imagemAppService.ProcessaUploadCapaVolume(volumeDTO, volumeDTO.Numero, novel.DiretorioImagemObra);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                volumeEncontrado.DiretorioImagemVolume = volumeDTO.DiretorioImagemVolume;
                volumeEncontrado.ImagemVolume = volumeDTO.ImagemVolume;
            }

            volumeEncontrado = _volumeService.AtualizaVolumeNovel(volumeDTO);
            if (!_volumeService.AlteracoesSalvas())
                return Result.Fail("Erro ao atualizar o volume!");

            var retornoVolume = TrataRetornoVolumeNovel(volumeEncontrado);
            return Result.Ok(retornoVolume);
        }

        public async Task<Result<RetornoVolume>> AtualizaVolumeComic(VolumeDTO volumeDTO)
        {
            var volumeEncontrado = await _volumeService.RetornaVolumeComicPorId(volumeDTO.Id);
            if (volumeEncontrado == null)
                return Result.Fail("Volume não encontrado!");

            if (volumeDTO.ImagemVolumeFile != null)
            {
                var comic = await _obraService.RetornaComicPorId(volumeDTO.ObraId);
                if (comic == null)
                    return Result.Fail("Não foi encontrada a obra informada");

                var result = _imagemAppService.ProcessaUploadCapaVolume(volumeDTO, volumeDTO.Numero, comic.DiretorioImagemObra);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                volumeEncontrado.DiretorioImagemVolume = volumeDTO.DiretorioImagemVolume;
                volumeEncontrado.ImagemVolume = volumeDTO.ImagemVolume;
            }

            volumeEncontrado = _volumeService.AtualizaVolumeComic(volumeDTO);
            if (!_volumeService.AlteracoesSalvas())
                return Result.Fail("Erro ao atualizar o volume!");

            var retornoVolume = TrataRetornoVolumeComic(volumeEncontrado);
            return Result.Ok(retornoVolume);
        }


        public async Task<Result<bool>> ExcluiVolumeNovel(Guid novelId)
        {
            var volumeEncontrado = await _volumeService.RetornaVolumeNovelPorId(novelId);
            if (volumeEncontrado == null)
                return Result.Fail("Volume não encontrado!");

            var volumeExcluido = _volumeService.ExcluiVolumeNovel(volumeEncontrado);
            _imagemAppService.ExcluiDiretorioImagens(volumeEncontrado.DiretorioImagemVolume);

            if (!volumeExcluido)
                return Result.Fail("Erro ao excluir o volume!");


            return Result.Ok().WithSuccess("Volume excluído com sucesso!");
        }

        public async Task<Result<bool>> ExcluiVolumeComic(Guid comicId)
        {
            var volumeEncontrado = await _volumeService.RetornaVolumeComicPorId(comicId);
            if (volumeEncontrado == null)
                return Result.Fail("Volume não encontrado!");

            var volumeExcluido = _volumeService.ExcluiVolumeComic(volumeEncontrado);
            _imagemAppService.ExcluiDiretorioImagens(volumeEncontrado.DiretorioImagemVolume);

            if (!volumeExcluido)
                return Result.Fail("Erro ao excluir o volume!");

            return Result.Ok().WithSuccess("Volume excluído com sucesso!");
        }


        private RetornoVolume TrataRetornoVolumeNovel(VolumeNovel volumeNovel)
        {
            var retornoVolume = _mapper.Map<RetornoVolume>(volumeNovel);
            retornoVolume.DataInclusao = volumeNovel.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoVolume.DataAlteracao = volumeNovel.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoVolume.UsuarioAlteracao = !string.IsNullOrEmpty(volumeNovel.UsuarioAlteracao) ? volumeNovel.UsuarioAlteracao : null;
            return retornoVolume;
        }

        private RetornoVolume TrataRetornoVolumeComic(VolumeComic volumeComic)
        {
            var retornoVolume = _mapper.Map<RetornoVolume>(volumeComic);
            retornoVolume.DataInclusao = volumeComic.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoVolume.DataAlteracao = volumeComic.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoVolume.UsuarioAlteracao = !string.IsNullOrEmpty(volumeComic.UsuarioAlteracao) ? volumeComic.UsuarioAlteracao : null;
            return retornoVolume;
        }
    }
}
