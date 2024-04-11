// Vitor Nunes da Silva               RA: 1976026
// Maycon de Oliveira Macedo          RA: 1967269
// Mario Henrique Rabelo Dias         RA: 1971824

MenuPrincipal menu = new MenuPrincipal();
MenuListar menuListar = new MenuListar();
TarefaCSV tarefaCSV = new TarefaCSV();
ResponsavelCSV responsavelCSV = new ResponsavelCSV();
Gerenciador gerenciador = new Gerenciador();

List<Responsavel> listaResponsaveis = new List<Responsavel>();
List<Tarefa> listaTarefas = new List<Tarefa>();

responsavelCSV.LerDados(listaResponsaveis);
tarefaCSV.LerDados(listaTarefas, listaResponsaveis);

do 
{
    menu.Mostrar();

    switch (menu.opcao)
    {
        case 1:
            gerenciador.CadastrarResponsavel(listaResponsaveis);
            responsavelCSV.EscreverDados(listaResponsaveis);
            break;
        case 2:
            gerenciador.CadastrarTarefa(listaResponsaveis, listaTarefas);
            tarefaCSV.EscreverDados(listaTarefas);
            break;
        case 3:
            gerenciador.ExcluirTarefa(listaTarefas, listaResponsaveis);
            tarefaCSV.EscreverDados(listaTarefas);
            break;
        case 4:
            gerenciador.AtualizarTarefa(listaTarefas, listaResponsaveis);
            tarefaCSV.EscreverDados(listaTarefas);
            break;
        case 5:
            gerenciador.AtualizarStatus(listaTarefas, listaResponsaveis);
            tarefaCSV.EscreverDados(listaTarefas);
            break;
        case 6:
            do {
                menuListar.Mostrar();
                switch (menuListar.opcao)
                {
                    case 1:
                        gerenciador.ListarTarefas(listaTarefas, listaResponsaveis);
                        break;
                    case 2:
                        gerenciador.ListarTarefasPendentes(listaTarefas, listaResponsaveis);
                        break;
                    case 3:
                        gerenciador.ListarTarefasFinalizadas(listaTarefas, listaResponsaveis);
                        break;
                }
            } while(menuListar.opcao != 0);
            break;
        case 7:
            responsavelCSV.LerDados(listaResponsaveis);
            tarefaCSV.LerDados(listaTarefas, listaResponsaveis);
            break;
        case 0:
            Console.Clear();
            System.Console.WriteLine("Até mais.");
            break;
    }
    Thread.Sleep(3000);
} while (menu.opcao != 0);

Console.Clear();
