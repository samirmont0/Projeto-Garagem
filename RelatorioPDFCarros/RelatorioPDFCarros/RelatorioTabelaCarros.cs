namespace RelatorioPDFCarros
{
    class RelatorioTabelaCarros
    {
        static void Main(string[] args)
        {
            var montador = new MontadorPDF();

            montador.AbreDocumento();

            //montador.InsereLogoNoDocumento();
            montador.InsereInformacoesNaTabela();

            montador.FechaDocumento();
        }   
    }
}
