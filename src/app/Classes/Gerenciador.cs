class Gerenciador
{
    public void CadastrarResponsavel(List<Responsavel> listaResponsaveis)
    {
        Console.WriteLine("Preencha os campos adequadamente ou pressione Enter no Nome do responsável para sair.");
        Console.Write("Nome do responsável: ");
        string nome = Console.ReadLine();

        if (nome == "")
        {
            return;
        }

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

    public void CadastrarTarefa(List<Responsavel> listaResponsaveis, List<Tarefa> listaTarefas)
    {
        Console.WriteLine("Preencha os campos adequadamente ou pressione Enter no Título para sair.");
        Console.WriteLine("Título da tarefa: ");
        string titulo = Console.ReadLine();

        if (titulo == "")
        {
            return;
        }

        Console.WriteLine("Escolha a prioridade:");
        Console.WriteLine("1 - BAIXA");
        Console.WriteLine("2 - MÉDIA");
        Console.WriteLine("3 - ALTA");
        EPrioridade prioridade = (EPrioridade)Convert.ToInt32(Console.ReadLine());

        DateTime data;
        int dataDif = 0;
        do {
            Console.WriteLine("Data limite (ano/mês/dia ex:2024/01/01): ");
            data = DateTime.Parse(Console.ReadLine());
            dataDif = (int)data.Subtract(DateTime.Today).TotalDays;
            if (dataDif > 7 && prioridade == EPrioridade.ALTA)
            {
                Console.WriteLine("Uma tarefa de alta prioridade não pode ser realizada em menos de 7 dias.");
            }
        } while(dataDif > 7 && prioridade == EPrioridade.ALTA);

        DateTime dataLimite = data;

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

    public void ExcluirTarefa(List<Tarefa> listaTarefas, List<Responsavel> listaResponsaveis)
    {
        Console.WriteLine("Selecione a tarefa para ser excluída ou Enter para voltar:");
        foreach (Tarefa tar in listaTarefas)
        {
            Console.WriteLine($"{tar.Id} - Título: {tar.Titulo}");
        }
        Console.WriteLine("Digite o código da tarefa:");
        string tarefaId = Console.ReadLine();

        if (tarefaId == "")
        {
            return;
        }

        Tarefa tarefa = listaTarefas.FirstOrDefault(t => t.Id == int.Parse(tarefaId));
        if (tarefa == null)
        {
            Console.WriteLine("Tarefa não encontrada.");
            return;
        }

        foreach (Responsavel responsavel in listaResponsaveis)
        {
            Tarefa tarefaDoResponsavel = responsavel.tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
            if (tarefaDoResponsavel != null)
            {
                responsavel.tarefas.Remove(tarefaDoResponsavel);
                break;
            }
        }

        listaTarefas.Remove(tarefa);
        Console.WriteLine("Tarefa excluída com sucesso.");
    }


    public void AtualizarTarefa(List<Tarefa> listaTarefas, List<Responsavel> listaResponsaveis)
    {
        Console.Clear();

        if (listaTarefas.Count() <= 0)
        {
            Console.WriteLine("Não há tarefas para serem atualizadas.\nPressione qualquer tecla.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Selecione a tarefa para ser atualizada ou aperte Enter para voltar:");
        foreach (Tarefa tar in listaTarefas)
        {
            Console.WriteLine($"{tar.Id} - Título: {tar.Titulo} Responsável: {tar.Responsavel.id} - {tar.Responsavel.nome}");
        }
        Console.WriteLine("Digite o código da tarefa:");
        string tarefaId = Console.ReadLine();

        if (tarefaId == "")
        {
            return;
        }

        Tarefa tarefa = listaTarefas.FirstOrDefault(t => t.Id == int.Parse(tarefaId));

        Console.WriteLine($"Tarefa selecionada: {tarefa.Titulo}");

        Console.WriteLine("Novo título da tarefa:");
        string novoTitulo = Console.ReadLine();
        if (novoTitulo != "")
        {
            tarefa.Titulo = novoTitulo;
        }

        Console.WriteLine("Nova data limite (ano/mês/dia ex:2024/01/01):");
        string novaDataLimiteStr = Console.ReadLine();
        if (novaDataLimiteStr != "")
        {
            tarefa.DataLimite = DateTime.Parse(novaDataLimiteStr);
        }

        Console.WriteLine("Nova prioridade (1 - BAIXA, 2 - MÉDIA, 3 - ALTA):");
        string novaPrioridadeStr = Console.ReadLine();
        if (novaPrioridadeStr != "")
        {
            if (Enum.IsDefined(typeof(EPrioridade), int.Parse(novaPrioridadeStr)))
            {
                tarefa.Prioridade = (EPrioridade)int.Parse(novaPrioridadeStr);
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
        if (novoResponsavelId != "")
        {
            Responsavel novoResponsavel = listaResponsaveis.FirstOrDefault(r => r.id == int.Parse(novoResponsavelId));
            if (novoResponsavel != null)
            {
                Responsavel antigoResponsavel = listaResponsaveis.FirstOrDefault(ar => ar == tarefa.Responsavel);
                antigoResponsavel.tarefas.Remove(tarefa);
                
                tarefa.Responsavel = novoResponsavel;
                Tarefa tarefaResponsavel = novoResponsavel.tarefas.FirstOrDefault(tr => tr.Id == tarefa.Id);
                tarefaResponsavel = tarefa;
            }
            else
            {
                Console.WriteLine("Responsável não encontrado. O responsável não será atualizado.");
            }
        }

        Console.WriteLine("Tarefa atualizada com sucesso.");
    }

    public void AtualizarStatus(List<Tarefa> listaTarefas, List<Responsavel> listaResponsaveis)
    {
        Console.Clear();
        if (listaTarefas.Count() <= 0)
        {
            Console.WriteLine("Não há tarefas para serem atualizadas.\nPressione qualquer tecla.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Selecione a tarefa para ser atualizada ou aperte Enter para voltar:");
        foreach (Tarefa tar in listaTarefas)
        {
            if (tar.Status != EStatus.FINALIZADO)
            {
                Console.WriteLine($"{tar.Id} - Título: {tar.Titulo} {tar.Status} Responsável: {tar.Responsavel.id} - {tar.Responsavel.nome}");
            }
        }
        Console.WriteLine("Digite o código da tarefa:");
        string tarefaId = Console.ReadLine();

        if (tarefaId == "")
        {
            return;
        }

        Tarefa tarefa = listaTarefas.FirstOrDefault(t => t.Id == int.Parse(tarefaId));

        Console.WriteLine($"Tarefa selecionada: {tarefa.Titulo}");

        if (tarefa.Status == EStatus.FINALIZADO)
        {
            Console.WriteLine("Está tarefa já está finalizada!");
        }
        
        Responsavel responsavel = listaResponsaveis.FirstOrDefault(r => r == tarefa.Responsavel);
        int tarefasResponsavelEmAndamento = 0;

        foreach (Tarefa tar in responsavel.tarefas)
        {
            if (tar.Status == EStatus.ANDAMENTO)
            {
                tarefasResponsavelEmAndamento++;
            }
        }

        if (tarefasResponsavelEmAndamento == 3)
        {
            Console.WriteLine("O responsável já possui 3 tarefas em andamento.");
            return;
        }

        EStatus antigoStatus = tarefa.Status;
        tarefa.Status = (EStatus)(int)tarefa.Status + 1;

        Tarefa tarefaResponsavel = responsavel.tarefas.FirstOrDefault(tr => tr == tarefa);
        tarefaResponsavel = tarefa;

        Console.WriteLine($"Status atualizado de {antigoStatus} para {tarefa.Status}.");

    }

    public void ListarTarefas(List<Tarefa> listaTarefas, List<Responsavel> listaResponsaveis)
    {
        Console.Clear();

        if (listaTarefas.Count() > 0)
        {
            Console.WriteLine("Selecione o responsável ou aperte Enter para listar todas as tarefas.");
            foreach (Responsavel resp in listaResponsaveis)
            {
                Console.WriteLine($"{resp.id} - Nome: {resp.nome} E-mail: {resp.email}");
            }
            Console.WriteLine("Digite o código do responsável:");
            string responsavelId = Console.ReadLine();

            Console.WriteLine("Deseja ordenar por (P)rioridade ou (D)ata? (P/D)");
            string ordenacao = Console.ReadLine().ToUpper();

            IEnumerable<Tarefa> tarefasFiltradas = listaTarefas;

            if (responsavelId == "")
            {
                tarefasFiltradas = listaTarefas;
            }
            else
            {
                tarefasFiltradas = listaTarefas.Where(t => t.Responsavel.id == int.Parse(responsavelId));
            }

            switch (ordenacao)
            {
                case "P":
                    tarefasFiltradas = tarefasFiltradas.OrderBy(t => t.Prioridade);
                    break;
                case "D":
                    tarefasFiltradas = tarefasFiltradas.OrderBy(t => t.DataLimite);
                    break;
                default:
                    Console.WriteLine("Opção inválida. Listando sem ordenação.");
                    break;
            }

            foreach (Tarefa tarefa in tarefasFiltradas)
            {
                Console.WriteLine($"{tarefa.Id} - {tarefa.Titulo} {DateOnly.FromDateTime(tarefa.DataLimite)} {tarefa.Status} {tarefa.Prioridade} {tarefa.Responsavel.nome}");
            }
        }
        else
        {
            Console.WriteLine("Não há tarefas cadastradas!");
        }

        Console.WriteLine("\nAperte qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }

    public void ListarTarefasPendentes(List<Tarefa> listaTarefas, List<Responsavel> listaResponsaveis)
    {
        Console.Clear();
        List<Tarefa> listaTarefasPendentes = new List<Tarefa>();
        if (listaTarefas.Count() > 0)
        {
            Console.WriteLine("Selecione o responsável ou aperte Enter para listar todas as tarefas.");
            foreach (Responsavel resp in listaResponsaveis)
            {
                Console.WriteLine($"{resp.id} - Nome: {resp.nome} E-mail: {resp.email}");
            }
            Console.WriteLine("Digite o código do responsável:");
            string responsavelId = Console.ReadLine();

            if (responsavelId == "")
            {
                listaTarefasPendentes = listaTarefas.Where(t => t.Status == EStatus.PENDENTE).ToList();
            }
            else
            {
                listaTarefasPendentes = listaTarefas.Where(t => t.Status == EStatus.PENDENTE && t.Responsavel.id == int.Parse(responsavelId)).ToList();
            }

            if (listaTarefasPendentes.Count > 0)
            {
                Console.WriteLine("Deseja ordenar por (P)rioridade ou (D)ata? (P/D)");
                string ordenacao = Console.ReadLine().ToUpper();

                switch (ordenacao)
                {
                    case "P":
                        listaTarefasPendentes = listaTarefasPendentes.OrderBy(t => t.Prioridade).ToList();
                        break;
                    case "D":
                        listaTarefasPendentes = listaTarefasPendentes.OrderBy(t => t.DataLimite).ToList();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Listando sem ordenação.");
                        break;
                }

                foreach (Tarefa tarefaPendente in listaTarefasPendentes)
                {
                    Console.WriteLine($"{tarefaPendente.Id} - {tarefaPendente.Titulo} {DateOnly.FromDateTime(tarefaPendente.DataLimite)} {tarefaPendente.Status} {tarefaPendente.Prioridade} {tarefaPendente.Responsavel.nome}");
                }
            }
            else
            {
                Console.WriteLine("Não há tarefas pendentes!");
            }
        }
        else
        {
            Console.WriteLine("Não há tarefas cadastradas!");
        }

        Console.WriteLine("\nAperte qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }

    public void ListarTarefasFinalizadas(List<Tarefa> listaTarefas, List<Responsavel> listaResponsaveis)
    {
        Console.Clear();
        List<Tarefa> listaTarefasFinalizadas = new List<Tarefa>();
        if (listaTarefas.Count() > 0)
        {
            Console.WriteLine("Selecione o responsável ou aperte Enter para listar todas as tarefas.");
            foreach (Responsavel resp in listaResponsaveis)
            {
                Console.WriteLine($"{resp.id} - Nome: {resp.nome} E-mail: {resp.email}");
            }
            Console.WriteLine("Digite o código do responsável:");
            string responsavelId = Console.ReadLine();

            if (responsavelId == "")
            {
                listaTarefasFinalizadas = listaTarefas.Where(t => t.Status == EStatus.FINALIZADO).ToList();
            }
            else
            {
                listaTarefasFinalizadas = listaTarefas.Where(t => t.Status == EStatus.FINALIZADO && t.Responsavel.id == int.Parse(responsavelId)).ToList();
            }

            if (listaTarefasFinalizadas.Count > 0)
            {
                Console.WriteLine("Deseja ordenar por (P)rioridade ou (D)ata? (P/D)");
                string ordenacao = Console.ReadLine().ToUpper();

                switch (ordenacao)
                {
                    case "P":
                        listaTarefasFinalizadas = listaTarefasFinalizadas.OrderBy(t => t.Prioridade).ToList();
                        break;
                    case "D":
                        listaTarefasFinalizadas = listaTarefasFinalizadas.OrderBy(t => t.DataLimite).ToList();
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Listando sem ordenação.");
                        break;
                }

                foreach (Tarefa tarefaFinalizada in listaTarefasFinalizadas)
                {
                    Console.WriteLine($"{tarefaFinalizada.Id} - {tarefaFinalizada.Titulo} {DateOnly.FromDateTime(tarefaFinalizada.DataLimite)} {tarefaFinalizada.Status} {tarefaFinalizada.Prioridade} {tarefaFinalizada.Responsavel.nome}");
                }
            }
            else
            {
                Console.WriteLine("Não há tarefas finalizadas!");
            }
        }
        else
        {
            Console.WriteLine("Não há tarefas cadastradas!");
        }

        Console.WriteLine("\nAperte qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }
}


























































































































































































































































































































































































































































































































































































































































































// Hello world XD
