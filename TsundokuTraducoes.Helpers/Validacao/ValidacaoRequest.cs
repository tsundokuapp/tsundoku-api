using System.Text.RegularExpressions;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;

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

        public static int RetornaSkipTratado(int? pagina)
        {
            return pagina == null ? 0 : pagina.GetValueOrDefault();
        }

        public static int RetornaTakeTratado(int? obrasPorPagina, bool home = false)
        {
            var valorObrasPorPagina = home == true ? 5 : 4;
            return obrasPorPagina == null ? valorObrasPorPagina : obrasPorPagina.GetValueOrDefault();
        }
    }
}
