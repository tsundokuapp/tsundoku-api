using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TsundokuTraducoes.Api.Models
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Slug { get; set; }

        public virtual List<GeneroObra> GenerosObra { get; set; }

        public Genero()
        {   
            GenerosObra = new List<GeneroObra>();
        }
    }
}
