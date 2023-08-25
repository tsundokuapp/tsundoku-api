using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Utilidades;

namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class CapituloDTO
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Parte { get; set; }
        public string Titulo { get; set; }
        public string DescritivoCapitulo
        {
            get
            {
                double numero;
                var parteAuxiliar = string.Empty;
                var descritivoCapitulo = string.Empty;
                if (double.TryParse(Numero, out numero) && !string.IsNullOrEmpty(Parte))
                {
                    parteAuxiliar = $" - Parte {Parte:00}";
                    descritivoCapitulo = $"Capítulo {Numero:00}{parteAuxiliar}";
                }
                else
                {
                    descritivoCapitulo = Numero;
                }

                return descritivoCapitulo;
            }

        }
        public string ConteudoNovel { get; set; }
        public string ListaImagemCapitulo { get; set; }
        public string Slug
        {
            get
            {
                var parteAuxiliar = string.Empty;
                var tituloAuxiliar = string.Empty;

                if (!string.IsNullOrEmpty(Parte))
                {
                    parteAuxiliar = $"- Parte {Parte.ToLower().Replace("parte", "")}";
                }

                if (!string.IsNullOrEmpty(Titulo))
                {
                    tituloAuxiliar = $" {Titulo}";
                }

                string capituloAuxiliar;
                double numero;
                if (double.TryParse(Numero, out numero))
                {
                    capituloAuxiliar = $"Capitulo {Numero:00}";
                }
                else
                {
                    capituloAuxiliar = Numero;
                }

                var slug = TratamentoDeStrings.RetornaStringSlug($"{capituloAuxiliar}{parteAuxiliar}{tituloAuxiliar}");
                return slug;
            }
        }
        public string UsuarioCadastro { get; set; }
        public string UsuarioAlteracao { get; set; }
        public int VolumeId { get; set; }
        public string Tradutor { get; set; }
        public string Revisor { get; set; }
        public string QC { get; set; }
        public string Editores { get; set; }
        public int OrdemCapitulo { get; set; }
        public bool EhIlustracoesNovel { get; set; }


        public string TituloObra { get; set; }
        public string TipoObraSlug { get; set; }
        public int ObraId { get; set; }
        public Novel Obra { get; set; }
        public string DiretorioImagemCapitulo { get; internal set; }

        public List<IFormFile> ListaImagensForm { get; set; }
    }
}
