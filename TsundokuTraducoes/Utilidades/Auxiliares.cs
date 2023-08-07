using System;
using System.Threading;
using TsundokuTraducoes.Api.DTOs.Admin;

namespace TsundokuTraducoes.Api.Utilidades
{
    public static class Auxiliares
    {
        public static void Aguarda(int minimo, int maximo)
        {   
            var tempoParaAguardar = new Random().Next(minimo, maximo);
            Thread.Sleep(tempoParaAguardar);
        }

        public static void AdicionaMensagemErro(ResultadoMensagemDTO resultadoMensagemDTO, string mensagemErro)
        {
            resultadoMensagemDTO.Erro = true;
            resultadoMensagemDTO.MensagemErro = mensagemErro;
        }
    }
}
