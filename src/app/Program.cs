MenuPrincipal menu = new MenuPrincipal();
MenuListar menuListar = new MenuListar();
TarefaCSV tarefaCSV = new TarefaCSV();
ResponsavelCSV responsavelCSV = new ResponsavelCSV();

List<Responsavel> listaResponsaveis = new List<Responsavel>();
List<Tarefa> listaTarefas = new List<Tarefa>();

tarefaCSV.LerDados(listaTarefas, listaResponsaveis);
responsavelCSV.LerDados(listaResponsaveis);

void CadastrarResponsavel(List<Responsavel> listaResponsaveis)
{
    Console.Write("Nome do responsável: ");
    string nome = Console.ReadLine();
    Console.Write("Email do responsável: ");
    string email = Console.ReadLine();

    int proximoId;
    if (listaResponsaveis.Count == 0)
    {
        proximoId = 0;
    } else
    {
        proximoId = listaResponsaveis[listaResponsaveis.Count - 1].id + 1;
    }
    Responsavel responsavel = new Responsavel(proximoId, nome, email);
    listaResponsaveis.Add(responsavel);
    Console.WriteLine("Responsável cadastrado com sucesso.");
}

void CadastrarTarefa(List<Responsavel> listaResponsaveis, List<Tarefa> listaTarefas)
{
    Console.WriteLine("Título da tarefa: ");
    string titulo = Console.ReadLine();
    Console.WriteLine("Escolha a prioridade:");
    Console.WriteLine("1 - BAIXA");
    Console.WriteLine("2 - MÉDIA");
    Console.WriteLine("3 - ALTA");
    EPrioridade prioridade = (EPrioridade)Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Data limite (ano/mês/dia ex:2024/01/01): ");
    DateTime dataLimite = DateTime.Parse(Console.ReadLine());
    Console.WriteLine("Selecione o responsável para a tarefa:");
    foreach (Responsavel resp in listaResponsaveis)
    {
        Console.WriteLine($"{resp.id} - Nome: {resp.nome} E-mail: {resp.email}");
    }
    Console.WriteLine("Digite o código do responsável:");
    Responsavel responsavel = listaResponsaveis.Find(
        delegate(Responsavel resp)
        {
            return resp.id == Convert.ToInt32(Console.ReadLine());
        }
    );
    EStatus status = EStatus.PENDENTE;
    int proximoId;
    if (listaTarefas.Count == 0)
    {
        proximoId = 0;
    } else
    {
        proximoId = listaTarefas[listaTarefas.Count - 1].Id + 1;
    }
    Tarefa tarefa = new Tarefa(proximoId, titulo, dataLimite, status, prioridade, responsavel);
    listaTarefas.Add(tarefa);
    responsavel.AdicionarTarefa(tarefa);
}

void ExcluirTarefa(List<Tarefa> listaTarefas, List<Responsavel> listaResponsaveis)
{
    Console.WriteLine("Selecione a tarefa para ser excluída:");
    foreach (Tarefa tar in listaTarefas)
    {
        Console.WriteLine($"{tar.Id} - Título: {tar.Titulo}");
    }
    Console.WriteLine("Digite o código da tarefa:");
    int tarefaId = int.Parse(Console.ReadLine());
    Tarefa tarefa = listaTarefas.FirstOrDefault(t => t.Id == tarefaId);
    if (tarefa == null)
    {
        Console.WriteLine("Tarefa não encontrada.");
        return;
    }
    listaTarefas.Remove(tarefa);
    Console.WriteLine("Tarefa excluída com sucesso.");
}

static void AtualizarTarefa(List<Tarefa> listaTarefas, List<Responsavel> listaResponsaveis)
{
    Console.WriteLine("Selecione a tarefa para ser atualizada:");
    foreach (Tarefa tar in listaTarefas)
    {
        Console.WriteLine($"{tar.Id} - Título: {tar.Titulo}");
    }
    Console.WriteLine("Digite o código da tarefa:");
    int tarefaId = int.Parse(Console.ReadLine());
    Tarefa tarefa = listaTarefas.FirstOrDefault(t => t.Id == tarefaId);
    if (tarefa == null)
    {
        Console.WriteLine("Tarefa não encontrada.");
        return;
    }

    Console.WriteLine($"Tarefa selecionada: {tarefa.Titulo}");

    Console.WriteLine("Novo título da tarefa:");
    string novoTitulo = Console.ReadLine();
    if (!string.IsNullOrEmpty(novoTitulo))
    {
        tarefa.Titulo = novoTitulo;
    }

    Console.WriteLine("Nova data limite (ano/mês/dia ex:2024/01/01):");
    string novaDataLimiteStr = Console.ReadLine();
    if (!string.IsNullOrEmpty(novaDataLimiteStr))
    {
        if (DateTime.TryParse(novaDataLimiteStr, out DateTime novaDataLimite))
        {
            tarefa.DataLimite = novaDataLimite;
        }
        else
        {
            Console.WriteLine("Data inserida inválida. A data não será atualizada.");
        }
    }

    Console.WriteLine("Nova prioridade (1 - BAIXA, 2 - MÉDIA, 3 - ALTA):");
    string novaPrioridadeStr = Console.ReadLine();
    if (!string.IsNullOrEmpty(novaPrioridadeStr) && int.TryParse(novaPrioridadeStr, out int novaPrioridadeInt))
    {
        if (Enum.IsDefined(typeof(EPrioridade), novaPrioridadeInt))
        {
            tarefa.Prioridade = (EPrioridade)(novaPrioridadeInt);
        }
        else
        {
            Console.WriteLine("Prioridade inserida inválida. A prioridade não será atualizada.");
        }
    }

    Console.WriteLine("Nova responsável para a tarefa:");
    foreach (Responsavel resp in listaResponsaveis)
    {
        Console.WriteLine($"{resp.id} - Nome: {resp.nome} E-mail: {resp.email}");
    }
    Console.WriteLine("Digite o código do novo responsável:");
    string novoResponsavelId = Console.ReadLine();
    if (!string.IsNullOrEmpty(novoResponsavelId))
    {
        Responsavel novoResponsavel = listaResponsaveis.FirstOrDefault(r => r.id == int.Parse(novoResponsavelId));
        if (novoResponsavel != null)
        {
            tarefa.Responsavel = novoResponsavel;
        }
        else
        {
            Console.WriteLine("Responsável não encontrado. O responsável não será atualizado.");
        }
    }

    Console.WriteLine("Tarefa atualizada com sucesso.");
}


do 
{
    menu.Mostrar();

    switch (menu.opcao)
    {
        case 1:
            CadastrarResponsavel(listaResponsaveis);
            responsavelCSV.EscreverDados(listaResponsaveis);
            break;
        case 2:
            CadastrarTarefa(listaResponsaveis, listaTarefas);
            tarefaCSV.EscreverDados(listaTarefas);
            break;
        case 3:
            ExcluirTarefa(listaTarefas, listaResponsaveis);
            tarefaCSV.EscreverDados(listaTarefas);
            break;
        case 4:
            AtualizarTarefa(listaTarefas, listaResponsaveis);
            tarefaCSV.EscreverDados(listaTarefas);
            break;
        case 5:
            System.Console.WriteLine(menu.opcao);
            break;
        case 6:
            System.Console.WriteLine(menu.opcao);
            do {
                menuListar.Mostrar();
                switch (menuListar.opcao)
                {
                    case 1:
                        System.Console.WriteLine(menuListar.opcao);
                        break;
                    case 2:
                        System.Console.WriteLine(menuListar.opcao);
                        break;
                    case 3:
                        System.Console.WriteLine(menuListar.opcao);
                        break;
                }
            } while(menuListar.opcao != 0);
            break;
        case 7:
            System.Console.WriteLine(menu.opcao);
            break;
        case 0:
            System.Console.WriteLine("Até mais.");
            break;
    }
    Thread.Sleep(3000);
} while (menu.opcao != 0);

Console.Clear();
