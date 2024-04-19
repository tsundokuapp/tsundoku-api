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
        public Result ProcessaUploadCapaObra(ObraDTO obraDTO)
        {
            var imagemCapaPrincipal = obraDTO.ImagemCapaPrincipalFile;

            if (!UtilidadeImagem.ValidaImagemPorContentType(imagemCapaPrincipal.ContentType))
                return Result.Fail("Verifique a extensão da imagem da capa. Extensões permitidas: JPG|JPEG|PNG");

            var nomeDiretorioImagensObra = TratamentoDeStrings.RetornaStringDiretorio(obraDTO.Alias);
            var diretorioImagemObra = Diretorios.RetornaDiretorioImagemCriado(nomeDiretorioImagensObra);

            string nomeArquivoImagem;
            string caminhoArquivoImagem;

            nomeArquivoImagem = $"Capa-Obra-{TratamentoDeStrings.RetornaStringSlugTitleCase(obraDTO.Alias)}.jpg";
            caminhoArquivoImagem = Path.Combine(diretorioImagemObra, nomeArquivoImagem);

            if (obraDTO.OtimizarImagem)
            {
                var retorno = RetornaImagemOtimizada(imagemCapaPrincipal, caminhoArquivoImagem);
                if (!retorno.IsSuccess)
                    return Result.Fail(retorno.Errors[0].Message);
            }
            else
            {   
                SalvaArquivoFormFile(imagemCapaPrincipal, caminhoArquivoImagem);
            }

            obraDTO.DiretorioImagemObra = diretorioImagemObra;
            obraDTO.ImagemCapaPrincipal = caminhoArquivoImagem;

            return Result.Ok();
        }

        public Result ProcessaUploadBannerObra(ObraDTO obraDTO)
        {
            var imagemBanner = obraDTO.ImagemBannerFile;

            if (!UtilidadeImagem.ValidaImagemPorContentType(imagemBanner.ContentType))
                return Result.Fail("Verifique a extensão da imagem do banner. Extensões permitidas: JPG|JPEG|PNG");

            string nomeArquivoImagem;

            nomeArquivoImagem = $"Banner-Obra-{TratamentoDeStrings.RetornaStringSlugTitleCase(obraDTO.Titulo)}.jpg";
            var caminhoArquivoImagemBanner = Path.Combine(obraDTO.DiretorioImagemObra, nomeArquivoImagem);

            if (obraDTO.OtimizarImagem)
            {
                var retorno = RetornaImagemOtimizada(imagemBanner, caminhoArquivoImagemBanner);
                if (!retorno.IsSuccess)
                    return Result.Fail(retorno.Errors[0].Message);
            }
            else
            {
                SalvaArquivoFormFile(imagemBanner, caminhoArquivoImagemBanner);
            }

            obraDTO.ImagemBanner = caminhoArquivoImagemBanner;

            return Result.Ok();
        }

        public Result ProcessaUploadCapaVolume(VolumeDTO volumeDTO, string numeroVolume, string diretorioImagemObra)
        {
            var imagemCapaVolume = volumeDTO.ImagemVolumeFile;

            if (!UtilidadeImagem.ValidaImagemPorContentType(imagemCapaVolume.ContentType))
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
                var caminhoArquivoImagemVolume = Path.Combine(diretorioImagemVolume, nomeImagemVolume);

                if (volumeDTO.OtimizarImagem)
                {
                    var retorno = RetornaImagemOtimizada(imagemCapaVolume, caminhoArquivoImagemVolume);
                    if (!retorno.IsSuccess)
                        return Result.Fail(retorno.Errors[0].Message);
                }
                else
                {
                    SalvaArquivoFormFile(imagemCapaVolume, caminhoArquivoImagemVolume);
                }

                volumeDTO.DiretorioImagemVolume = diretorioImagemVolume;
                volumeDTO.ImagemVolume = caminhoArquivoImagemVolume;
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
                    if (!UtilidadeImagem.ValidaImagemPorContentType(ilustracaoNovel.ContentType))
                        return Result.Fail("Verifique a extensão da imagem. Extensões permitidas: JPG|JPEG|PNG");

                    var extensaoImagem = Path.GetExtension(ilustracaoNovel.FileName);
                    var nomeImagem = ilustracaoNovel.FileName;
                    var nomeImagemTratada = nomeImagem.Replace(extensaoImagem, "");
                    var nomeArquivo = $"{nomeImagemTratada}.jpg";
                    var urlPaginasCapitulo = Path.Combine(diretorioCapitulo, nomeArquivo);

                    if (capituloDTO.OtimizarImagem)
                    {
                        var retorno = RetornaImagemOtimizada(ilustracaoNovel, urlPaginasCapitulo);
                        if (!retorno.IsSuccess)
                            return Result.Fail(retorno.Errors[0].Message);
                    }
                    else
                    {
                        SalvaArquivoFormFile(ilustracaoNovel, urlPaginasCapitulo);
                    }

                    listaEnderecoImagemDTO.Add(new EnderecoImagemDTO { Id = contador, Ordem = contador, Alt = nomeImagemTratada, Url = urlPaginasCapitulo });

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
                    if (!UtilidadeImagem.ValidaImagemPorContentType(imagemPagina.ContentType))
                        return Result.Fail("Verifique a extensão da imagem. Extensões permitidas: JPG|JPEG|PNG");

                    var nomeArquivo = $"Pagina-{contador:#00}.jpg";
                    var urlPaginasCapitulo = Path.Combine(diretorioCapitulo, nomeArquivo);

                    if (capituloDTO.OtimizarImagem)
                    {
                        var retorno = RetornaImagemOtimizada(imagemPagina, urlPaginasCapitulo);
                        if (!retorno.IsSuccess)
                            return Result.Fail(retorno.Errors[0].Message);
                    }
                    else
                    {
                        SalvaArquivoFormFile(imagemPagina, urlPaginasCapitulo);
                    }

                    listaEnderecoImagemDTO.Add(new EnderecoImagemDTO { Id = contador, Url = urlPaginasCapitulo, Ordem = contador });

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

        private static Result RetornaImagemOtimizada(IFormFile imagemFormFile, string caminhoArquivoImagem)
        {
            var retornoTask = Task.Run(() => OtimizarImagem(imagemFormFile, caminhoArquivoImagem));
            if (!retornoTask.Wait(TimeSpan.FromMinutes(2)))
                return Result.Fail("Erro de Time out ao tentar otimizar as imagens!");

            if (!retornoTask.Result.IsSuccess)
                return Result.Fail(retornoTask.Result.Errors[0].Message);

            return Result.Ok();
        }

        private static async Task<Result> OtimizarImagem(IFormFile imagemFormFile, string caminhoArquivoImagem)
        {
            var byteImagem = UtilidadeImagem.ConverteStreamParaByteArray(imagemFormFile.OpenReadStream());
            var resultByteImagemOtimizada = await OtimizacaoImagemTinify.OtimizarImagem(ConfigurationExternal.RetornaApiKeyTinify(), byteImagem);

            if (!resultByteImagemOtimizada.IsSuccess)
                return Result.Fail("Erro ao carregar bytes de imagem otimização");

            if (!UtilidadeImagem.SalvaArquivoImagem(resultByteImagemOtimizada.Value, caminhoArquivoImagem))
                return Result.Fail("Erro ao tentar salvar o arquivo de imagem otimizada localmente");

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