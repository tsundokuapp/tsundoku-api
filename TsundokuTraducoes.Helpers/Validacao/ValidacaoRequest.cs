using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.Imagens;

namespace TsundokuTraducoes.Helpers.Validacao
{
    public static class ValidacaoRequest
    {
        public static bool ValidaImagemCapaPrincipalObra(ObraDTO obraDTO)
        {
            return obraDTO.ImagemCapaPrincipalFile != null;
        }

        public static bool ValidaImagemCapaVolume(VolumeDTO volumeDTO)
        {
            return volumeDTO.ImagemVolumeFile != null;
        }

        public static bool ValidaConteudoTextoCapituloNovel(CapituloDTO capituloDTO)
        {
            return capituloDTO.EhIlustracoesNovel != true ? VerificaString(capituloDTO.ConteudoNovel) : true;
        }

        public static bool ValidaConteudoImagemCapituloNovel(CapituloDTO capituloDTO)
        {
            return capituloDTO.EhIlustracoesNovel == true
                   ? capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0 
                   : true;
        }

        public static bool ValidaDadosRequestCapituloComic(CapituloDTO capituloDTO)
        {
            var resquestValido = capituloDTO.ListaImagensForm != null &&
                capituloDTO.ListaImagensForm.Count > 0;

            return resquestValido;
        }

        public static bool VerificaString(string valor)
        {
            return !string.IsNullOrEmpty(valor) && !valor.Contains("\"\""); ;
        }

        public static bool ValidaCorHexaDecimal(string corHexaDeximal)
        {
            if (!string.IsNullOrEmpty(corHexaDeximal))
            {
                var regexPattern = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
                return Regex.Match(corHexaDeximal, regexPattern).Success;
            }

            return true;
        }

        public static bool VerificaParametrosObras(RequestObras requestObras)
        {
            return ValidaParametrosObra(requestObras);
        }

        public static bool ValidaParametrosObra(RequestObras requestObras)
        {
            return !string.IsNullOrEmpty(requestObras.Pesquisar) ||
                   !string.IsNullOrEmpty(requestObras.Nacionalidade) ||
                   !string.IsNullOrEmpty(requestObras.Status) ||
                   !string.IsNullOrEmpty(requestObras.Tipo) ||
                   !string.IsNullOrEmpty(requestObras.Genero);
        }

        public static int RetornaTakeTratado(int? obrasPorPagina)
        {
            var valorObrasPorPagina = 6;
            return obrasPorPagina == null ? valorObrasPorPagina : obrasPorPagina.GetValueOrDefault();
        }

        public static int RetornaSkipTratado(int? pagina, int obrasPorPagina)
        {
            return pagina == null ? 0 : (pagina.GetValueOrDefault() < 0 ? 0 : pagina.GetValueOrDefault());
        }

        public static int RetornaTakeCapituloTratado(int? obrasPorPagina)
        {
            var valorObrasPorPagina = 1;
            return obrasPorPagina == null ? valorObrasPorPagina : obrasPorPagina.GetValueOrDefault();
        }

        public static int RetornaSkipCapituloTratado(int? pagina, int obrasPorPagina)
        {
            return pagina == null ? 0 : (pagina.GetValueOrDefault() < 0 ? 0 : pagina.GetValueOrDefault());
        }

        public static bool ValidaDadosRequestGenero(GeneroDTO generoDTO)
        {
            var resquestValido = VerificaString(generoDTO.Descricao) &&
                VerificaString(generoDTO.UsuarioInclusao);
            return resquestValido;
        }

        public static bool ValidaDadosRequestGeneroAtualizacao(GeneroDTO generoDTO)
        {
            var resquestValido = VerificaString(generoDTO.Descricao) &&
                VerificaString(generoDTO.UsuarioInclusao);
            VerificaString(generoDTO.UsuarioAlteracao);
            return resquestValido;
        }

        public static bool ValidaImagemRequest(IFormFile imagem)
        {
            var streamImagem = imagem.OpenReadStream();
            var byteImagem = UtilidadeImagem.ConverteStreamParaByteArray(streamImagem);
            var ehImagem = UtilidadeImagem.ValidaImagem(byteImagem);

            return ehImagem;
        }

        public static bool ValidaListaImagemRequest(List<IFormFile> listaImagem)
        {
            var retorno = true;

            foreach (var file in listaImagem)
            {
                if (retorno)
                {
                    var streamImagem = file.OpenReadStream();
                    var byteImagem = UtilidadeImagem.ConverteStreamParaByteArray(streamImagem);
                    retorno = UtilidadeImagem.ValidaImagem(byteImagem);
                }
            }

            return retorno;
        }

        public static bool ValidaListaVolumeCapitulo(RequestObras requestObras)
        {
            return !string.IsNullOrEmpty(requestObras.IdObra) &&
                   requestObras.IdObra.ToString() != "00000000-0000-0000-0000-000000000000";
        }
    }
}