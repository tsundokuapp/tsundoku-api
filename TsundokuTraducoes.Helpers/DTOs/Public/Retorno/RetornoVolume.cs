﻿namespace TsundokuTraducoes.Helpers.DTOs.Public.Retorno
{
    public class RetornoVolume
    {
        public Guid IdObra { get; set; }
        public Guid Id { get; set; }
        public string NumeroVolume { get; set; }
        public string UrlCapaVolume { get; set; }
        public string SlugVolume { get; set; }
        public DateTime DataInclusao { get; set; }
        public string Sinopse { get; set; }
        public IEnumerable<RetornoCapitulo> ListaCapitulos { get; set; }
    }
}