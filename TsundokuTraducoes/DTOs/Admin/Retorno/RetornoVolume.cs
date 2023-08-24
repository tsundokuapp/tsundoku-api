using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TsundokuTraducoes.Api.DTOs.Admin.Retorno
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class RetornoVolume
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Numero { get; set; }
        public string DescritivoTituloNumeroVolume
        {
            get
            {
                var unico = (Numero.ToLower() == "unico" || Numero.ToLower() == "único");
                if (unico)
                {
                    return $"Volume Único";
                }
                else
                {
                    var numero = Convert.ToInt32(Numero);
                    return $"Volume {numero:00}";
                }
            }
        }
        public string Sinopse { get; set; }
        public string ImagemVolume { get; set; }
        public string Slug { get; set; }
        public string UsuarioCadastro { get; set; }
        public string UsuarioAlteracao { get; set; }
        public string DataCadastro { get; set; }
        public string DataAlteracao { get; set; }
        public int ObraId { get; set; }
        public List<RetornoCapituloNovel> ListaCapituloNovel { get; set; } = null;
        public List<RetornoCapituloComic> ListaCapituloComic { get; set; } = null;
    }
}