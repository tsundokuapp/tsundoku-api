using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TsundokuTraducoes.Helpers.DTOs.Admin
{
    public class CapituloDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Numero { get; set; }
        public string Parte { get; set; }
        public string Titulo { get; set; }
        public string ConteudoNovel { get; set; }
        public string ListaImagensJson { get; set; }
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
        [Required]
        public string UsuarioInclusao { get; set; }
        public string UsuarioAlteracao { get; set; }
        [Required]
        public Guid VolumeId { get; set; }
        public string Tradutor { get; set; }
        public string Revisor { get; set; }
        public string QC { get; set; }
        public string Editores { get; set; }
        public int OrdemCapitulo { get; set; }
        public bool EhIlustracoesNovel { get; set; }
        public List<IFormFile> ListaImagensForm { get; set; }
        public string DiretorioImagemCapitulo { get; set; }
        public bool OtimizarImagem { get; set; }
        public bool SalvarLocal { get; set; }
        public bool Publicado { get; set; }
    }
}