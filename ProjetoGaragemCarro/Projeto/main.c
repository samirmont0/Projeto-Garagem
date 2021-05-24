#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include <locale.h>
#include <ctype.h>

//Cria estrutura de dados do tipo struct para os carros
typedef struct Estoque{
        char nome[30];
        char ano[10];
        char valor[10];
}carros;

//Cria estrutura de dados do tipo struct para os gastos
typedef struct gastosMensais{
        char nome[30];
        char ano[10];
        char valor[10];
}gastos;

/*============  Prot�tipos  ===============*/
void abrirArquivo(char*, char*);
void fechaArquivo(FILE*);
void limpaTela();
void limpaBufferTeclado();
void listaCarrosCadastrados();
void imprimeContratoCompraEVenda();
void imprimeRelatorioCarros();
void imprimeRelatorioGastos();
void visualizaEAdicionaGastosMensais();
void adicionaDespesas();
void removeDespesas();
void removeDespesaEspecifica();
void zeraGastosMensais();
void cadastraCarro();

/*============  Vari�veis Globais  ============*/
carros carrosCadastrados[50];
gastos gastosMen[30];
FILE *arquivo;
static int quantidade = 0;

int main()
{
    system("color f4"); //Muda a cor do background do console para branco e as letras para vermelho(Tela garagem Munir Ve�culos)
    setlocale(LC_ALL, "Portuguese"); //Muda para a l�ngua local(Portugu�s)
    menuPrincipal(); //Chama a fun��o do menu
    system("pause");
}

/*============  Fun��es  ============*/
void menuPrincipal()
{

    char opcao[2];
    int op;

    system("cls");
    printf("\t\t\t\t======================================\n");
    printf("\t\t\t\t#                                    #\n");
    printf("\t\t\t\t#           MENU PRINCIPAL           #\n");
    printf("\t\t\t\t#                                    #\n");
    printf("\t\t\t\t#           MUNIR VEICULOS           #\n");
    printf("\t\t\t\t#                                    #\n");
    printf("\t\t\t\t======================================\n\n");

    printf("\n Escolha uma das op��es\n\n");


    //OP��ES DE ESCOLHA PARA O CLIENTE, QUANDO O PROGRAMA FOR ABERTO
    printf("\n (1) - Adicionar carro");
    printf("\n (2) - Excluir carro");
    printf("\n (3) - Alterar carro");
    printf("\n (4) - Listar carros");
    printf("\n (5) - Gerenciar gastos mensais");
    printf("\n (6) - Simular financiamento");
    printf("\n (7) - Imprimir relatorio dos carros");
    printf("\n (8) - Imprimir contrato de compra e venda");
    printf("\n (0) - Sair");
    printf("\n\n\n Opcao:");
    limpaBufferTeclado();
    scanf("%d", &op);

    //SWITCH PARA CHAMADA DAS FUN��ES
        switch(op)
        {
            case 1 :
                cadastraCarroOrdenado(0);
                break;

            case 2:
                removeCarro(0);
                break;

            case 3:
                alteraCarro();
                break;

            case 4:
                listaCarrosCadastrados();
                break;

            case 5:
                visualizaEAdicionaGastosMensais();
                break;

            case 6:
                //();
                break;

            case 7:
                imprimeRelatorioCarros();
                break;

            case 8:
                imprimeContratoCompraEVenda();
                break;

            case 0 :
                exit(0);

            default:
                menuPrincipal();
        }
}

void alteraCarro()
{
    removeCarro(1); //Chama a fun��o de remover carro, passando 1 como argumento, para n�o ter a op��o de remover mais de um carro
}

//ABRE O ARQUIVO DE ACORDO COM O QUE FOR DESEJADO("W" PARA ESCRITA, "R" PARA LEITURA E "A" PARA CRIAR/ANEXAR)
void abrirArquivo(char *nomeArq, char *modo)
{
    arquivo = fopen(nomeArq, modo);

    if(arquivo == NULL)
    {
        printf("Erro na abertura do arquivo");
        getch();
        exit(0);
    }
}

//FECHA O ARQUIVO TXT
void fechaArquivo(FILE *p)
{
    fclose(p);
}

//LIMPA O QUE FOR QUE ESTEJA ESCRITO NO CONSOLE
void limpaTela()
{
    system("cls");
}

//RETIRA TUDO DA MEMORIA DO TECLADO
void limpaBufferTeclado()
{
    setbuf(stdin, NULL);
}

//LISTA TODOS OS CARROS QUE EST�O CADASTRADOS
void listaCarrosCadastrados()
{
    limpaTela();
    const size_t tamLinha = 50;
    char* linhaArq = malloc(tamLinha);
    int qtd = 0;

    abrirArquivo("RelacaoDosCarros.txt", "r");

    while (fgets(linhaArq, tamLinha, arquivo) != NULL)  {
        printf("%s", linhaArq);
        qtd++;
    }

    free(linhaArq);
    fechaArquivo(arquivo);

    printf("\nQuantidade de carros cadastrados: %d\n\n", qtd);
    printf("\n\nDigite alguma tecla para retornar ao menu principal...");
    getch();
    menuPrincipal();
}

void removeCarro(int ehChamadaAlteracao)
{
    FILE *arquivoTemp;
    carros carrosNoArquivo;
    const size_t tamLinha = 50;
    char* linhaArq = malloc(tamLinha);
    char nomeCarroRemovido[30], opcao;
    int qntd = 0, qntdCarroNoArquivo = 0;

    limpaBufferTeclado();
    limpaTela();

    if(ehChamadaAlteracao)
    {
        printf("Qual o nome do carro que deseja alterar?(O nome deve ser id�ntico ao nome cadastrado)\n");
        gets(nomeCarroRemovido);
    }
    else
    {
        printf("Qual o nome do carro que deseja remover?(O nome deve ser id�ntico ao nome cadastrado)\n");
        gets(nomeCarroRemovido);
    }

    abrirArquivo("RelacaoDosCarros.txt", "r");
    arquivoTemp = fopen("temp.txt", "w");

    while (fgets(linhaArq, tamLinha, arquivo) != NULL)
    {
        separaLinhaDeCarro(linhaArq, &carrosNoArquivo);

        int tamanhoNomeCarro = strlen(nomeCarroRemovido);

        if(!strcmp(carrosNoArquivo.nome, nomeCarroRemovido) == 0)
        {
            fprintf(arquivoTemp,"%s", linhaArq);
            qntdCarroNoArquivo++;
        }
        qntd++;
    }

    fechaArquivo(arquivo);
    fechaArquivo(arquivoTemp);

    abrirArquivo("RelacaoDosCarros.txt", "w");
    arquivoTemp = fopen("temp.txt", "r");

    passaConteudoArquivo(arquivo, arquivoTemp);

    fechaArquivo(arquivo);
    fechaArquivo(arquivoTemp);

    limpaTela();

    if(!ehChamadaAlteracao)
    {
        if(qntd != qntdCarroNoArquivo)
        {
            printf("Carro removido com sucesso!\n\n");
            printf("Deseja remover outro carro? (S) ou (N)\n");
        }
        else
        {
            printf("Nome inexistente no sistema!\n\n");
            printf("Deseja tentar novamente? (S) ou (N)\n");
        }


        scanf("%s", &opcao);

        if(opcao == 'S' || opcao == 's')
        {
            removeCarro(0);
        }
        else
        {
            menuPrincipal();
        }
    }
    cadastraCarroOrdenado(1); //Chama a fun��o de cadastrar carro, passando 1 como argumento, para n�o ter a op��o de cadastrar mais de um carro
}

/*void simulaFinancimaneto()
{
    system("cls");

    int meses, anoCarro;
    double valorCarro, valorEntrada, valorParcela, result, divisorParcial, divisorParcialElevado, divisorTotal, valorFinanciado;
    float IOF, cadastro = 756, avaliacao = 570, contrato = 250, CET;

    printf("Digite o valor do carro: ");
    scanf("%lf", &valorCarro);

    printf("Digite o valor de entrada que o cliente deseja(Sugerido = R$ %.2lf): ", valorCarro * 0.3);
    scanf("%lf", &valorEntrada);

    printf("Digite o ano do carro: ");
    scanf("%d", &anoCarro);

    printf("Digite a quantidade de meses do financiamento: ");
    scanf("%d", &meses);

    valorFinanciado = valorCarro - valorEntrada;

    CET = 1.5;

    if(anoCarro < 2021)
    {
        while (anoCarro < 2021)
        {
            CET += 0.025;
            anoCarro++;
        }
    }

    if(valorCarro < 15000)
    {
        CET += 2.3;
    }

    if(valorCarro < )

    divisorParcial = 1.0 / (1.0 + 0.0201);
    divisorParcialElevado = pow(divisorParcial, meses);
    divisorTotal = 1 - divisorParcialElevado;
    result = 0.0201 / divisorTotal;
    printf("\nO valor da parcela ficaria em torno de R$ %.2lf\n", valorFinanciado * result);
}*/

//CHAMA UMA APLICA��O EM C# PARA IMPRIMIR EM PDF OS CARROS CADASTRADOS NO PROGRAMA
void imprimeRelatorioCarros()
{
    limpaTela();
    popen("C:/RelatorioCarro/RelatorioPDFCarros.exe", "r");
    printf("Aperte qualquer tecla para o PDF com a tabela dos carros ser aberta...");
    getch();
    popen("C:/RelatorioCarro/TabelaAlfabetica.pdf", "r");
    menuPrincipal();
}

void imprimeRelatorioGastos()
{
    limpaTela();
    popen("C:/RelatorioCarro/RelatorioPDFCarros.exe", "r");
    printf("Aperte qualquer tecla para o PDF com a tabela dos carros ser aberta...");
    getch();
    popen("C:/RelatorioCarro/GastosMensais.pdf", "r");
    menuPrincipal();
}

void imprimeContratoCompraEVenda()
{
    //popen()
}

void visualizaEAdicionaGastosMensais()
{
    char o;

    limpaTela();
    limpaBufferTeclado();

    printf("------------Despesas do mes------------\n\n");
    printf("Qual opcao voce deseja?\n");
    printf("(1)Adicionar despesas\n");
    printf("(2)Remover despesas\n\n");
    printf("(3)Visualizar despesas do mes\n");
    printf("(4)Imprimir relatorio de despesas\n");

    printf("(0)Voltar para o menu principal\n");
    scanf("%c", &o);

    switch(o)
        {
            case 1:
                adicionaDespesas();
                break;

            case 2:
                removeDespesas();
                break;

            case 3:
                //alteraCarro();
                break;

            case 4:
                imprimeRelatorioGastos();
                break;

            default:
                menuPrincipal();
        }
}

void adicionaDespesas()
{
    int *pont;
    char tipo[20], valor[10], opcao;
    char nomeArquivo[30] = "Gastos Mensais.txt";
    char modoAbertura[3] = "a";

    limpaTela();
    limpaBufferTeclado();

    printf("------------Cadastro de despesas------------\n");
    printf("Digite o tipo da despesa: ");
    scanf("%[^\n]", &tipo);

    printf("Digite o valor da despesa: ");
    scanf("%s", &valor);

    //pont = abrirArquivo(nomeArquivo, modoAbertura);

//    fprintf(pont, "%s|%s|R$ %s\n", nome, ano, valor);

    limpaTela();
    fechaArquivo(pont);

    printf("Despesa cadastrada com sucesso!\n\n");

    printf("Deseja cadastrar outra despesa? (S) ou (N)\n");
    scanf("%s", &opcao);

    if(opcao == 'S' || opcao == 's')
    {
        adicionaDespesas();
    }
    else
    {
        menuPrincipal();
    }
}

void removeDespesas()
{
    char o;
    limpaTela();
    limpaBufferTeclado();

    printf("Qual opcao voce deseja?\n");
    printf("(1)Remover alguma despesa especifica\nzn");
    printf("(2)Zerar despesas\n");

    printf("(0)Voltar para o menu principal\n");

    scanf("%c", &o);

    switch(o)
    {
            case 1:
                removeDespesaEspecifica();
                break;

            case 2:
                zeraGastosMensais();
                removeDespesas();
                break;

            default:
                menuPrincipal();
    }
}

void removeDespesaEspecifica()
{

}

void zeraGastosMensais()
{
    remove("C:/Users/samir/Desktop/ProjetoGaragemCarro/ProjetoGaragem/ProjetoGaragemCarro/Gastos Mensais.txt");
}

void separaLinhaDeCarro(char *linha, carros *carro)
{
    int a = 0, b = 0, c = 0;
    int i = 0, k = 0;
    while(linha[i] != '\0')
    {
        int j = i;
        while(linha[j] != '\0' && linha[j] != '|')
        {
            switch(k)
            {
            case 0:
                carro->nome[a] = linha[j];
                a++;
                break;
            case 1:
                carro->ano[b] = linha[j];
                b++;
                break;
            case 2:
                carro->valor[c] = linha[j];
                c++;
                break;
            }
            j++;
        }
        i = j;

        if(linha[j] != '\0')
            i++;

        k++;
    }
    carro->nome[a] = carro->ano[b] = carro->valor[c] = '\0';
}

void cadastraCarroOrdenado(int ehChamadaAlteracao)
{
    carros carro;
    int ehCarroNaoCadastrado = 1, qntd;
    char opcao;
    char nomeArquivo[50] = "RelacaoDosCarros.txt";
    char modoAbertura[3] = "r";
    char *nomeCabecalho;

    if(ehChamadaAlteracao)
    {
        nomeCabecalho = "Altera��o";
    }
    else
    {
        nomeCabecalho = "Cadastro";
    }

    limpaTela();
    limpaBufferTeclado();

    printf("------------%s de carro------------\n", nomeCabecalho);
    printf("Digite o nome do carro: ");
    gets(carro.nome);

    printf("Digite o ano do carro: ");
    gets(carro.ano);

    printf("Digite o valor do carro: ");
    gets(carro.valor);

    abrirArquivo(nomeArquivo, modoAbertura);

    FILE *arquivoTemp = fopen("temp.txt", "w");

    if(arquivoTemp == NULL)
    {
        printf("Erro na abertura do arquivo");
        getch();
        exit(0);
    }

    const size_t tamLinha = 50;
    char* linhaArq = malloc(tamLinha);

    limpaBufferTeclado();

    while (fgets(linhaArq, tamLinha, arquivo) != NULL)
    {
        carros carroTemp;
        separaLinhaDeCarro(linhaArq, &carroTemp);

        if(ehCarroNaoCadastrado && (strcmp(carroTemp.nome, carro.nome) > 0))
        {
            fprintf(arquivoTemp, "\n%s|%s|R$ %s", carro.nome, carro.ano, carro.valor);
            ehCarroNaoCadastrado = 0;
        }

        fprintf(arquivoTemp, "\n%s|%s|%s", carroTemp.nome, carroTemp.ano, carroTemp.valor);

        printf(linhaArq);
        limpaBufferTeclado();
        qntd++;
    }

    if(ehCarroNaoCadastrado && !strstr(linhaArq, "R$") == NULL)
    {
        fprintf(arquivoTemp, "\n%s|%s|R$ %s", carro.nome, carro.ano, carro.valor);
    }

    fechaArquivo(arquivo);

    if(strstr(linhaArq, "R$") == NULL)
    {
        fprintf(arquivoTemp, "%s|%s|R$ %s", carro.nome, carro.ano, carro.valor);
    }

    free(linhaArq);

    limpaTela();

    fechaArquivo(arquivoTemp);

    arquivoTemp = fopen("temp.txt", "r");
    abrirArquivo(nomeArquivo, "w");

    passaConteudoArquivo(arquivo, arquivoTemp);

    fechaArquivo(arquivo);
    fechaArquivo(arquivoTemp);

    if(!ehChamadaAlteracao)
    {
        printf("Carro cadastrado com sucesso!\n\n");

        printf("Deseja cadastrar outro carro? (S) ou (N)\n");
        scanf("%s", &opcao);

        if(opcao == 'S' || opcao == 's')
        {
            cadastraCarroOrdenado(0);
        }
        else
        {
            menuPrincipal();
        }
    }
    menuPrincipal();
}

void passaConteudoArquivo(FILE *destino, FILE *origem)
{
    const size_t tamLinha = 50;
    char* linhaArq = malloc(tamLinha); //Aloca dinamicamente as linhas do arquivo

    while (fgets(linhaArq, tamLinha, origem) != NULL)//Traz do arquivo de origem, o cont�udo at� que seja NULL
    {
        if(strlen(linhaArq) > 3)//Condicional para eliminar do arquivo as linhas em branco
        {
            fprintf(destino, "%s", linhaArq);//Coloca dentro do arquivo, linha por linha do arquivo de origem
        }
    }
}
