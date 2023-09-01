﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TsundokuTraducoes.Api.Models.DePara;

namespace TsundokuTraducoes.Api.Models.Generos
{
    public class Genero
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Slug { get; set; }
        public virtual List<GeneroNovel> GenerosNovel { get; set; }
        public virtual List<GeneroComic> GenerosComic { get; set; }

        public Genero()
        {
            GenerosNovel = new List<GeneroNovel>();
            GenerosComic = new List<GeneroComic>();
        }
    }
}
