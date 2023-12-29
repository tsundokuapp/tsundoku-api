using System.Text.RegularExpressions;

namespace TsundokuTraducoes.Helpers
{
    public static class TratamentoDeStrings
    {
        public static string RetornaStringDiretorio(string stringAhSerTratada)
        {
            var stringSlugAuxiliar = RetornaStringSlug(stringAhSerTratada);
            var matrizStringSlugAuxiliar = stringSlugAuxiliar.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
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
            return RetornaStringTratadaComRegex(stringAhSerTratada, "-").ToLower();
        }

        public static string RetornaStringSlugTitleCase(string stringAhSerTratada)
        {
            var stringSlugAuxiliar = RetornaStringSlug(stringAhSerTratada);
            var matrizStringSlugAuxiliar = stringSlugAuxiliar.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
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
            if (!string.IsNullOrEmpty(stringAlteracao))
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

            return string.Empty;
           
        }

        public static bool ValidaCorHexaDecimal(string corHexaDeximal)
        {
            var regexPattern = "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
            return Regex.Match(corHexaDeximal, regexPattern).Success;
        }

        public static string RetornaDescritivoCapitulo(string numero, string parte)
        {
            string descritivoCapitulo;

            if (int.TryParse(numero, out int numeroInt))
            {
                descritivoCapitulo = $"Capítulo {numeroInt:#00}";
            }
            else
            {
                if (double.TryParse(numero, out double numeroDouble))
                {
                    descritivoCapitulo = $"Capítulo {numeroDouble:#00.0}";
                }
                else
                {
                    descritivoCapitulo = numero;
                }
            }

            if (!string.IsNullOrEmpty(parte))
            {
                // TODO - Retirar esse tratamento quando adicionar as validações na controller, tem que ser um valor inteiro
                if (parte.Contains(','))
                {
                    parte = parte.Substring(0, parte.IndexOf(','));
                }
                
                var parteNumero = Convert.ToInt32(parte);
                descritivoCapitulo += $" - Parte {parteNumero:00}";
            }

            return descritivoCapitulo;
        }

        public static string RetornaDescritivoVolume(string numero)
        {
            var unico = (numero.ToLower() == "unico" || numero.ToLower() == "único");
            if (unico)
            {
                return $"Volume Único";
            }
            else
            {
                var numeroVolume = Convert.ToInt32(numero);
                return $"Volume {numeroVolume:00}";
            }
        }
    }
}
