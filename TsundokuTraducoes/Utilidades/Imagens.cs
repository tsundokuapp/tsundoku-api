using System.IO;
using TsundokuTraducoes.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Models;
using Microsoft.AspNetCore.Hosting;
using TsundokuTraducoes.Api.DTOs.Admin;
using Newtonsoft.Json;

namespace TsundokuTraducoes.Api.Utilidades
{
    public class Imagens
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Imagens(IWebHostEnvironment webHostEnvironment = null)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void ProcessaUploadImagemCapaObra(IFormFile imagemCapa, string titulo, Obra obra, ObraDTO obraDTO)
        {
            var extensaoImagem = Path.GetExtension(imagemCapa.FileName);
            var diretorioArquivo = TratamentoDeStrings.RetornaStringDiretorio(titulo);
            var diretorioObraLocal = Path.Combine(_webHostEnvironment.WebRootPath, "images", diretorioArquivo);
            var diretorioObraApi = Path.Combine(Constantes.UrlDiretioWebImagens, "images", diretorioArquivo);
            var diretorioObraLocalHost = Path.Combine(Constantes.UrlDiretorioLocalHostImagens, "images", diretorioArquivo);

            Diretorios.CriaDiretorio(diretorioObraLocal);

            var nomeArquivoImagem = $"Capa-Obra-{TratamentoDeStrings.RetornaStringSlugTitleCase(titulo)}{extensaoImagem}";
            var caminhoArquivo = Path.Combine(diretorioObraLocal, nomeArquivoImagem);
            var caminhoArquivoApi = Path.Combine(diretorioObraApi, nomeArquivoImagem);
            var caminhoArquivoLocalHost = Path.Combine(diretorioObraLocalHost, nomeArquivoImagem);

            using var fileStream = new FileStream(caminhoArquivo, FileMode.Create);
            imagemCapa.CopyTo(fileStream);

            if (Constantes.AmbienteDesenvolvimento.ToLower() == "sim")
            {               
                //obra.DiretorioObra = diretorioObraLocal;
                obra.EnderecoUrlCapa = caminhoArquivoLocalHost;
                obraDTO.EnderecoUrlCapa = caminhoArquivoLocalHost;
            }
            else
            {
                //obra.DiretorioObra = diretorioObraLocal;
                obra.EnderecoUrlCapa = caminhoArquivoApi;
                obraDTO.EnderecoUrlCapa = caminhoArquivoApi;
            }
        }

        public void ProcessaUploadImagemCapaVolume(IFormFile imagemCapa, Volume volume, Obra obra, VolumeDTO volumeDTO)
        {
            //var extensaoImagem = Path.GetExtension(imagemCapa.FileName);
            //var slugTituloObra = TratamentoDeStrings.RetornaStringDiretorio(obra.Titulo);

            //if (Directory.Exists(obra.DiretorioObra))
            //{   
            //    var diretorioVolumeLocal = Path.Combine(obra.DiretorioObra, $"Volume-{volume.Numero:00}");
            //    Diretorios.CriaDiretorio(diretorioVolumeLocal);

            //    var nomeArquivoImagem = $"Capa-Volume-{TratamentoDeStrings.RetornaStringSlugTitleCase(volume.Numero.ToString())}-Obra-{slugTituloObra}{extensaoImagem}";
            //    var caminhoArquivo = Path.Combine(diretorioVolumeLocal, nomeArquivoImagem);
            //    volume.DiretorioVolume = diretorioVolumeLocal;

            //    SalvaArquivoFormFile(imagemCapa, caminhoArquivo);

            //    var diretorioArquivo = TratamentoDeStrings.RetornaStringDiretorio(obra.Titulo);
            //    var diretorioVolumeApi = Path.Combine(Constantes.UrlDiretioWebImagens, "images", diretorioArquivo, $"Volume-{volume.Numero:00}");
            //    var caminhoArquivoApi = Path.Combine(diretorioVolumeApi, nomeArquivoImagem);

            //    var diretorioVolumeLocalHost = Path.Combine(Constantes.UrlDiretorioLocalHostImagens, "images", diretorioArquivo, $"Volume-{volume.Numero:00}");
            //    var caminhoArquivoLocalHost = Path.Combine(diretorioVolumeLocalHost, nomeArquivoImagem);


            //    if (Constantes.AmbienteDesenvolvimento.ToLower() == "sim")
            //    {
            //        volume.ImagemVolume = caminhoArquivoLocalHost;
            //        volumeDTO.UrlImagemVolume = caminhoArquivoLocalHost;
            //    }
            //    else
            //    {
            //        volume.ImagemVolume = caminhoArquivoApi;
            //        volumeDTO.UrlImagemVolume = caminhoArquivoApi;
            //    }
            //}
        }

        public ResultadoMensagemDTO ProcessaListaUploadImagemPaginaCapitulo(List<IFormFile> listaImagemPagina, Obra obra, Volume volume, CapituloNovel capitulo)
        {
            var resultadoMensagemDTO = new ResultadoMensagemDTO();

            if (Directory.Exists(volume.DiretorioVolume))
            {
                var nomeDiretorioCapitulo = $"Capitulo-{capitulo.Numero:00}";
                var diretorioCapitulo = Path.Combine(volume.DiretorioVolume, nomeDiretorioCapitulo);

                if (!Directory.Exists(diretorioCapitulo))
                {
                    Diretorios.CriaDiretorio(diretorioCapitulo);
                }

                var diretorioCapituloApi = Path.Combine(Constantes.UrlDiretioWebImagens, "images", TratamentoDeStrings.RetornaStringDiretorio(obra.Titulo), $"Volume-{volume.Numero:00}", nomeDiretorioCapitulo);
                var diretorioCapituloLocalHost = Path.Combine(Constantes.UrlDiretorioLocalHostImagens, "images", TratamentoDeStrings.RetornaStringDiretorio(obra.Titulo), $"Volume-{volume.Numero:00}", nomeDiretorioCapitulo);

                var contador = 0;

                foreach (var imagemPagina in listaImagemPagina)
                {
                    var extensaoImagem = Path.GetExtension(imagemPagina.FileName);
                    var nomeArquivo = $"Pagina-{contador:#00}{extensaoImagem}";
                    var urlPaginasCapitulo = Path.Combine(diretorioCapitulo, nomeArquivo);
                    var urlPaginasCapituloApi = Path.Combine(diretorioCapituloApi, nomeArquivo);
                    var urlPaginasCapituloLocalHost = Path.Combine(diretorioCapituloLocalHost, nomeArquivo);

                    SalvaArquivoFormFile(imagemPagina, urlPaginasCapitulo);

                    if (Constantes.AmbienteDesenvolvimento.ToLower() == "sim")
                    {
                        capitulo.ListaImagensManga.Add(new CapituloManga { Url = urlPaginasCapituloLocalHost, CapituloId = capitulo.Id });
                    }
                    else
                    {
                        capitulo.ListaImagensManga.Add(new CapituloManga { Url = urlPaginasCapituloApi, CapituloId = capitulo.Id });
                    }

                    contador++;
                }

                //var imagensJson = JsonConvert.SerializeObject(capitulo.ListaImagensManga);


                capitulo.DiretorioCapitulo = diretorioCapitulo;
            }
            else
            {
                Auxiliares.AdicionaMensagemErro(resultadoMensagemDTO, "Não foi encontrado o diretório do volume!");
            }

            return resultadoMensagemDTO;
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
