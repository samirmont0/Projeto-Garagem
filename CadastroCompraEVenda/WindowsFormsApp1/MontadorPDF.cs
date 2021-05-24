using iTextSharp.text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ContratoCompraEVenda
{
    public class MontadorPDF
    {
        public Document doc;
        Cliente _cliente;
        string caminho;
        private Font _fonteNegrito = FontFactory.GetFont("Arial", 13, Font.BOLD);
        private Font _fonte = FontFactory.GetFont("Arial", 13, Font.NORMAL);

        private PdfWriter writer;

        public MontadorPDF(Cliente cliente)
        {
            _cliente = cliente;

            doc = new Document(PageSize.A4);
            doc.AddCreationDate();
            string[] nomeCliente = _cliente.Nome.Split( );
            caminho = @"C:\Programa Garagem\ContratosClientes\" + nomeCliente[0] + ".pdf";
            writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));
        }

        public void AbrePDF()
        {
            System.Diagnostics.Process.Start(caminho);
        }

        public void AbreDocumento()
        {
            doc.Open();
        }

        public void AlteraMargemDocumento(int Esquerda, int Direita, int Cima, int Baixo)
        {
            doc.SetMargins(Esquerda, Direita, Cima, Baixo);
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
            Image logo = Image.GetInstance(@"C:\Users\samir\Desktop\ProjetoGaragemCarro\LogoMunir.jpeg");
            logo.Alignment = Element.ALIGN_CENTER;
            logo.ScaleAbsolute(340, 140);

            return logo;
        }

        public void InsereLogo()
        {
            var logo = RetornaLogo();

            var tabelaLogo = new PdfPTable(1);

            tabelaLogo.AddCell(new PdfPCell(logo, false)
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            InsereTabelaNoDocumento(tabelaLogo);
        }

        public void SeccaoCabecalho()
        {
            var tabelaCabecalho = new PdfPTable(1)
            {
                SpacingBefore = 40
            };
            tabelaCabecalho.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaCabecalho.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            tabelaCabecalho.AddCell(new Phrase("CONTRATO PARTICULAR DE COMPRA E VENDA DE AUTOMÓVEL", _fonteNegrito));
            InsereTabelaNoDocumento(tabelaCabecalho);
        }

        public void SeccaoPartes()
        {
            var tabelaPartes = new PdfPTable(1)
            {
                SpacingBefore = 30
            };

            tabelaPartes.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaPartes.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

            tabelaPartes.AddCell(new Phrase("DAS PARTES\n\n\n", _fonteNegrito));

            tabelaPartes.DefaultCell.SetLeading(0, 1.5f);


            var primeiroChkVendedor = new Chunk("VENDEDOR: ", _fonteNegrito);
            var segundoChkVendedor  = new Chunk("MUNIR VEÍCULOS, inscrita no CNPJ: 15.123.875/0001-58, situada a Av. Min.João Alberto, S/N, " +
                                                        "Centro, Barra do Garças- MT, neste ato representada por Munir Fahed Ibrahim Filho, portador " +
                                                            "do CPF sob o nº 049.281.871-13.\n", _fonte);
            
            var fraseVendedor = new Phrase();
            fraseVendedor.Add(primeiroChkVendedor);
            fraseVendedor.Add(segundoChkVendedor);


            tabelaPartes.AddCell(fraseVendedor);

            var primeiroChkComprador = new Chunk("COMPRADOR: ", _fonteNegrito);
            var segundoChkComprador  = new Chunk($"{_cliente.Nome}, {_cliente.EstadoCivil}, {_cliente.Profissao}, Portador da Cédula de Identidade RG nº {_cliente.RG} {_cliente.OrgaoExpedidor}," +
                                                    $" inscrito no CPF sob o nº {_cliente.CPF}, Telefone: {_cliente.Telefone}, residente e domiciliado à {_cliente.EnderecoCliente.Logradouro}, " +
                                                        $"{_cliente.EnderecoCliente.Cidade} - {_cliente.EnderecoCliente.UF}", _fonte);

            var fraseComprador = new Phrase();
            fraseComprador.Add(primeiroChkComprador);
            fraseComprador.Add(segundoChkComprador);

            tabelaPartes.AddCell(fraseComprador);

            tabelaPartes.AddCell(new Phrase("\n\nPor este instrumento particular, as partes acima identificadas têm, entre si, " +
                                                "justo e acertado o presente Contrato de Compra e Venda de Automóvel, que se regerá pelas " +
                                                    "cláusulas seguintes e pelas condições descritas no presente.", _fonte));

            InsereTabelaNoDocumento(tabelaPartes);


        }
    }
}