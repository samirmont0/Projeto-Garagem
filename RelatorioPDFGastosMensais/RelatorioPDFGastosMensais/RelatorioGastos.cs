using System;

namespace RelatorioPDFGastosMensais
{
    class RelatorioGastos
    {
        static void Main(string[] args)
        {
            var montador = new MontadorPDF();

            montador.AbreDocumento();

            montador.InsereInformacoesNaTabela();

            montador.FechaDocumento();
        }
    }
}
