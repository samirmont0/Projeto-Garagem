using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace RelatorioPDFCarros
{
    public class MontadorPDF
    {
        private Document doc;
        private const string caminho = @"C:\MunirVeiculos\Tabela Munir Veiculos.pdf";
        private PdfWriter writer;
        private Font _fonteCarros = new Font(Font.FontFamily.TIMES_ROMAN, 16, 1);

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
            string[] lines = File.ReadAllLines(@"C:\MunirVeiculos\RelacaoDosCarros.txt");

            var logo = RetornaLogo();

            var tabelaLogo = new PdfPTable(1);

            tabelaLogo.AddCell(new PdfPCell(logo, false)
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


            var tabela = new PdfPTable(new float[] 
            { 
                70,
                14.5f,
                15.5f
            });

            tabela.DefaultCell.Colspan = 3;
            tabela.DefaultCell.Border = Rectangle.NO_BORDER;
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabela.AddCell(tabelaLogo);

            tabela.WidthPercentage = 100;
            tabela.DefaultCell.Border = Rectangle.BOX;
            tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabela.DefaultCell.Colspan = 1;
            tabela.HeaderRows = 1;


            var contador = 0;
            foreach (var line in lines)
            {
                if(contador % 2 == 0)
                {
                    tabela.DefaultCell.BackgroundColor = new BaseColor(207, 211, 213);
                }
                else
                {
                    tabela.DefaultCell.BackgroundColor = BaseColor.WHITE;
                }
                string[] a = line.Split('|');

                for (int i = 0; i < 3; i++)
                {
                    if (i == 1)
                    {
                        tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    tabela.AddCell(new Phrase(a[i], _fonteCarros));
                    tabela.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                }
                contador++;
            }
            InsereTabelaNoDocumento(tabela);
        }
    }
}
