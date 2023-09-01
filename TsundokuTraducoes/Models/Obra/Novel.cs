using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TsundokuTraducoes.Api.Models.DePara;
using TsundokuTraducoes.Api.Models.Volume;

namespace TsundokuTraducoes.Api.Models.Obra
{
    public class Novel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string TituloAlternativo { get; set; }
        public string Alias { get; set; }
        public string Autor { get; set; }
        public string Artista { get; set; }
        public string Ano { get; set; }
        public string Slug { get; set; }
        public int Visualizacoes { get; set; }
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        public string ImagemCapaPrincipal { get; set; }
        public string Sinopse { get; set; }
        public DateTime DataInclusao { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; }
        public bool EhObraMaiorIdade { get; set; }
        public bool EhRecomendacao { get; set; }
        public string CodigoCorHexaObra { get; set; }
        public string ImagemBanner { get; set; }
        public string CargoObraDiscord { get; set; }
        public string DiretorioImagemObra { get; set; }
        public string StatusObraSlug { get; set; }
        public string TipoObraSlug { get; set; }
        public string NacionalidadeSlug { get; set; }
        public string ImagemCapaUltimoVolume { get; set; }
        public string NumeroUltimoVolume { get; set; }
        public string SlugUltimoVolume { get; set; }
        public string NumeroUltimoCapitulo { get; set; }
        public string SlugUltimoCapitulo { get; set; }
        public DateTime? DataAtualizacaoUltimoCapitulo { get; set; }

        public virtual List<VolumeNovel> Volumes { get; set; }
        public virtual List<GeneroNovel> GenerosNovel { get; set; }

        public Novel()
        {
            Volumes = new List<VolumeNovel>();
            GenerosNovel = new List<GeneroNovel>();
        }
    }
}