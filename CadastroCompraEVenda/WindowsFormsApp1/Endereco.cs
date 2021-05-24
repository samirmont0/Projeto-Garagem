namespace ContratoCompraEVenda
{
    public class Endereco
    {
        public string Cidade { get; set; }

        public string UF { get; set; }

        public string Logradouro { get; set; }

        public Endereco(string cidade, string uf, string logradouro)
        {
            Cidade = cidade;
            UF = uf;
            Logradouro = logradouro;
        }
    }
}
