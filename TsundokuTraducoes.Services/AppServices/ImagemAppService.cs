using FluentResults;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Helpers.Configuration;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.Imagens;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{
    public class ImagemAppService : IImagemAppService
    {
        public async Task<Result> ProcessaUploadCapaObra(ObraDTO obraDTO, bool alterarImagem)
        {
            var servicoAmazon = new ServicosImagemAmazonS3();
            var result = new Result<byte[]>();
            var resultImagem = new Result<ImagemDTO>();
            var imagemCapaPrincipal = obraDTO.ImagemCapaPrincipalFile;

            if (!UtilidadeImagem.ValidaImagemPorContentType(imagemCapaPrincipal.ContentType))
                return Result.Fail("Verifique a extensão da imagem da capa. Extensões permitidas: JPG|JPEG|PNG");

            var nomeDiretorioImagensObra = TratamentoDeStrings.RetornaStringDiretorio(obraDTO.Alias);
            var nomeArquivoImagem = $"Capa-Obra-{TratamentoDeStrings.RetornaStringSlugTitleCase(obraDTO.Alias)}.jpg";

            if (obraDTO.OtimizarImagem)
            {
                result = RetornaImagemOtimizada(imagemCapaPrincipal);
                if (!result.IsSuccess)
                    return Result.Fail(result.Errors[0].Message);
            }

            var arrayByteImagem = RetornaByteArray(imagemCapaPrincipal, result);

            if (obraDTO.SalvarLocal)
            {
                resultImagem = SalvaArquivoImagemLocal(nomeDiretorioImagensObra, nomeArquivoImagem, arrayByteImagem);
            }
            else
            {
                nomeDiretorioImagensObra = nomeDiretorioImagensObra + "/";
                resultImagem = await RealizaUploadImagemS3(alterarImagem, servicoAmazon, nomeDiretorioImagensObra, nomeArquivoImagem, arrayByteImagem);
                if (!resultImagem.IsSuccess)
                    return Result.Fail(resultImagem.Errors[0].Message);
            }

            obraDTO.DiretorioImagemObra = resultImagem.Value.Diretorio;
            obraDTO.ImagemCapaPrincipal = resultImagem.Value.Url;

            return Result.Ok();
        }

        public async Task<Result> ProcessaUploadBannerObra(ObraDTO obraDTO, bool alterarImagem)
        {
            var servicoAmazon = new ServicosImagemAmazonS3();
            var result = new Result<byte[]>();
            var imagemBanner = obraDTO.ImagemBannerFile;
            var resultImagem = new Result<ImagemDTO>();

            if (!UtilidadeImagem.ValidaImagemPorContentType(imagemBanner.ContentType))
                return Result.Fail("Verifique a extensão da imagem do banner. Extensões permitidas: JPG|JPEG|PNG");

            string nomeArquivoImagem;

            nomeArquivoImagem = $"Banner-Obra-{TratamentoDeStrings.RetornaStringSlugTitleCase(obraDTO.Titulo)}.jpg";
            var caminhoArquivoImagemBanner = Path.Combine(obraDTO.DiretorioImagemObra, nomeArquivoImagem);

            if (obraDTO.OtimizarImagem)
            {
                result = RetornaImagemOtimizada(imagemBanner);
                if (!result.IsSuccess)
                    return Result.Fail(result.Errors[0].Message);
            }

            var arrayByteImagem = RetornaByteArray(imagemBanner, result);

            if (obraDTO.SalvarLocal)
            {
                resultImagem = SalvaArquivoImagemLocal(obraDTO.DiretorioImagemObra, nomeArquivoImagem, arrayByteImagem);
            }
            else
            {
                resultImagem = await RealizaUploadImagemS3(alterarImagem, servicoAmazon, obraDTO.DiretorioImagemObra, nomeArquivoImagem, arrayByteImagem);
                if (!resultImagem.IsSuccess)
                    return Result.Fail(resultImagem.Errors[0].Message);
            }

            obraDTO.ImagemBanner = resultImagem.Value.Url;

            return Result.Ok();
        }

        public async Task<Result> ProcessaUploadCapaVolume(VolumeDTO volumeDTO, string numeroVolume, string diretorioImagemObra, bool alterarImagem)
        {
            var result = new Result<byte[]>();
            var imagemCapaVolume = volumeDTO.ImagemVolumeFile;
            var servicoAmazon = new ServicosImagemAmazonS3();
            var resultImagem = new Result<ImagemDTO>();

            if (!UtilidadeImagem.ValidaImagemPorContentType(imagemCapaVolume.ContentType))
                return Result.Fail("Verifique a extensão da imagem. Extensões permitidas: JPG|JPEG|PNG");

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

            if (volumeDTO.OtimizarImagem)
            {
                result = RetornaImagemOtimizada(imagemCapaVolume);
                if (!result.IsSuccess)
                    return Result.Fail(result.Errors[0].Message);
            }

            var arrayByteImagem = RetornaByteArray(imagemCapaVolume, result);
            var nomeImagemVolume = $"Capa-{tituloVolumeTratado}.jpg";
            string diretorioImagemVolume;

            if (volumeDTO.SalvarLocal)
            {
                if (Directory.Exists(diretorioImagemObra))
                {
                    diretorioImagemVolume = Diretorios.RetornaDiretorioImagemCriado(diretorioImagemObra, $"{TratamentoDeStrings.RetornaStringDiretorio(tituloVolumeTratado.Replace("-", " "))}");
                    resultImagem = SalvaArquivoImagemLocal(diretorioImagemVolume, nomeImagemVolume, arrayByteImagem);
                }
                else
                {
                    return Result.Fail("Diretório da obra não encontrado!");
                }
            }
            else
            {
                diretorioImagemVolume = diretorioImagemObra + TratamentoDeStrings.RetornaStringDiretorio(tituloVolumeTratado.Replace("-", " ")) + "/";
                resultImagem = await RealizaUploadImagemS3(alterarImagem, servicoAmazon, diretorioImagemVolume, nomeImagemVolume, arrayByteImagem);
                if (!resultImagem.IsSuccess)
                    return Result.Fail(resultImagem.Errors[0].Message);
            }

            volumeDTO.DiretorioImagemVolume = resultImagem.Value.Diretorio;
            volumeDTO.ImagemVolume = resultImagem.Value.Url;

            return Result.Ok();
        }

        public async Task<Result> ProcessaUploadListaImagensCapituloNovel(CapituloDTO capituloDTO, VolumeNovel volume, bool alterarImagem ,int? ordemPaginaImagem = null)
        {
            var result = new Result<byte[]>();
            var nomeDiretorioCapitulo = capituloDTO.Slug;
            var servicoAmazon = new ServicosImagemAmazonS3();
            var resultImagem = new Result<ImagemDTO>();

            // TODO - Adicionar esse tratamento posteriormente. A ordem ordem é igual ao ID, ao menos que alguém informe.
            var contador = 1;
            var listaEnderecoImagemDTO = new List<EnderecoImagemDTO>();

            string diretorioCapitulo;
            if (capituloDTO.SalvarLocal)
            {
                if (Directory.Exists(volume.DiretorioImagemVolume))
                {
                    diretorioCapitulo = Diretorios.RetornaDiretorioImagemCriado(volume.DiretorioImagemVolume, TratamentoDeStrings.RetornaStringDiretorio(nomeDiretorioCapitulo.Replace("-", " ")));
                }
                else
                {
                    return Result.Fail("Não foi encontrado o diretório do volume!");
                }
            }
            else
            {
                diretorioCapitulo = volume.DiretorioImagemVolume + TratamentoDeStrings.RetornaStringDiretorio(nomeDiretorioCapitulo.Replace("-", " ")) + "/";
            }

            foreach (var ilustracaoNovel in capituloDTO.ListaImagensForm)
            {
                if (!UtilidadeImagem.ValidaImagemPorContentType(ilustracaoNovel.ContentType))
                    return Result.Fail("Verifique a extensão da imagem. Extensões permitidas: JPG|JPEG|PNG");

                var extensaoImagem = Path.GetExtension(ilustracaoNovel.FileName);
                var nomeImagem = ilustracaoNovel.FileName;
                var nomeImagemTratada = nomeImagem.Replace(extensaoImagem, "");
                var nomeArquivo = $"{nomeImagemTratada}.jpg";

                if (capituloDTO.OtimizarImagem)
                {
                    result = RetornaImagemOtimizada(ilustracaoNovel);
                    if (!result.IsSuccess)
                        return Result.Fail(result.Errors[0].Message);
                }

                var arrayByteImagem = RetornaByteArray(ilustracaoNovel, result);

                if (capituloDTO.SalvarLocal)
                {
                    resultImagem = SalvaArquivoImagemLocal(diretorioCapitulo, nomeArquivo, arrayByteImagem);
                }
                else
                {
                    resultImagem = await RealizaUploadImagemS3(alterarImagem, servicoAmazon, diretorioCapitulo, nomeArquivo, arrayByteImagem);
                    if (!resultImagem.IsSuccess)
                        return Result.Fail(resultImagem.Errors[0].Message);
                }

                listaEnderecoImagemDTO.Add(new EnderecoImagemDTO { Id = contador, Ordem = contador, Alt = nomeImagemTratada, Url = resultImagem.Value.Url });
                contador++;
            }

            var imagensJson = JsonConvert.SerializeObject(listaEnderecoImagemDTO);
            capituloDTO.DiretorioImagemCapitulo = resultImagem.Value.Diretorio;
            capituloDTO.ConteudoNovel = imagensJson;

            return Result.Ok();
        }

        public async Task<Result> ProcessaUploadListaImagensCapituloManga(CapituloDTO capituloDTO, VolumeComic volume, bool alterarImagem, int? ordemPaginaImagem = null)
        {
            var result = new Result<byte[]>();
            var nomeDiretorioCapitulo = capituloDTO.Slug;
            var servicoAmazon = new ServicosImagemAmazonS3();
            var resultImagem = new Result<ImagemDTO>();

            // TODO - Adicionar esse tratamento posteriormente. A ordem ordem é igual ao ID, ao menos que alguém informe.
            var contador = 1;
            var listaEnderecoImagemDTO = new List<EnderecoImagemDTO>();

            string diretorioCapitulo;
            if (capituloDTO.SalvarLocal)
            {
                if (Directory.Exists(volume.DiretorioImagemVolume))
                {
                    diretorioCapitulo = Diretorios.RetornaDiretorioImagemCriado(volume.DiretorioImagemVolume, TratamentoDeStrings.RetornaStringDiretorio(nomeDiretorioCapitulo.Replace("-", " ")));
                }
                else
                {
                    return Result.Fail("Não foi encontrado o diretório do volume!");
                }
            }
            else
            {
                diretorioCapitulo = volume.DiretorioImagemVolume + TratamentoDeStrings.RetornaStringDiretorio(nomeDiretorioCapitulo.Replace("-", " ")) + "/";
            }

            foreach (var ilustracaoNovel in capituloDTO.ListaImagensForm)
            {
                if (!UtilidadeImagem.ValidaImagemPorContentType(ilustracaoNovel.ContentType))
                    return Result.Fail("Verifique a extensão da imagem. Extensões permitidas: JPG|JPEG|PNG");

                var nomeArquivo = $"Pagina-{contador:#00}.jpg";
                var urlPaginasCapitulo = Path.Combine(diretorioCapitulo, nomeArquivo);

                if (capituloDTO.OtimizarImagem)
                {
                    result = RetornaImagemOtimizada(ilustracaoNovel);
                    if (!result.IsSuccess)
                        return Result.Fail(result.Errors[0].Message);
                }

                var arrayByteImagem = RetornaByteArray(ilustracaoNovel, result);

                if (capituloDTO.SalvarLocal)
                {
                    resultImagem = SalvaArquivoImagemLocal(diretorioCapitulo, nomeArquivo, arrayByteImagem);                   
                }
                else
                {
                    resultImagem = await RealizaUploadImagemS3(alterarImagem, servicoAmazon, diretorioCapitulo, nomeArquivo, arrayByteImagem);
                    if (!resultImagem.IsSuccess)
                        return Result.Fail(resultImagem.Errors[0].Message);
                }

                listaEnderecoImagemDTO.Add(new EnderecoImagemDTO { Id = contador, Url = resultImagem.Value.Url, Ordem = contador });
                contador++;
            }

            var imagensJson = JsonConvert.SerializeObject(listaEnderecoImagemDTO);
            capituloDTO.ListaImagemCapitulo = imagensJson;
            capituloDTO.DiretorioImagemCapitulo = resultImagem.Value.Diretorio;

            return Result.Ok();
        }

        public async Task<bool> ExcluiDiretorioImagens(string diretorioImagens, bool diretorioLocal)
        {
            bool diretorioExcluido;
            
            if (diretorioLocal) 
            {
                if (Directory.Exists(diretorioImagens))
                {
                    Directory.Delete(diretorioImagens, true);
                }

                diretorioExcluido = true;
            }
            else
            {
                var servicoAmazon = new ServicosImagemAmazonS3();
                diretorioExcluido = await servicoAmazon.ExcluiObjetoBucket(diretorioImagens);
            }

            return diretorioExcluido;
        }

        private static byte[] RetornaByteArray(IFormFile imagemCapaPrincipal, Result<byte[]> result)
        {
            var arrayByteImagem = result.Value;
            arrayByteImagem ??= UtilidadeImagem.ConverteStreamParaByteArray(imagemCapaPrincipal.OpenReadStream());
            return arrayByteImagem;
        }

        private static Result<byte[]> RetornaImagemOtimizada(IFormFile imagemFormFile)
        {
            var retornoTask = Task.Run(() => OtimizarImagem(imagemFormFile));
            if (!retornoTask.Wait(TimeSpan.FromMinutes(2)))
                return Result.Fail("Erro de Time out ao tentar otimizar as imagens!");

            if (!retornoTask.Result.IsSuccess)
                return Result.Fail(retornoTask.Result.Errors[0].Message);

            return Result.Ok(retornoTask.Result.Value);
        }

        private static async Task<Result<byte[]>> OtimizarImagem(IFormFile imagemFormFile)
        {
            var byteImagem = UtilidadeImagem.ConverteStreamParaByteArray(imagemFormFile.OpenReadStream());
            var resultByteImagemOtimizada = await OtimizacaoImagemTinify.OtimizarImagem(ConfigurationExternal.RetornaApiKeyTinify(), byteImagem);

            if (!resultByteImagemOtimizada.IsSuccess)
                return Result.Fail("Erro ao carregar bytes de imagem otimização");

            return Result.Ok(resultByteImagemOtimizada.Value);
        }

        private static Result<ImagemDTO> SalvaArquivoImagemLocal(string nomeDiretorioImagens, string nomeArquivoImagem, byte[] arrayByteImagem)
        {
            var imagemDTO = new ImagemDTO();
            var diretorioImagemObra = Diretorios.RetornaDiretorioImagemCriado(nomeDiretorioImagens);
            var caminhoArquivoImagem = Path.Combine(diretorioImagemObra, nomeArquivoImagem);

            if (!UtilidadeImagem.SalvaArquivoImagem(arrayByteImagem, caminhoArquivoImagem))
                return Result.Fail("Erro ao tentar salvar o arquivo de imagem otimizada localmente");

            imagemDTO.Diretorio = diretorioImagemObra;
            imagemDTO.Url = caminhoArquivoImagem;

            return Result.Ok(imagemDTO);
        }

        private static async Task<Result<ImagemDTO>> RealizaUploadImagemS3(bool alterarImagem, ServicosImagemAmazonS3 servicoAmazon, string nomeDiretorioImagens, string nomeArquivoImagem, byte[] arrayByteImagem)
        {
            var imagemDTO = new ImagemDTO();
            var pastaCriada = false;
            var existePasta = await servicoAmazon.VerificaObjetoExistenteAwsS3(nomeDiretorioImagens);

            if (!existePasta)
            {
                pastaCriada = await servicoAmazon.CriarPastaS3(nomeDiretorioImagens);
            }
            else
            {
                pastaCriada = true;
            }

            if (!pastaCriada)
                return Result.Fail("Problema ao criar pasta/folder na Amazon S3");

            var stream = new MemoryStream(arrayByteImagem);
            var caminhoCompletoImagem = $"{nomeDiretorioImagens}{nomeArquivoImagem}";

            var imagemUpada = await servicoAmazon.UploadImagem(stream, caminhoCompletoImagem, alterarImagem);

            if (!imagemUpada)
                return Result.Fail("Problema ao realizar upload de imagem na Amazon S3");

            imagemDTO.Diretorio = nomeDiretorioImagens;
            imagemDTO.Url = $"{servicoAmazon._distributionDomainName}{caminhoCompletoImagem}";

            return Result.Ok(imagemDTO);
        }
    }
}