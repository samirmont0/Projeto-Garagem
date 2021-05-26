namespace ContratoCompraEVenda
{
    public class Carro
    {
        public string Ano { get; set; }

        public string Nome { get; set; }

        public string Placa { get; set; }

        public string Cor { get; set; }

        public string KM { get; set; }

        public string Marca { get; set; }

        public string Renavam { get; set; }

        public string Chassi { get; set; }

        public string FormaPagamento { get; set; }

        public string Valor { get; set; }

        public Carro(string ano, string nome, string placa, string cor, string km, string marca, string renavam, string chassi, string formaPagamento, string valor)
        {
            Ano = ano;
            Nome = nome;
            Placa = placa;
            Cor = cor;
            KM = km;
            Marca = marca;
            Renavam = renavam;
            Chassi = chassi;
            FormaPagamento = formaPagamento;
            Valor = valor;
        }
    }

}
