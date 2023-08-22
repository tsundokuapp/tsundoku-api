using System.Text.RegularExpressions;
using System;

namespace TsundokuTraducoes.Api.Utilidades
{
    public static class TratamentoDeStrings
    {
        public static string RetornaStringDiretorio(string stringAhSerTratada)
        {
            var stringSlugAuxiliar = RetornaStringSlug(stringAhSerTratada);
            var matrizStringSlugAuxiliar = stringSlugAuxiliar.Split(new string[] { Constantes.Hifen }, StringSplitOptions.RemoveEmptyEntries);
            var stringTratadaTitleCase = string.Empty;
            foreach (var stringSlug in matrizStringSlugAuxiliar)
            {
                var teste = stringSlug[0];
                stringTratadaTitleCase += Char.ToUpperInvariant(stringSlug[0]) + stringSlug.Substring(1);
            }

            return stringTratadaTitleCase;
        }

        public static string RetornaStringSlug(string stringAhSerTratada)
        {
            return RetornaStringTratadaComRegex(stringAhSerTratada, Constantes.Hifen).ToLower();
        }

        public static string RetornaStringSlugTitleCase(string stringAhSerTratada)
        {
            var stringSlugAuxiliar = RetornaStringSlug(stringAhSerTratada);
            var matrizStringSlugAuxiliar = stringSlugAuxiliar.Split(new string[] { Constantes.Hifen }, StringSplitOptions.RemoveEmptyEntries);
            var stringTratadaTitleCase = string.Empty;
            foreach (var stringSlug in matrizStringSlugAuxiliar)
            {
                var teste = stringSlug[0];
                stringTratadaTitleCase += Char.ToUpperInvariant(stringSlug[0]) + stringSlug.Substring(1) + "-";
            }

            stringTratadaTitleCase = stringTratadaTitleCase.Substring(0, stringTratadaTitleCase.Length - 1);

            return stringTratadaTitleCase;
        }

        public static string RetornaStringTratadaComRegex(string stringAlteracao, string caractereDeSubstituicao)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
            {
                stringAlteracao = stringAlteracao.Replace(acentos[i], semAcento[i]);
            }
            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "¹", "²", "³", "£", "¢", "¬", "º", "¨", "\"", "'", ".", ",", "-", ":", "(", ")", "ª", "|", "\\\\", "°", "_", "@", "#", "!", "$", "%", "&", "*", ";", "/", "<", ">", "?", "[", "]", "{", "}", "=", "+", "§", "´", "`", "^", "~" };

            foreach (var caractereEspecial in caracteresEspeciais)
            {
                stringAlteracao = stringAlteracao.Replace(caractereEspecial, "");
            }

            /** Troca os caracteres especiais da string por " " **/
            return Regex.Replace(stringAlteracao, @"[^\w\.@-]", caractereDeSubstituicao,
                                RegexOptions.None, TimeSpan.FromSeconds(1.5)).Trim();
        }

        public static bool ValidaCorHexaDecimal(string corHexaDeximal)
        {
            var retorno = false;
            var regexPattern = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";

            var match = Regex.Match(corHexaDeximal, regexPattern);
            if (match.Success)
                retorno = true;

            return retorno;
        }
    }
}
