using System.IO;
using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace RelatorioPDFGastosMensais
{
    public class MontadorPDF
    {
        private Document doc;
        private const string caminho = @"C:\MunirVeiculos\GastosMensais.pdf";
        private PdfWriter writer;
        private Font _fonteCabecalho = new Font(Font.FontFamily.TIMES_ROMAN, 14, 1, BaseColor.RED);
        private Font _fonteCarros = new Font(Font.FontFamily.TIMES_ROMAN, 14, 1);

        public MontadorPDF()
        {
            doc = new Document(PageSize.A4);
            doc.SetMargins(40, 40, 0, 40);
            writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));
        }
        public void AbreDocumento()
        {
            doc.Open();
        }


        public void FechaDocumento()
        {
            doc.Close();
        }

        public void InsereTabelaNoDocumento(PdfPTable tabela)
        {
            doc.Add(tabela);
        }

        public Image RetornaLogo()
        {
            Image logo = Image.GetInstance(@"C:\MunirVeiculos\imagemLogo\LogoMunirVeiculos.jpeg");
            logo.Alignment = Element.ALIGN_CENTER;
            logo.ScaleAbsolute(340, 140);

            return logo;
        }

        public void InsereInformacoesNaTabela()
        {
            double total = 0;
            double tot = 0;
            double valor = 0;
            string[] lines = File.ReadAllLines(@"C:\MunirVeiculos\GastosMensais.txt");

            var logo = RetornaLogo();

            var tabelaLogo = new PdfPTable(1);

            tabelaLogo.AddCell(new PdfPCell(logo, false)
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


            var tabela = new PdfPTable(new float[]
            {
                83,
                17
            });

            tabela.DefaultCell.Colspan = 3;
            tabela.DefaultCell.Border = Rectangle.NO_BORDER;
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabela.AddCell(tabelaLogo);
            tabela.AddCell(new Phrase("Gastos Mensais\n\n", _fonteCabecalho));

            tabela.WidthPercentage = 100;
            tabela.DefaultCell.Border = Rectangle.BOX;
            tabela.DefaultCell.Colspan = 1;
            tabela.HeaderRows = 1;

            foreach (var line in lines)
            {
                string[] a = line.Split('|');
                Double.TryParse(a[1], out tot);
                total += tot;

                for (int i = 0; i < 2; i++)
                {
                    tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    if (i == 0)
                    {
                        tabela.AddCell(new Phrase(a[i], _fonteCarros));
                    }
                    else
                    {
                        Double.TryParse(a[1], out valor);
                        tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        tabela.AddCell(new Phrase($"{valor.ToString("C")}", _fonteCarros));
                    }
                }
            }
            tabela.DefaultCell.Colspan = 1;
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabela.AddCell(new Phrase("Total:", _fonteCarros));
            tabela.AddCell(new Phrase($"{total.ToString("C")}", _fonteCarros));
            InsereTabelaNoDocumento(tabela);
        }
    }
}
