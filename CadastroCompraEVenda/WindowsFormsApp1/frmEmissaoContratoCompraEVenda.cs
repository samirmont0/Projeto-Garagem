using System;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

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
			if(txtLogradouro.Text != "" && cmbUF.Text != "" && txtCidade.Text != "")
            {
				return true;
            }
			return false;
        }

		public bool TemCarroPreenchido()
        {
			if(txtCarro.Text != "" && txtKM.Text != "" && txtPlaca.Text != "" && txtMarca.Text != "" && txtRenavam.Text != "" && txtCor.Text != "" && txtAno.Text != "")
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

			if (txtNomeCliente.Text != "" && mskTxtCPF.Text != "" && txtProfissao.Text != "" && cmbEstadoCivil.Text != "" && txtRG.Text != "")
            {
				_enderecoCliente = new Endereco(txtCidade.Text, cmbUF.Text, txtLogradouro.Text);

                try
                {
					_cliente = new Cliente(txtNomeCliente.Text, mskTxtCPF.Text, txtProfissao.Text, cmbEstadoCivil.Text, txtRG.Text, txtExpedidor.Text, mskTxtTelefone.Text, _enderecoCliente);
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
					Cliente cliente = new Cliente(txtNomeCliente.Text, mskTxtCPF.Text, txtProfissao.Text, cmbEstadoCivil.Text, txtRG.Text, txtExpedidor.Text, mskTxtTelefone.Text, enderecoCliente);
					Carro carro = new Carro(txtAno.Text, txtCarro.Text, txtPlaca.Text, txtCor.Text, txtKM.Text, txtMarca.Text, txtRenavam.Text);

					var montadorPDF = new MontadorPDF(cliente, carro);
					
					montadorPDF.AlteraMargemDocumento(3, 3, 5, 5);
					montadorPDF.AbreDocumento();

					montadorPDF.InsereLogo();
					montadorPDF.SeccaoCabecalho();
					montadorPDF.SeccaoPartes();
					montadorPDF.SeccaoDoObjetoDoContrato();

					montadorPDF.FechaDocumento();
					montadorPDF.AbrePDF();
				}
				catch (Exception Ex)
				{
					MessageBox.Show($"Ocorreu um erro ao gerar o PDF - Erro: {Ex.Message}");
				}
			}
		}
    }
}