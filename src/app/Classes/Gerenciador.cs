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
                Console.WriteLine($"{tar.Id} - Título: {tar.Titulo} Responsável: {tar.Responsavel.id} - {tar.Responsavel.nome}");
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

        EStatus antigoStatus = tarefa.Status;
        tarefa.Status = (EStatus)(int)tarefa.Status + 1;

        Responsavel responsavel = listaResponsaveis.FirstOrDefault(r => r == tarefa.Responsavel);
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

            if (responsavelId == "")
            {
                foreach (Tarefa tarefa in listaTarefas)
                {
                    Console.WriteLine($"{tarefa.Id} - {tarefa.Titulo} {DateOnly.FromDateTime(tarefa.DataLimite)} {tarefa.Status} {tarefa.Prioridade} {tarefa.Responsavel.nome}");
                }
            }else
            {
                foreach (Tarefa tarefa in listaTarefas)
                {
                    if (tarefa.Responsavel.id == int.Parse(responsavelId))
                    {
                        Console.WriteLine($"{tarefa.Id} - {tarefa.Titulo} {DateOnly.FromDateTime(tarefa.DataLimite)} {tarefa.Status} {tarefa.Prioridade} {tarefa.Responsavel.nome}");
                    }
                }
            }

        } else
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
                foreach (Tarefa tarefa in listaTarefas)
                {
                    if (tarefa.Status == EStatus.PENDENTE)
                    {
                        listaTarefasPendentes.Add(tarefa);
                        if (listaTarefasPendentes.Count() > 0)
                        {
                            foreach (Tarefa tarefaPendente in listaTarefasPendentes)
                            {
                                Console.WriteLine($"{tarefaPendente.Id} - {tarefaPendente.Titulo} {DateOnly.FromDateTime(tarefaPendente.DataLimite)} {tarefaPendente.Status} {tarefaPendente.Prioridade} {tarefaPendente.Responsavel.nome}");
                            }
                        }
                    }
                }
            } else
            {
                foreach (Tarefa tarefa in listaTarefas)
                {
                    if (tarefa.Status == EStatus.PENDENTE && tarefa.Responsavel.id == int.Parse(responsavelId))
                    {
                        listaTarefasPendentes.Add(tarefa);
                        if (listaTarefasPendentes.Count() > 0)
                        {
                            foreach (Tarefa tarefaPendente in listaTarefasPendentes)
                            {
                                Console.WriteLine($"{tarefaPendente.Id} - {tarefaPendente.Titulo} {DateOnly.FromDateTime(tarefaPendente.DataLimite)} {tarefaPendente.Status} {tarefaPendente.Prioridade} {tarefaPendente.Responsavel.nome}");
                            }
                        }
                    }
                }
            }
        } else
        {
            Console.WriteLine("Não há tarefas cadastradas!");
        }
        if (listaTarefasPendentes.Count() == 0)
        {
            Console.WriteLine("Não há tarefas pendentes!");
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
                foreach (Tarefa tarefa in listaTarefas)
                {
                    if (tarefa.Status == EStatus.FINALIZADO)
                    {
                        listaTarefasFinalizadas.Add(tarefa);
                        if (listaTarefasFinalizadas.Count() > 0)
                        {
                            foreach (Tarefa tarefaFinalizada in listaTarefasFinalizadas)
                            {
                                Console.WriteLine($"{tarefaFinalizada.Id} - {tarefaFinalizada.Titulo} {DateOnly.FromDateTime(tarefaFinalizada.DataLimite)} {tarefaFinalizada.Status} {tarefaFinalizada.Prioridade} {tarefaFinalizada.Responsavel.nome}");
                            }
                        }
                    }
                }
            }else
            {
                foreach (Tarefa tarefa in listaTarefas)
                {
                    if (tarefa.Status == EStatus.FINALIZADO && tarefa.Responsavel.id == int.Parse(responsavelId))
                    {
                        listaTarefasFinalizadas.Add(tarefa);
                        if (listaTarefasFinalizadas.Count() > 0)
                        {
                            foreach (Tarefa tarefaFinalizada in listaTarefasFinalizadas)
                            {
                                Console.WriteLine($"{tarefaFinalizada.Id} - {tarefaFinalizada.Titulo} {DateOnly.FromDateTime(tarefaFinalizada.DataLimite)} {tarefaFinalizada.Status} {tarefaFinalizada.Prioridade} {tarefaFinalizada.Responsavel.nome}");
                            }
                        }
                    }
                }
            }

        } else
        {
            Console.WriteLine("Não há tarefas cadastradas!");
        }
        if (listaTarefasFinalizadas.Count() == 0)
        {
            Console.WriteLine("Não há tarefas finalizadas!");
        }
        Console.WriteLine("\nAperte qualquer tecla para voltar ao menu.");
        Console.ReadKey();
    }

}
