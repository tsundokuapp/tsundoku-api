using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.Imagens;

namespace TsundokuTraducoes.Helpers.Validacao
{
    public static class ValidacaoRequest
    {
        public static bool ValidaDadosRequestObra(ObraDTO obraDTO)
        {
            var resquestValido = VerificaString(obraDTO.Titulo) &&
                VerificaString(obraDTO.Alias) &&
                VerificaString(obraDTO.TituloAlternativo) &&
                VerificaString(obraDTO.Autor) &&
                VerificaString(obraDTO.Ano) &&
                VerificaString(obraDTO.UsuarioInclusao) &&
                VerificaString(obraDTO.Sinopse) &&
                VerificaString(obraDTO.CodigoCorHexaObra) &&
                VerificaString(obraDTO.NacionalidadeSlug) &&
                VerificaString(obraDTO.StatusObraSlug) &&
                VerificaString(obraDTO.TipoObraSlug) &&
                obraDTO.ListaGeneros.Count > 0 &&
                obraDTO.ImagemCapaPrincipalFile != null;

            return resquestValido;
        }

        public static bool ValidaDadosRequestObraAtualizacao(ObraDTO obraDTO)
        {
            var resquestValido = VerificaString(obraDTO.Titulo) &&
                VerificaString(obraDTO.Alias) &&
                VerificaString(obraDTO.TituloAlternativo) &&
                VerificaString(obraDTO.Autor) &&
                VerificaString(obraDTO.Ano) &&
                VerificaString(obraDTO.UsuarioInclusao) &&
                VerificaString(obraDTO.UsuarioAlteracao) &&
                VerificaString(obraDTO.Sinopse) &&
                VerificaString(obraDTO.CodigoCorHexaObra) &&
                VerificaString(obraDTO.NacionalidadeSlug) &&
                VerificaString(obraDTO.StatusObraSlug) &&
                VerificaString(obraDTO.TipoObraSlug) &&
                obraDTO.ListaGeneros.Count > 0;

            return resquestValido;
        }

        public static bool ValidaDadosRequestVolume(VolumeDTO volumeDTO)
        {
            var resquestValido = VerificaString(volumeDTO.Numero) &&
                VerificaString(volumeDTO.UsuarioInclusao) &&
                volumeDTO.ObraId.ToString() != "00000000-0000-0000-0000-000000000000" &&
                volumeDTO.ImagemVolumeFile != null;

            return resquestValido;
        }

        public static bool ValidaDadosRequestVolumeAtualizacao(VolumeDTO volumeDTO)
        {
            var resquestValido = VerificaString(volumeDTO.Numero) &&
                VerificaString(volumeDTO.UsuarioInclusao) &&
                VerificaString(volumeDTO.UsuarioAlteracao) &&
                volumeDTO.ObraId.ToString() != "00000000-0000-0000-0000-000000000000";

            return resquestValido;
        }

        public static bool ValidaDadosRequestCapituloNovel(CapituloDTO capituloDTO)
        {
            var requestListaImagemForm = true;
            var resquestValidoEhConteudoNovel = true;

            var resquestValido =
                VerificaString(capituloDTO.Numero) &&
                VerificaString(capituloDTO.UsuarioInclusao) &&
                capituloDTO.OrdemCapitulo > 0 &&
                capituloDTO.VolumeId.ToString() != "00000000-0000-0000-0000-000000000000";

            if (!capituloDTO.EhIlustracoesNovel)
                resquestValidoEhConteudoNovel = VerificaString(capituloDTO.ConteudoNovel);

            if (capituloDTO.EhIlustracoesNovel)
            {
                requestListaImagemForm = capituloDTO.ListaImagensForm != null &&
                capituloDTO.ListaImagensForm.Count > 0;
            }

            return resquestValido && requestListaImagemForm && resquestValidoEhConteudoNovel;
        }

        public static bool ValidaDadosRequestCapituloComic(CapituloDTO capituloDTO)
        {
            var resquestValido =
                VerificaString(capituloDTO.Numero) &&
                VerificaString(capituloDTO.UsuarioInclusao) &&
                capituloDTO.OrdemCapitulo > 0 &&
                capituloDTO.VolumeId.ToString() != "00000000-0000-0000-0000-000000000000" &&
                capituloDTO.ListaImagensForm != null &&
                capituloDTO.ListaImagensForm.Count > 0;

            return resquestValido;
        }

        public static bool ValidaDadosRequestCapituloAtualizacao(CapituloDTO capituloDTO)
        {
            var resquestValido =
                VerificaString(capituloDTO.Numero) &&
                VerificaString(capituloDTO.UsuarioInclusao) &&
                capituloDTO.OrdemCapitulo > 0 &&
                capituloDTO.VolumeId.ToString() != "00000000-0000-0000-0000-000000000000" &&
                VerificaString(capituloDTO.UsuarioAlteracao);

            return resquestValido;
        }        

        public static bool VerificaString(string valor)
        {
            return !string.IsNullOrEmpty(valor) && !valor.Contains("\"\""); ;
        }

        public static bool ValidaCorHexaDecimal(string corHexaDeximal)
        {
            var regexPattern = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
            return Regex.Match(corHexaDeximal, regexPattern).Success;
        }

        public static bool ValidaParametrosNovel(RequestObras requestObras)
        {
            return ValidaParametrosObra(requestObras);
        }

        public static bool ValidaParametrosObra(RequestObras requestObras)
        {
            return !string.IsNullOrEmpty(requestObras.Pesquisar) ||
                   !string.IsNullOrEmpty(requestObras.Nacionalidade) ||
                   !string.IsNullOrEmpty(requestObras.Status) ||
                   !string.IsNullOrEmpty(requestObras.Tipo) ||
                   !string.IsNullOrEmpty(requestObras.Genero) ||
                   requestObras.Skip != null ||
                   requestObras.Take != null;
        }

        public static int RetornaTakeTratadoAdmin(int? obrasPorPagina)
        {
            var valorObrasPorPagina = 8;
            return obrasPorPagina == null ? valorObrasPorPagina : obrasPorPagina.GetValueOrDefault();
        }

        public static int RetornaSkipTratadoAdmin(int? pagina)
        {
            return pagina == null ? 0 : pagina.GetValueOrDefault();
        }

        public static int RetornaTakeTratado(int? obrasPorPagina, bool home = false)
        {
            var valorObrasPorPagina = home == true ? 5 : 4;
            return obrasPorPagina == null ? valorObrasPorPagina : obrasPorPagina.GetValueOrDefault();
        }

        public static int RetornaSkipTratado(int? pagina)
        {
            return pagina == null ? 0 : pagina.GetValueOrDefault();
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