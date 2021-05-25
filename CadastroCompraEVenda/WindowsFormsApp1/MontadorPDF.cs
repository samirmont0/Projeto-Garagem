using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;

namespace ContratoCompraEVenda
{
    public class MontadorPDF
    {
        public Document doc;
        private Cliente _cliente;
        private Carro _carro;
        string caminho;
        private Font _fonteNegrito = FontFactory.GetFont("Arial", 13, Font.BOLD);
        private Font _fonte = FontFactory.GetFont("Arial", 13, Font.NORMAL);

        private PdfWriter writer;

        public MontadorPDF(Cliente cliente, Carro carro)
        {
            _cliente = cliente;
            _carro = carro;
            doc = new Document(PageSize.A4);
            doc.AddCreationDate();
            string[] nomeCliente = _cliente.Nome.Split( );
            caminho = @"C:\RelatorioCarro\ContratosClientes\" + _cliente.Nome + ".pdf";
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

            tabelaLogo.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaLogo.AddCell(new Phrase("\n\n"));
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

            tabelaCabecalho.AddCell(new Phrase("CONTRATO PARTICULAR DE COMPRA E VENDA DE AUTOMÓVEL\n\n\n\n", _fonteNegrito));
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
                                                        $"{_cliente.EnderecoCliente.Cidade} - {_cliente.EnderecoCliente.UF}", _fonte); //Precisa adicionar nacionalidade?

            var fraseComprador = new Phrase();
            fraseComprador.Add(primeiroChkComprador);
            fraseComprador.Add(segundoChkComprador);

            tabelaPartes.AddCell(fraseComprador);

            tabelaPartes.AddCell(new Phrase("\n\nPor este instrumento particular, as partes acima identificadas têm, entre si, " +
                                                "justo e acertado o presente Contrato de Compra e Venda de Automóvel, que se regerá pelas " +
                                                    "cláusulas seguintes e pelas condições descritas no presente.", _fonte));

            InsereTabelaNoDocumento(tabelaPartes);
        }

        public void SeccaoDoObjetoDoContrato()
        {
            var tabelaObjetoContrato = new PdfPTable(1)
            {
                SpacingBefore = 30
            };

            tabelaObjetoContrato.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaObjetoContrato.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

            tabelaObjetoContrato.AddCell(new Phrase("DO OBJETO DO CONTRATO\n\n", _fonteNegrito));

            tabelaObjetoContrato.DefaultCell.SetLeading(0, 1.5f);

            var primChunk = new Chunk("Cláusula 1ª ", _fonteNegrito);
            var segChunk = new Chunk($"O presente contrato tem como objeto o veículo {_carro.Nome}, {_carro.Marca}, {_carro.Ano}, " +
                $"{_carro.Cor}, {_carro.Placa} e RENAVAM: {_carro.Renavam}.\n", _fonte); //Precisa adicionar chassi?

            var frasePrimClausula = new Phrase();
            frasePrimClausula.Add(primChunk);
            frasePrimClausula.Add(segChunk);

            tabelaObjetoContrato.AddCell(frasePrimClausula);

            var primChunk1 = new Chunk("             1.2 ", _fonteNegrito);
            var segChunk1 = new Chunk($"O veículo, objeto deste contrato, encontra-se com a quilometragem {_carro.KM} KM", _fonte);

            var frasePrimClausula1 = new Phrase();
            frasePrimClausula1.Add(primChunk1);
            frasePrimClausula1.Add(segChunk1);

            tabelaObjetoContrato.AddCell(frasePrimClausula1);

            InsereTabelaNoDocumento(tabelaObjetoContrato);
            doc.NewPage();

            var primChunk2 = new Chunk("Cláusula 2ª ", _fonteNegrito);
            var segChunk2 = new Chunk($"Compõe ainda o objeto do presente contrato, os acessórios do veículo, quais sejam ...\n", _fonte); //Precisa adicionar chassi?

            var frasePrimClausula2 = new Phrase();
            frasePrimClausula2.Add(primChunk2);
            frasePrimClausula2.Add(segChunk2);

            tabelaObjetoContrato.AddCell(frasePrimClausula2);
        }

        public void SeccaoObrigacoes() 
        { 

        }
    }
}