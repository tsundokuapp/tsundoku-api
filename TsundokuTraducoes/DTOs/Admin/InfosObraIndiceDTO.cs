using System.Collections.Generic;
using System;
using TsundokuTraducoes.Models;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class InfosObraIndiceDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string TituloAlternativo { get; set; }
        public string AutorObra { get; set; }
        public string Artista { get; set; }
        public string Ano { get; set; }
        public string Slug { get; set; }
        public int Visualizacoes { get; set; }
        public string UsuarioCadastro { get; set; }
        public string UsuarioAlteracao { get; set; }
        public string EnderecoUrlCapa { get; set; }
        public string Sinopse { get; set; }
        public bool EhObraMaiorIdade { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int StatusObraId { get; set; }
        public string StatusObra { get; set; }
        public int TipoObraId { get; set; }
        public string TipoObra { get; set; }
        public List<Volume> Volumes { get; set; }
        public List<string> Generos { get; set; }
        public string Alias { get; set; }
        public string CodigoCorHexaIndice { get; set; }
        public string Nacionalidade { get; set; }
    }
}
