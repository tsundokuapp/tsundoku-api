using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.Utilidades;

namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class CapituloDTO : UploadImagemDTO
    {
        public int Id { get; set; }
        public double Numero { get; set; }
        public string Parte { get; set; }
        public string Titulo { get; set; }
        public string DescritivoCapitulo
        {
            get
            {
                var parteAuxiliar = string.Empty;
                if (!string.IsNullOrEmpty(Parte))
                {
                    parteAuxiliar = $" - Parte {Parte:00}";
                }

                var descritivoCapitulo = $"Capítulo {Numero:00}{parteAuxiliar}";

                return descritivoCapitulo;
            }

        }
        public string ConteudoNovel { get; set; }
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

                var slug = TratamentoDeStrings.RetornaStringSlug($"Capitulo {Numero:00}{parteAuxiliar}{tituloAuxiliar}");
                return slug;
            }
        }
        public string UsuarioCadastro { get; set; }
        public string UsuarioAlteracao { get; set; }
        public int VolumeId { get; set; }
        public string Tradutor { get; set; }
        public string Revisor { get; set; }
        public string QC { get; set; }
        public int OrdemCapitulo { get; set; }
        public bool EhIlustracoesNovel { get; set; }


        public string TituloObra { get; set; }
        public string TipoObraSlug { get; set; }
        public int ObraId { get; set; }
        public Obra Obra { get; set; }        
    }
}
