using System;

namespace ContratoCompraEVenda
{
    public class Cliente
    {
        public Endereco EnderecoCliente;

        private string _nome;

        private string _cpf;

        public string OrgaoExpedidor { get; set; }

        public string Nacionalidade { get; set; }

        public string Nome
        {
            get => _nome;
            set
            {
                if (value == "" || value == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(Nome));
                }
                for (int i = 0; i < value.Length; i++)
                {
                    if (!char.IsWhiteSpace(value[i]) && !char.IsLetter(value[i]))
                    {
                        throw new ApplicationException("Verifique o nome! (Digite apenas letras)");
                    }
                }
                _nome = value;
            }
        }

        public string Profissao { get; set; }

        public string Telefone { get; set; }

        public string EstadoCivil { get; set; }

        public string RG { get; set; }


        public string CPF
        {
            get => _cpf;
            set
            {
                if (value == null || value.Length != 11)
                {
                    throw new ArgumentOutOfRangeException(nameof(CPF));
                }

                for (int i = 0; i < 11; i++)
                {
                    if (!char.IsNumber(value[i]))
                    {
                        throw new ApplicationException("Verifique o CPF! (Digite apenas números)");
                    }
                }

                int totalPrimeiroDigito = 0;
                for (int i = 0, n = 10; i < 9; i++)
                {
                    totalPrimeiroDigito += (Convert.ToInt32(value[i]) - 48) * n;
                    n--;
                }

                int resto1 = (totalPrimeiroDigito * 10) % 11 == 10 ? 0 : (totalPrimeiroDigito * 10) % 11;
                if (resto1 != Convert.ToInt32(value[9] - 48))
                {
                    throw new ArgumentOutOfRangeException(nameof(CPF));
                }

                int totalSegundoDigito = 0;
                for (int i = 0, n = 11; i < 10; i++)
                {
                    totalSegundoDigito += (Convert.ToInt32(value[i]) - 48) * n;
                    n--;
                }

                int resto2 = (totalSegundoDigito * 10) % 11 == 10 ? 0 : (totalSegundoDigito * 10) % 11;
                if (resto2 != Convert.ToInt32(value[10] - 48))
                {
                    throw new ArgumentOutOfRangeException(nameof(CPF));
                }
                _cpf = string.Format(@"{0:000\.000\.000-00}", Convert.ToInt64(value));
            }
        }

        public Cliente(string nome, string cpf, string profissao, string estadoCivil, string rg, string orgaoExpedidor, string telefone, string nacionalidade, Endereco enderecoCliente)
        {
            Nome = nome;
            CPF = cpf;
            Profissao = profissao;
            EstadoCivil = estadoCivil;
            RG = rg;
            Telefone = telefone;
            Nacionalidade = nacionalidade;
            EnderecoCliente = enderecoCliente;
            OrgaoExpedidor = orgaoExpedidor;
        }


    }
}