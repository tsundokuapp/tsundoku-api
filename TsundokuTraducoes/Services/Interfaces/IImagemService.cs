﻿using FluentResults;
using Microsoft.AspNetCore.Http;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models.Volume;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface IImagemService
    {
        Result ProcessaUploadCapaObra(ObraDTO obraDTO);
        Result ProcessaUploadBannerObra(ObraDTO obraDTO);
        Result ProcessaUploadCapaVolume(VolumeDTO volumeDTO, string numeroVolume, string diretorioImagemObra);
        Result ProcessaUploadListaImagensCapituloNovel(CapituloDTO capituloDTO, VolumeNovel volume, int? ordemPaginaImagem = null);
        Result ProcessaUploadListaImagensCapituloManga(CapituloDTO capituloDTO, VolumeComic volume, int? ordemPaginaImagem = null);
        void SalvaArquivoFormFile(IFormFile arquivoFormFile, string caminhoCompletoArquivo);
        void ExcluiDiretorioImagens(string diretorioImagens);
        bool ValidaImagemPorContentType(string contentType);
    }
}
