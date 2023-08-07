using System;
using System.Threading;

namespace TsundokuTraducoes.Api.Utilidades
{
    public static class Auxiliares
    {
        public static void Aguarda(int minimo, int maximo)
        {   
            var tempoParaAguardar = new Random().Next(minimo, maximo);
            Thread.Sleep(tempoParaAguardar);
        }
    }
}
