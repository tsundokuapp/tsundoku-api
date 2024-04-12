using FluentResults;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Services.AppServices.Interfaces;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.Imagens;
using TsundokuTraducoes.Helpers.Configuration;

namespace TsundokuTraducoes.Services.AppServices
{
    public class ImagemAppService : IImagemAppService
    {
        public string ApiKey = ConfigurationExternal.RetornaApiKeyTinify();

        public Result ProcessaUploadCapaObra(ObraDTO obraDTO)
        {
            var imagemCapaPrincipal = obraDTO.ImagemCapaPrincipalFile;

            if (!ValidaImagemPorContentType(imagemCapaPrincipal.ContentType))
                return Result.Fail("Verifique a extensão da imagem da capa. Extensões permitidas: JPG|JPEG|PNG");

            var nomeDiretorioImagensObra = TratamentoDeStrings.RetornaStringDiretorio(obraDTO.Alias);
            var diretorioImagemObra = Diretorios.RetornaDiretorioImagemCriado(nomeDiretorioImagensObra);

            string nomeArquivoImagem;
            string caminhoArquivoImagem;

            nomeArquivoImagem = $"Capa-Obra-{TratamentoDeStrings.RetornaStringSlugTitleCase(obraDTO.Alias)}.jpg";
            caminhoArquivoImagem = Path.Combine(diretorioImagemObra, nomeArquivoImagem);
            var byteImagem = OtimizacaoImagemTinify.ConverteStreamParaByteArray(imagemCapaPrincipal.OpenReadStream());
            var resultByteImagemOtimizada = OtimizacaoImagemTinify.OtimizarImagem(ApiKey, byteImagem).Result;

            if (resultByteImagemOtimizada.IsSuccess)
            {
                if(!OtimizacaoImagemTinify.SalvaArquivoImagem(resultByteImagemOtimizada.Value, caminhoArquivoImagem))
                    return Result.Fail("Erro ao tentar salvar o arquivo de imagem otimizada localmente");

                obraDTO.DiretorioImagemObra = diretorioImagemObra;
                obraDTO.ImagemCapaPrincipal = caminhoArquivoImagem;
            }            

            return Result.Ok();
        }

        public Result ProcessaUploadBannerObra(ObraDTO obraDTO)
        {
            var imagemBanner = obraDTO.ImagemBannerFile;

            if (!ValidaImagemPorContentType(imagemBanner.ContentType))
                return Result.Fail("Verifique a extensão da imagem do banner. Extensões permitidas: JPG|JPEG|PNG");

            string nomeArquivoImagem;

            nomeArquivoImagem = $"Banner-Obra-{TratamentoDeStrings.RetornaStringSlugTitleCase(obraDTO.Titulo)}.jpg";
            var caminhoArquivoImagemBanner = Path.Combine(obraDTO.DiretorioImagemObra, nomeArquivoImagem);
            var byteImagem = OtimizacaoImagemTinify.ConverteStreamParaByteArray(imagemBanner.OpenReadStream());
            var resultByteImagemOtimizada = OtimizacaoImagemTinify.OtimizarImagem(ApiKey, byteImagem).Result;

            if (resultByteImagemOtimizada.IsSuccess)
            {
                if (!OtimizacaoImagemTinify.SalvaArquivoImagem(resultByteImagemOtimizada.Value, caminhoArquivoImagemBanner))
                    return Result.Fail("Erro ao tentar salvar o arquivo de imagem otimizada localmente");

                obraDTO.ImagemBanner = caminhoArquivoImagemBanner;
            }            

            return Result.Ok();
        }

        public Result ProcessaUploadCapaVolume(VolumeDTO volumeDTO, string numeroVolume, string diretorioImagemObra)
        {
            var imagemCapa = volumeDTO.ImagemVolumeFile;

            if (!ValidaImagemPorContentType(imagemCapa.ContentType))
                return Result.Fail("Verifique a extensão da imagem. Extensões permitidas: JPG|JPEG|PNG");

            if (Directory.Exists(diretorioImagemObra))
            {
                string tituloVolumeTratado;
                var unico = numeroVolume?.ToLower() == "único";
                if (unico)
                {
                    tituloVolumeTratado = "Volume-Unico";
                }
                else
                {
                    var ehNumeroDouble = double.TryParse(numeroVolume, out double numeroVolumeTratado);
                    
                    if (!ehNumeroDouble)
                        return Result.Fail("Verifique o valor informado no campo Número!");

                    tituloVolumeTratado = $"Volume-{numeroVolumeTratado:00}";                    
                }

                var nomeImagemVolume = $"Capa-{tituloVolumeTratado}.jpg";
                var diretorioImagemVolume = Diretorios.RetornaDiretorioImagemCriado(diretorioImagemObra, $"{TratamentoDeStrings.RetornaStringDiretorio(tituloVolumeTratado.Replace("-", " "))}");
                var caminhoArquivoImagem = Path.Combine(diretorioImagemVolume, nomeImagemVolume);
                var byteImagem = OtimizacaoImagemTinify.ConverteStreamParaByteArray(imagemCapa.OpenReadStream());
                var resultByteImagemOtimizada = OtimizacaoImagemTinify.OtimizarImagem(ApiKey, byteImagem).Result;

                if (resultByteImagemOtimizada.IsSuccess)
                {
                    if (!OtimizacaoImagemTinify.SalvaArquivoImagem(resultByteImagemOtimizada.Value, caminhoArquivoImagem))
                        return Result.Fail("Erro ao tentar salvar o arquivo de imagem otimizada localmente");

                    volumeDTO.DiretorioImagemVolume = diretorioImagemVolume;
                    volumeDTO.ImagemVolume = caminhoArquivoImagem;
                }                
            }
            else
            {
                return Result.Fail("Diretório da obra não encontrado!");
            }

            return Result.Ok();
        }

        public Result ProcessaUploadListaImagensCapituloNovel(CapituloDTO capituloDTO, VolumeNovel volume, int? ordemPaginaImagem = null)
        {
            if (Directory.Exists(volume.DiretorioImagemVolume))
            {
                var nomeDiretorioCapitulo = capituloDTO.Slug;
                var diretorioCapitulo = Diretorios.RetornaDiretorioImagemCriado(volume.DiretorioImagemVolume, TratamentoDeStrings.RetornaStringDiretorio(nomeDiretorioCapitulo.Replace("-", " ")));

                // TODO - Adicionar esse tratamento posteriormente. A ordem ordem é igual ao ID, ao menos que alguém informe.
                var contador = 1;
                var listaEnderecoImagemDTO = new List<EnderecoImagemDTO>();

                foreach (var ilustracaoNovel in capituloDTO.ListaImagensForm)
                {
                    if (!ValidaImagemPorContentType(ilustracaoNovel.ContentType))
                        return Result.Fail("Verifique a extensão da imagem. Extensões permitidas: JPG|JPEG|PNG");

                    var extensaoImagem = Path.GetExtension(ilustracaoNovel.FileName);
                    var nomeImagem = ilustracaoNovel.FileName;
                    var nomeImagemTratada = nomeImagem.Replace(extensaoImagem, "");
                    var nomeArquivo = $"{nomeImagemTratada}.jpg";
                    var urlPaginasCapitulo = Path.Combine(diretorioCapitulo, nomeArquivo);
                    var byteImagem = OtimizacaoImagemTinify.ConverteStreamParaByteArray(ilustracaoNovel.OpenReadStream());
                    var resultByteImagemOtimizada = OtimizacaoImagemTinify.OtimizarImagem(ApiKey, byteImagem).Result;

                    if (resultByteImagemOtimizada.IsSuccess)
                    {
                        if (!OtimizacaoImagemTinify.SalvaArquivoImagem(resultByteImagemOtimizada.Value, urlPaginasCapitulo))
                            return Result.Fail("Erro ao tentar salvar o arquivo de imagem otimizada localmente");
                                                
                        listaEnderecoImagemDTO.Add(new EnderecoImagemDTO { Id = contador, Ordem = contador, Alt = nomeImagemTratada, Url = urlPaginasCapitulo });
                    }

                    contador++;
                }

                var imagensJson = JsonConvert.SerializeObject(listaEnderecoImagemDTO);

                capituloDTO.DiretorioImagemCapitulo = diretorioCapitulo;
                capituloDTO.ConteudoNovel = imagensJson;
            }
            else
            {
                return Result.Fail("Não foi encontrado o diretório do volume!");
            }

            return Result.Ok();
        }

        public Result ProcessaUploadListaImagensCapituloManga(CapituloDTO capituloDTO, VolumeComic volume, int? ordemPaginaImagem = null)
        {
            if (Directory.Exists(volume.DiretorioImagemVolume))
            {
                var nomeDiretorioCapitulo = capituloDTO.Slug;
                var diretorioCapitulo = Path.Combine(volume.DiretorioImagemVolume, TratamentoDeStrings.RetornaStringDiretorio(nomeDiretorioCapitulo.Replace("-", " ")));

                if (!Directory.Exists(diretorioCapitulo))
                {
                    Diretorios.CriaDiretorio(diretorioCapitulo);
                }

                // TODO - Adicionar esse tratamento posteriormente. A ordem ordem é igual ao ID, ao menos que alguém informe.
                var contador = 1;
                var listaEnderecoImagemDTO = new List<EnderecoImagemDTO>();

                foreach (var imagemPagina in capituloDTO.ListaImagensForm)
                {
                    if (!ValidaImagemPorContentType(imagemPagina.ContentType))
                        return Result.Fail("Verifique a extensão da imagem. Extensões permitidas: JPG|JPEG|PNG");

                    var nomeArquivo = $"Pagina-{contador:#00}.jpg";
                    var urlPaginasCapitulo = Path.Combine(diretorioCapitulo, nomeArquivo);
                    var byteImagem = OtimizacaoImagemTinify.ConverteStreamParaByteArray(imagemPagina.OpenReadStream());
                    var resultByteImagemOtimizada = OtimizacaoImagemTinify.OtimizarImagem(ApiKey, byteImagem).Result;

                    if (resultByteImagemOtimizada.IsSuccess)
                    {
                        if (!OtimizacaoImagemTinify.SalvaArquivoImagem(resultByteImagemOtimizada.Value, urlPaginasCapitulo))
                            return Result.Fail("Erro ao tentar salvar o arquivo de imagem otimizada localmente");

                        listaEnderecoImagemDTO.Add(new EnderecoImagemDTO { Id = contador, Url = urlPaginasCapitulo, Ordem = contador });
                    }

                    contador++;
                }

                var imagensJson = JsonConvert.SerializeObject(listaEnderecoImagemDTO);

                capituloDTO.ListaImagemCapitulo = imagensJson;
                capituloDTO.DiretorioImagemCapitulo = diretorioCapitulo;
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

        public bool ValidaImagemPorContentType(string contentType)
        {
            var imagemValida = contentType.ToLower().Contains("png") ||
                contentType.ToLower().Contains("jpg") ||
                contentType.ToLower().Contains("jpeg");

            return imagemValida;
        }
    }
}
