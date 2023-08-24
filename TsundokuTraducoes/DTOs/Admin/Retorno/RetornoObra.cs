using System;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Utilidades;

namespace TsundokuTraducoes.Api.DTOs.Admin.Retorno
{
    public class RetornoObra
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string TituloAlternativo { get; set; }
        public string Alias { get; set; }
        public string Autor { get; set; }
        public string Artista { get; set; }
        public string Ano { get; set; }
        public string Slug { get; set; }
        public string UsuarioCadastro { get; set; }
        public string UsuarioAlteracao { get; set; }
        public string ImagemCapaPrincipal { get; set; }
        public string Sinopse { get; set; }
        public string DataInclusao { get; set; }
        public string DataAlteracao { get; set; }
        public bool EhObraMaiorIdade { get; set; }
        public string CodigoCorHexaObra { get; set; }
        public string CargoObraDiscord { get; set; }
        public string ImagemBanner { get; set; }
        public string StatusObraSlug { get; set; }
        public string TipoObraSlug { get; set; }
        public string NacionalidadeSlug { get; set; }
        public string StatusObra { get { return SlugAuxiliar.RetornaStatusObraPorSlug(StatusObraSlug); } }
        public string TipoObra { get { return SlugAuxiliar.RetornaTipoObraPorSlug(TipoObraSlug); } }
        public string Nacionalidade { get { return SlugAuxiliar.RetornaNacionalidadePorSlug(NacionalidadeSlug); } }
        public List<RetornoGenero> Generos { get; set; }

        public RetornoObra()
        {
            Generos = new List<RetornoGenero>();
        }
    }
}
