using br.corp.bonus630.FullText;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace ContratoCompraEVenda
{
    public class MontadorPDF
    {
        public Document doc;
        private Cliente _cliente;
        private Carro _carro;
        string caminho;
        private Font _fonteNegrito = FontFactory.GetFont("Arial", 13.5f, Font.BOLD);
        private Font _fonte = FontFactory.GetFont("Arial", 13.5f, Font.NORMAL);

        private PdfWriter writer;

        public MontadorPDF(Cliente cliente, Carro carro)
        {
            _cliente = cliente;
            _carro = carro;
            doc = new Document(PageSize.A4);
            doc.AddCreationDate();
            string[] nomeCliente = _cliente.Nome.Split();
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

            tabelaPartes.AddCell(new Phrase("DAS PARTES\n", _fonteNegrito));

            tabelaPartes.DefaultCell.SetLeading(0, 1.5f);


            var primeiroChkVendedor = new Chunk("VENDEDOR: ", _fonteNegrito);
            var segundoChkVendedor = new Chunk("MUNIR VEÍCULOS, inscrita no CNPJ: 15.123.875/0001-58, situada a Av. Min.João Alberto, S/N, " +
                                                        "Centro, Barra do Garças- MT, neste ato representada por Munir Fahed Ibrahim Filho, portador " +
                                                            "do CPF sob o nº 049.281.871-13.\n", _fonte);

            var fraseVendedor = new Phrase();
            fraseVendedor.Add(primeiroChkVendedor);
            fraseVendedor.Add(segundoChkVendedor);

            tabelaPartes.AddCell(fraseVendedor);

            var primeiroChkComprador = new Chunk("COMPRADOR: ", _fonteNegrito);
            var segundoChkComprador = new Chunk($"{_cliente.Nome}, nacionalidade {_cliente.Nacionalidade}, {_cliente.EstadoCivil}, {_cliente.Profissao}, Portador da Cédula de Identidade RG nº {_cliente.RG} {_cliente.OrgaoExpedidor}," +
                                                    $" inscrito no CPF sob o nº {_cliente.CPF}, Telefone: {_cliente.Telefone}, residente e domiciliado à {_cliente.EnderecoCliente.Logradouro}, " +
                                                        $"{_cliente.EnderecoCliente.Cidade}-{_cliente.EnderecoCliente.UF}", _fonte);

            var fraseComprador = new Phrase();
            fraseComprador.Add(primeiroChkComprador);
            fraseComprador.Add(segundoChkComprador);

            tabelaPartes.AddCell(fraseComprador);

            tabelaPartes.AddCell(new Phrase("\nPor este instrumento particular, as partes acima identificadas têm, entre si, " +
                                                "justo e acertado o presente Contrato de Compra e Venda de Automóvel, que se regerá pelas " +
                                                    "cláusulas seguintes e pelas condições descritas no presente.", _fonte));

            InsereTabelaNoDocumento(tabelaPartes);
        }

        public void SeccaoDoObjetoDoContrato()
        {
            var tabelaObjetoContrato = new PdfPTable(1)
            {
                SpacingBefore = 45
            };

            tabelaObjetoContrato.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaObjetoContrato.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

            tabelaObjetoContrato.AddCell(new Phrase("DO OBJETO DO CONTRATO\n", _fonteNegrito));

            tabelaObjetoContrato.DefaultCell.SetLeading(0, 1.5f);

            var primChunk = new Chunk("Cláusula 1ª ", _fonteNegrito);
            var segChunk = new Chunk($"O presente contrato tem como objeto o veículo {_carro.Nome}, {_carro.Marca}, {_carro.Ano}, " +
                $"{_carro.Cor}, {_carro.Placa}, RENAVAM: {_carro.Renavam} e Chassi: {_carro.Chassi}.\n", _fonte);

            var frasePrimClausula = new Phrase();
            frasePrimClausula.Add(primChunk);
            frasePrimClausula.Add(segChunk);

            tabelaObjetoContrato.AddCell(frasePrimClausula);

            var primChunk1 = new Chunk("             1.2 ", _fonteNegrito);
            var segChunk1 = new Chunk($"O veículo, objeto deste contrato, encontra-se com a quilometragem {_carro.KM} KM\n", _fonte);

            var frasePrimClausula1 = new Phrase();
            frasePrimClausula1.Add(primChunk1);
            frasePrimClausula1.Add(segChunk1);

            tabelaObjetoContrato.AddCell(frasePrimClausula1);

            var primChunk2 = new Chunk("Cláusula 2ª ", _fonteNegrito);
            var segChunk2 = new Chunk($"Compõe ainda o objeto do presente contrato, os acessórios do veículo, quais sejam ...\n", _fonte);

            var fraseSegClausula = new Phrase();
            fraseSegClausula.Add(primChunk2);
            fraseSegClausula.Add(segChunk2);

            tabelaObjetoContrato.AddCell(fraseSegClausula);

            InsereTabelaNoDocumento(tabelaObjetoContrato);
            doc.NewPage();
        }

        public void SeccaoObrigacoes()
        {
            var tabelaDasObrigacoes = new PdfPTable(1)
            {
                SpacingBefore = 30
            };

            tabelaDasObrigacoes.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaDasObrigacoes.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

            tabelaDasObrigacoes.AddCell(new Phrase("\n\nDAS OBRIGAÇÕES\n", _fonteNegrito));

            tabelaDasObrigacoes.DefaultCell.SetLeading(0, 1.5f);

            var primChunk = new Chunk("Cláusula 3ª ", _fonteNegrito);
            var segChunk = new Chunk($"O VENDEDOR responde pela garantia (somente de veículos não consignados, ou seja, de propriedade desta empresa) dos " +
                $"seguintes componentes: MOTOR, CÂMBIO e DIFERENCIAL, em conformidade com o prazo estabelecido de 90 (noventa) dias ou caso o veículo tenha " +
                $"percorrido até 3.000 km (três mil quilômetros), valendo o que ocorrer primeiro.", _fonte);

            var fraseTercClausula = new Phrase();
            fraseTercClausula.Add(primChunk);
            fraseTercClausula.Add(segChunk);

            tabelaDasObrigacoes.AddCell(fraseTercClausula);

            var primChunk1 = new Chunk("             3.2 ", _fonteNegrito);
            var segChunk1 = new Chunk($"O COMPRADOR se compromete a fazer a revisão do veículo para uso, principalmente no tocante à troca de óleo e a correia dentada.\n\n", _fonte);

            var fraseTercClausula1 = new Phrase();
            fraseTercClausula1.Add(primChunk1);
            fraseTercClausula1.Add(segChunk1);

            tabelaDasObrigacoes.AddCell(fraseTercClausula1);

            var primChunkClaus4 = new Chunk("Cláusula 4ª ", _fonteNegrito);
            var segChunkClaus4 = new Chunk($"O VENDEDOR se responsabilizará pela entrega do automóvel ao COMPRADOR, livre de qualquer ônus ou encargos.\n\n", _fonte);

            var fraseQuarClausula = new Phrase();
            fraseQuarClausula.Add(primChunkClaus4);
            fraseQuarClausula.Add(segChunkClaus4);

            tabelaDasObrigacoes.AddCell(fraseQuarClausula);

            var primChunkClaus5 = new Chunk("Cláusula 5ª ", _fonteNegrito);
            var segChunkClaus5 = new Chunk($"O COMPRADOR se responsabilizará, após a assinatura deste instrumento, pelos impostos e taxas que incidirem sobre o automóvel.\n\n", _fonte);

            var fraseQuinClausula = new Phrase();
            fraseQuinClausula.Add(primChunkClaus5);
            fraseQuinClausula.Add(segChunkClaus5);

            tabelaDasObrigacoes.AddCell(fraseQuinClausula);

            var primChunkClaus6 = new Chunk("Cláusula 6ª ", _fonteNegrito);
            var segChunkClaus6 = new Chunk($"O VENDEDOR não se responsabilizará pelos danos causados no bem por negligência do COMPRADOR e nem por problemas decorrentes da não realização das revisões acertadas.\n\n", _fonte);

            var fraseSexClausula = new Phrase();
            fraseSexClausula.Add(primChunkClaus6);
            fraseSexClausula.Add(segChunkClaus6);

            tabelaDasObrigacoes.AddCell(fraseSexClausula);

            InsereTabelaNoDocumento(tabelaDasObrigacoes);
        }

        public void SeccaoPrecoPagamento()
        {
            var tft = new ToFullText();
            var tabelaPreco = new PdfPTable(1);
            int valorCarro = Int32.Parse(_carro.Valor);
            string valorFormatado = valorCarro.ToString("C");

            tabelaPreco.DefaultCell.SetLeading(0, 1.5f);
            tabelaPreco.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaPreco.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

            var primChunkClaus7 = new Chunk("Cláusula 7ª ", _fonteNegrito);
            var segChunkClaus7 = new Chunk($"O valor total da presente transação é de {valorFormatado}({tft.showText(_carro.Valor)}), onde o COMPRADOR se compromete a pagar ao VENDEDOR, da seguinte maneira:\n", _fonte);

            var fraseSetClausula = new Phrase();
            fraseSetClausula.Add(primChunkClaus7);
            fraseSetClausula.Add(segChunkClaus7);

            tabelaPreco.AddCell(fraseSetClausula);

            string[] pagamentos = _carro.FormaPagamento.Split('\n');

            for (int i = 0; i < pagamentos.Length; i++)
            {
                tabelaPreco.AddCell(new Phrase($"{pagamentos[i]}", _fonte));
            }

            InsereTabelaNoDocumento(tabelaPreco);
        }

        public void SeccaoRescisao()
        {

            var tabelaRescisao = new PdfPTable(1)
            {
                SpacingBefore = 30
            };

            tabelaRescisao.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaRescisao.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

            tabelaRescisao.AddCell(new Phrase("DA RESCISÃO CONTRATUAL E MULTA POR DESCUMPRIMENTO DO CONTRATO\n", _fonteNegrito));

            tabelaRescisao.DefaultCell.SetLeading(0, 1.5f);

            var primChunkClaus8 = new Chunk("Cláusula 8ª ", _fonteNegrito);
            var segChunkClaus8 = new Chunk($"No caso de rescisão contratual, fica estabelecida a multa de 20% (vinte por cento), sobre o valor da venda do automóvel, cominada à parte que desistir, descumprir ou dificultar o fiel cumprimento de qualquer uma das cláusulas e condições do presente contrato.\n\n", _fonte);

            var fraseOitClausula = new Phrase();
            fraseOitClausula.Add(primChunkClaus8);
            fraseOitClausula.Add(segChunkClaus8);

            tabelaRescisao.AddCell(fraseOitClausula);

            var primChunkClaus9 = new Chunk("\n\nCláusula 9ª ", _fonteNegrito);
            var segChunkClaus9 = new Chunk($"Havendo atraso no pagamento das parcelas acordadas, incidirá multa de 2% (dois por cento) sobre o valor da parcela em atraso, e juros de 0,03333% ao dia.\n\n", _fonte);

            var fraseNonaClausula = new Phrase();
            fraseNonaClausula.Add(primChunkClaus9);
            fraseNonaClausula.Add(segChunkClaus9);

            tabelaRescisao.AddCell(fraseNonaClausula);

            var primChunkClaus10 = new Chunk("Cláusula 10ª ", _fonteNegrito);
            var segChunkClaus10 = new Chunk($"Caso alguma das partes não cumpra o disposto nas cláusulas acima, responsabilizar-se-á pelo pagamento de multa equivalente a 20% (vinte por cento) do valor da venda do automóvel.\n\n", _fonte);

            var fraseDecimaClausula = new Phrase();
            fraseDecimaClausula.Add(primChunkClaus10);
            fraseDecimaClausula.Add(segChunkClaus10);

            tabelaRescisao.AddCell(fraseDecimaClausula);

            InsereTabelaNoDocumento(tabelaRescisao);
        }

        public void SeccaoTransferencia()
        {
            var tabelaTransferencia = new PdfPTable(1)
            {
                SpacingBefore = 30
            };

            tabelaTransferencia.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaTransferencia.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

            tabelaTransferencia.AddCell(new Phrase("DA TRANSFERÊNCIA E POSSE DO VEÍCULO\n", _fonteNegrito));

            tabelaTransferencia.DefaultCell.SetLeading(0, 1.5f);

            var primChunkClaus11 = new Chunk("Cláusula 11ª ", _fonteNegrito);
            var segChunkClaus11 = new Chunk($"Diante do comprovante de quitação de todos os débitos acordados referente à compra e venda do veículo, " +
                                                $"será transferida a propriedade e posse definitiva e todos os direitos e deveres relacionados ao bem indicado no presente contrato, " +
                                                     $"mediante a entrega das chaves, todos os documentos e acessórios, a menos que as partes definam outro momento.\n\n", _fonte);

            var fraseDecPrimClausula = new Phrase();
            fraseDecPrimClausula.Add(primChunkClaus11);
            fraseDecPrimClausula.Add(segChunkClaus11);

            tabelaTransferencia.AddCell(fraseDecPrimClausula);

            var primChunkClaus12 = new Chunk("Cláusula 12ª ", _fonteNegrito);
            var segChunkClaus12 = new Chunk($"A partir da assinatura deste instrumento, as benfeitorias realizadas por quaisquer das partes serão incorporadas ao veículo e não gerarão," +
                                                $" assim, direito a ressarcimento ou a indenização, exceto se as partes expressamente, por meio escrito ou verbal, acordarem o contrário.\n\n", _fonte);

            var fraseDecSegClausula = new Phrase();
            fraseDecSegClausula.Add(primChunkClaus12);
            fraseDecSegClausula.Add(segChunkClaus12);

            tabelaTransferencia.AddCell(fraseDecSegClausula);

            var primChunkClaus13 = new Chunk("Cláusula 13ª ", _fonteNegrito);
            var segChunkClaus13 = new Chunk($"O COMPRADOR compromete-se, no prazo de 30 (trinta) dias contados da assinatura do contrato, a providenciar, junto ao DETRAN, o registro da " +
                                                $"respectiva transferência de propriedade, sob pena, de não o fazendo, vir a responder pelos encargos, multas e demais cominações decorrentes" +
                                                    $" de sua omissão.\n\n", _fonte);

            var fraseDecTercClausula = new Phrase();
            fraseDecTercClausula.Add(primChunkClaus13);
            fraseDecTercClausula.Add(segChunkClaus13);

            tabelaTransferencia.AddCell(fraseDecTercClausula);

            InsereTabelaNoDocumento(tabelaTransferencia);
        }

        public void SeccaoDisposicoesGerais()
        {
            var tabelaDisposicoesGerais = new PdfPTable(1)
            {
                SpacingBefore = 30
            };

            tabelaDisposicoesGerais.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaDisposicoesGerais.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

            tabelaDisposicoesGerais.AddCell(new Phrase("DISPOSIÇÕES GERAIS\n", _fonteNegrito));

            tabelaDisposicoesGerais.DefaultCell.SetLeading(0, 1.5f);

            var primChunkClaus14 = new Chunk("Cláusula 14ª ", _fonteNegrito);
            var segChunkClaus14 = new Chunk($"O presente contrato passa a valer a partir da assinatura pelas partes, obrigando-se a ele os herdeiros ou sucessores das mesmas.\n\n", _fonte);

            var fraseDecQuartaClausula = new Phrase();
            fraseDecQuartaClausula.Add(primChunkClaus14);
            fraseDecQuartaClausula.Add(segChunkClaus14);

            tabelaDisposicoesGerais.AddCell(fraseDecQuartaClausula);

            var primChunkClaus15 = new Chunk("Cláusula 15ª ", _fonteNegrito);
            var segChunkClaus15 = new Chunk($"As partes elegem o Foro da Comarca de BARRA DO GARÇAS-MT, para dirimir eventuais divergências deste instrumento.\n\n", _fonte);

            var fraseDecQuuintaClausula = new Phrase();
            fraseDecQuuintaClausula.Add(primChunkClaus15);
            fraseDecQuuintaClausula.Add(segChunkClaus15);

            tabelaDisposicoesGerais.AddCell(fraseDecQuuintaClausula);

            InsereTabelaNoDocumento(tabelaDisposicoesGerais);
            doc.NewPage();
        }

        public void SeccaoFinalAssinaturas()
        {
            var tabelaPreAssinatura = new PdfPTable(1)
            {
                SpacingBefore = 45
            };

            tabelaPreAssinatura.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaPreAssinatura.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            tabelaPreAssinatura.DefaultCell.SetLeading(0, 1.5f);

            tabelaPreAssinatura.AddCell(new Phrase("\n\n         Por estarem assim justos e contratados, firmam o presente instrumento, em duas vias de igual teor e forma, " +
                                                                    "juntamente com 2 (duas) testemunhas presenciais.\n", _fonte));

            tabelaPreAssinatura.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabelaPreAssinatura.AddCell(new Phrase($"Barra do Garças- MT, {DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy")}.", _fonte));

            InsereTabelaNoDocumento(tabelaPreAssinatura);


            var tabelaAssinatura = new PdfPTable(2)
            {
                SpacingBefore = 45
            };

            tabelaAssinatura.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaAssinatura.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            tabelaAssinatura.DefaultCell.SetLeading(0, 1.5f);
            tabelaAssinatura.DefaultCell.Colspan = 2;

            tabelaAssinatura.AddCell(new Phrase("PARTES\n\n\n", _fonteNegrito));

            tabelaAssinatura.DefaultCell.Colspan = 1;

            tabelaAssinatura.AddCell(new Phrase("_____________________", _fonte));
            tabelaAssinatura.AddCell(new Phrase("\t         _____________________", _fonte));
            tabelaAssinatura.AddCell(new Phrase("           VENDEDOR", _fonte));
            tabelaAssinatura.AddCell(new Phrase("\t                   COMPRADOR", _fonte));

            tabelaAssinatura.DefaultCell.Colspan = 2;
            tabelaAssinatura.DefaultCell.SetLeading(0, 1f);
            tabelaAssinatura.AddCell(new Phrase("      MUNIR VEICULOS", _fonteNegrito));
            tabelaAssinatura.AddCell(new Phrase("     15.123.875/0001-58", _fonteNegrito));

            InsereTabelaNoDocumento(tabelaAssinatura);


            var tabelaAssinaturaTestemunha = new PdfPTable(1)
            {
                SpacingBefore = 45
            };
            tabelaAssinaturaTestemunha.DefaultCell.Border = Rectangle.NO_BORDER;
            tabelaAssinaturaTestemunha.DefaultCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            tabelaAssinaturaTestemunha.DefaultCell.SetLeading(0, 1.5f);

            tabelaAssinaturaTestemunha.AddCell(new Phrase("TESTEMUNHAS:\n\n", _fonteNegrito));

            tabelaAssinaturaTestemunha.AddCell(new Phrase("______________________________________, CPF n° _______________", _fonte));
            tabelaAssinaturaTestemunha.AddCell(new Phrase("(nome completo e assinatura)\n\n\n", _fonte));

            tabelaAssinaturaTestemunha.AddCell(new Phrase("______________________________________, CPF n° _______________", _fonte));
            tabelaAssinaturaTestemunha.AddCell(new Phrase("(nome completo e assinatura)", _fonte));

            InsereTabelaNoDocumento(tabelaAssinaturaTestemunha);
        }
    }
}