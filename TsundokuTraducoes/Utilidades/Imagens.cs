using FluentResults;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Utilidades
{
    public class Imagens
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Imagens(IWebHostEnvironment webHostEnvironment = null)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void ProcessaImagemObra(IFormFile imagemCapa, string titulo, Obra obra, ObraDTO obraDTO, bool banner = false)
        {
            var extensaoImagem = Path.GetExtension(imagemCapa.FileName);
            var diretorioArquivo = TratamentoDeStrings.RetornaStringDiretorio(titulo);
            var diretorioObraLocal = Path.Combine(_webHostEnvironment.WebRootPath, "images", diretorioArquivo);
            var diretorioObraApi = Path.Combine(Constantes.UrlDiretioWebImagens, "images", diretorioArquivo);
            var diretorioObraLocalHost = Path.Combine(Constantes.UrlDiretorioLocalHostImagens, "images", diretorioArquivo);

            Diretorios.CriaDiretorio(diretorioObraLocal);

            var nomeArquivoImagem = string.Empty;
            if (!banner)
            {
                nomeArquivoImagem = $"Capa-Obra-{TratamentoDeStrings.RetornaStringSlugTitleCase(titulo)}{extensaoImagem}";
            }
            else
            {
                nomeArquivoImagem = $"Banner-Obra-{TratamentoDeStrings.RetornaStringSlugTitleCase(titulo)}{extensaoImagem}";
            }
            var caminhoArquivo = Path.Combine(diretorioObraLocal, nomeArquivoImagem);
            var caminhoArquivoApi = Path.Combine(diretorioObraApi, nomeArquivoImagem);
            var caminhoArquivoLocalHost = Path.Combine(diretorioObraLocalHost, nomeArquivoImagem);

            using var fileStream = new FileStream(caminhoArquivo, FileMode.Create);
            imagemCapa.CopyTo(fileStream);

            if (Constantes.AmbienteDesenvolvimento.ToLower() == "sim")
            {
                if (!banner)
                {
                    obra.DiretorioImagemObra = diretorioObraLocal;
                    obra.ImagemCapaPrincipal = caminhoArquivoLocalHost;
                    obraDTO.ImagemCapaPrincipal = caminhoArquivoLocalHost;
                }
                else
                {
                    obra.DiretorioImagemObra = diretorioObraLocal;
                    obra.ImagemBanner = caminhoArquivoLocalHost;
                    obraDTO.ImagemBanner = caminhoArquivoLocalHost;
                }
                    
            }
            else
            {
                if (!banner)
                {
                    obra.DiretorioImagemObra = diretorioObraApi;
                    obra.ImagemCapaPrincipal = caminhoArquivoApi;
                    obraDTO.ImagemCapaPrincipal = caminhoArquivoApi;
                }
                else
                {
                    obra.DiretorioImagemObra = diretorioObraApi;
                    obra.ImagemBanner = caminhoArquivoApi;
                    obraDTO.ImagemBanner = caminhoArquivoApi;
                }
            }
        }

        public void ProcessaUploadImagemCapaVolume(IFormFile imagemCapa, Volume volume, Obra obra, VolumeDTO volumeDTO)
        {
            var extensaoImagem = Path.GetExtension(imagemCapa.FileName);
            var slugTituloObra = TratamentoDeStrings.RetornaStringDiretorio(obra.Titulo);

            if (Directory.Exists(obra.DiretorioImagemObra))
            {
                var diretorioVolumeLocal = Path.Combine(obra.DiretorioImagemObra, $"Volume-{volumeDTO.DescritivoVolume}");
                Diretorios.CriaDiretorio(diretorioVolumeLocal);

                var nomeArquivoImagem = $"Capa-Volume-{TratamentoDeStrings.RetornaStringSlugTitleCase(volume.DescritivoVolume)}-Obra-{slugTituloObra}{extensaoImagem}";
                var caminhoArquivo = Path.Combine(diretorioVolumeLocal, nomeArquivoImagem);
                volume.DiretorioImagemVolume = diretorioVolumeLocal;

                SalvaArquivoFormFile(imagemCapa, caminhoArquivo);

                var diretorioArquivo = TratamentoDeStrings.RetornaStringDiretorio(obra.Titulo);
                var diretorioVolumeApi = Path.Combine(Constantes.UrlDiretioWebImagens, "images", diretorioArquivo, $"Volume-{volumeDTO.DescritivoVolume}");
                var caminhoArquivoApi = Path.Combine(diretorioVolumeApi, nomeArquivoImagem);

                var diretorioVolumeLocalHost = Path.Combine(Constantes.UrlDiretorioLocalHostImagens, "images", diretorioArquivo, $"Volume-{volume.DescritivoVolume}");
                var caminhoArquivoLocalHost = Path.Combine(diretorioVolumeLocalHost, nomeArquivoImagem);


                if (Constantes.AmbienteDesenvolvimento.ToLower() == "sim")
                {
                    volume.ImagemVolume = caminhoArquivoLocalHost;
                    volumeDTO.ImagemCapaVolume = caminhoArquivoLocalHost;
                }
                else
                {
                    volume.ImagemVolume = caminhoArquivoApi;
                    volumeDTO.ImagemCapaVolume = caminhoArquivoApi;
                }
            }
        }

        public Result ProcessaListaUploadImagemPaginaCapitulo(CapituloDTO capituloDTO, Obra obra, Volume volume, int capituloId)
        {
            if (Directory.Exists(volume.DiretorioImagemVolume))
            {   
                var nomeDiretorioCapitulo = capituloDTO.Slug;
                var diretorioCapitulo = Path.Combine(volume.DiretorioImagemVolume, nomeDiretorioCapitulo);

                if (!Directory.Exists(diretorioCapitulo))
                {
                    Diretorios.CriaDiretorio(diretorioCapitulo);
                }

                var diretorioCapituloApi = Path.Combine(Constantes.UrlDiretioWebImagens, "images", TratamentoDeStrings.RetornaStringDiretorio(obra.Titulo), $"Volume-{volume.Numero:00}", nomeDiretorioCapitulo);
                var diretorioCapituloLocalHost = Path.Combine(Constantes.UrlDiretorioLocalHostImagens, "images", TratamentoDeStrings.RetornaStringDiretorio(obra.Titulo), $"Volume-{volume.Numero:00}", nomeDiretorioCapitulo);

                var contador = 1;
                var listaEnderecoImagemDTO = new List<EnderecoImagemDTO>();

                foreach (var imagemPagina in capituloDTO.ListaImagensForm)
                {
                    var extensaoImagem = Path.GetExtension(imagemPagina.FileName);
                    var nomeArquivo = $"Pagina-{contador:#00}{extensaoImagem}";
                    var urlPaginasCapitulo = Path.Combine(diretorioCapitulo, nomeArquivo);
                    var urlPaginasCapituloApi = Path.Combine(diretorioCapituloApi, nomeArquivo);
                    var urlPaginasCapituloLocalHost = Path.Combine(diretorioCapituloLocalHost, nomeArquivo);

                    SalvaArquivoFormFile(imagemPagina, urlPaginasCapitulo);


                    if (Constantes.AmbienteDesenvolvimento.ToLower() == "sim")
                    {
                        listaEnderecoImagemDTO.Add(new EnderecoImagemDTO { Id = contador, Url = urlPaginasCapituloLocalHost });
                    }
                    else
                    {
                        listaEnderecoImagemDTO.Add(new EnderecoImagemDTO { Id = contador, Url = urlPaginasCapituloApi});
                    }

                    contador++;
                }

                capituloDTO.DiretorioImagemCapitulo = diretorioCapitulo;  
                var imagensJson = JsonConvert.SerializeObject(listaEnderecoImagemDTO);

                if (capituloDTO.EhIlustracoesNovel)
                {
                    capituloDTO.ConteudoNovel = imagensJson;
                }
                else
                {
                    capituloDTO.ListaImagemCapitulo = imagensJson;
                }
            }
            else
            {

                return Result.Fail("Não foi encontrado o diretório do volume!");
            }

            return Result.Ok();
        }

        public void SalvaArquivoFormFile(IFormFile arquivoFormFile, string caminhoCompletoArquivo)
        {
            using var fileStream = new FileStream(caminhoCompletoArquivo, FileMode.Create);
            arquivoFormFile.CopyTo(fileStream);
        }        

        public void ExcluiDiretorioImagens(string diretorioImagens)
        {
            if (Directory.Exists(diretorioImagens))
            {
                Directory.Delete(diretorioImagens, true);
            }
        }
    }
}
