using Newtonsoft.Json;
using System;
using TsundokuTraducoes.Api.Utilidades;

namespace TsundokuTraducoes.Api.DTOs.Admin.Retorno
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class RetornoCapitulo
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string Parte { get; set; }
        public string Titulo { get; set; }
        public string ConteudoNovel { get; set; }
        public string Slug { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public string DataInclusao { get; set; }
        public string DataAlteracao { get; set; }
        public string DiretorioImagemCapitulo { get; set; }
        public int OrdemCapitulo { get; set; }
        public bool EhIlustracoesNovel { get; set; }
        public Guid VolumeId { get; set; }
        public string Tradutor { get; set; }
        public string Revisor { get; set; }
        public string QC { get; set; }
        public string ListaImagens { get; set; }
        public string DescritivoCapitulo => TratamentoDeStrings.RetornaDescritivoCapitulo(Numero, Parte);
    }   
}
