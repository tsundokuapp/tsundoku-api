using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Utils;

namespace TsundokuTraducoes.Domain.Interfaces.Services;

public interface IAwsS3Service
{
    Task<Result<ImagemDTO>> RealizaUploadImagemS3(ParametroAwsServiceDto parametrosAmazonServiceDto);
    Task<bool> VerificaObjetoExistenteAwsS3(string nomePasta);
    Task<bool> CriarPastaS3(string nomePasta);
    Task<bool> UploadImagem(Stream stream, string caminhoCompletoImagem, bool alterarImagem);
    Task<bool> AtualizaCache(string caminhoCompletoImagem);
    Task<bool> ExcluiObjetoBucket(string keyName);
}