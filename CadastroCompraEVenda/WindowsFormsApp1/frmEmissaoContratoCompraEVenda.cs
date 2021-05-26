using System;
using System.Windows.Forms;

namespace ContratoCompraEVenda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool EhEnderecoValido()
        {
            if (txtLogradouro.Text != "" && cmbUF.Text != "" && txtCidade.Text != "")
            {
                return true;
            }
            return false;
        }

        public bool TemCarroPreenchido()
        {
            if (txtCarro.Text != "" && txtKM.Text != "" && txtPlaca.Text != "" && txtMarca.Text != "" && txtRenavam.Text != "" && txtCor.Text != "" && txtAno.Text != "" && txtChassi.Text != "" && txtFormaPagamento.Text != "" && txtValor.Text != "")
            {
                return true;
            }
            MessageBox.Show("Verifique as informações referentes ao carro!");
            return false;
        }

        public bool ValidaClienteEEndereco()
        {
            Cliente _cliente;
            Endereco _enderecoCliente;

            if (!EhEnderecoValido())
            {
                MessageBox.Show("Verifique as informações referentes ao endereço do cliente!");
                return false;
            }

            if (txtNomeCliente.Text != "" && mskTxtCPF.Text != "" && txtProfissao.Text != "" && cmbEstadoCivil.Text != "" && txtRG.Text != "" && txtNacionalidade.Text != "" && txtExpedidor.Text != "" && mskTxtTelefone.Text != "")
            {
                _enderecoCliente = new Endereco(txtCidade.Text, cmbUF.Text, txtLogradouro.Text);

                try
                {
                    _cliente = new Cliente(txtNomeCliente.Text, mskTxtCPF.Text, txtProfissao.Text, cmbEstadoCivil.Text, txtRG.Text, txtExpedidor.Text, mskTxtTelefone.Text, txtNacionalidade.Text, _enderecoCliente);
                }
                catch (ArgumentOutOfRangeException Ex)
                {
                    MessageBox.Show(Ex.Message);
                    return false;
                }
                catch (ApplicationException Ex)
                {
                    MessageBox.Show(Ex.Message);
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("Verifique as informações referentes aos dados do cliente!");
                return false;
            }
        }

        private void btnGerarPDF_Click(object sender, EventArgs e)
        {
            if (ValidaClienteEEndereco() && TemCarroPreenchido())
            {
                try
                {
                    Endereco enderecoCliente = new Endereco(txtCidade.Text, cmbUF.Text, txtLogradouro.Text);
                    Cliente cliente = new Cliente(txtNomeCliente.Text, mskTxtCPF.Text, txtProfissao.Text, cmbEstadoCivil.Text, txtRG.Text, txtExpedidor.Text, mskTxtTelefone.Text, txtNacionalidade.Text, enderecoCliente);
                    Carro carro = new Carro(txtAno.Text, txtCarro.Text, txtPlaca.Text, txtCor.Text, txtKM.Text, txtMarca.Text, txtRenavam.Text, txtChassi.Text, txtFormaPagamento.Text, txtValor.Text);

                    var montadorPDF = new MontadorPDF(cliente, carro);

                    montadorPDF.AlteraMargemDocumento(3, 3, 5, 5);
                    montadorPDF.AbreDocumento();

                    montadorPDF.InsereLogo();
                    montadorPDF.SeccaoCabecalho();
                    montadorPDF.SeccaoPartes();
                    montadorPDF.SeccaoDoObjetoDoContrato();
                    montadorPDF.SeccaoObrigacoes();
                    montadorPDF.SeccaoPrecoPagamento();
                    montadorPDF.SeccaoRescisao();
                    montadorPDF.SeccaoTransferencia();
                    montadorPDF.SeccaoDisposicoesGerais();
                    montadorPDF.SeccaoFinalAssinaturas();

                    montadorPDF.FechaDocumento();
                    montadorPDF.AbrePDF();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show($"Ocorreu um erro ao gerar o PDF - Erro: {Ex.Message}");
                }
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08 && e.KeyChar != 44)
            {
                e.Handled = true;
            }
        }

        private void txtRG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08 && e.KeyChar != 45)
            {
                e.Handled = true;
            }
        }

        private void btnModeloPagamento_Click(object sender, EventArgs e)
        {
            txtFormaPagamento.Text = "-Pagamento à vista: R$ (valor numérico e por extenso), pagamento a ser efetuado no dia __, em moeda " +
                                            "corrente nacional, através de depósito bancário na conta (conta completa, nome do banco, ag. e cc.), servindo os comprovantes de" +
                                                " depósito, como recibo de quitação de tais quantias.\n-Pagamento parcelado: R$ (valor numérico e por extenso), pagos em parcelas, " +
                                                    "até o dia 10 de cada mês, com a primeira parcela em(data), por meio de depósito bancário na conta ___. ";
        }

        private void txtKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void txtCor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != 08 && e.KeyChar != 32)
            {
                e.Handled = true;
            }
        }

        private void txtAno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08 && e.KeyChar != 47)
            {
                e.Handled = true;
            }
        }

        private void txtNacionalidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != 08 && e.KeyChar != 32)
            {
                e.Handled = true;
            }
        }

        private void txtNomeCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != 08 && e.KeyChar != 32)
            {
                e.Handled = true;
            }
        }
    }
}