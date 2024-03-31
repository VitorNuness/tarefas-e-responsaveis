class MenuPrincipal
{
    private int _opcao;
    public int opcao
    {
        get => _opcao;
    }

    public void Mostrar()
    {
        Console.Clear();
        System.Console.WriteLine(@"
+----------------+
| MENU PRINCIPAL |
+----------------+

1 - Cadastrar um novo responsável
2 - Cadastrar uma nova tarefa
3 - Excluir uma tarefa
4 - Alterar informações de uma tarefa
5 - Atualizar o status de uma tarefa
6 - Listar tarefas
7 - Recarregar informações
0 - Sair
");
        int opcaoSelecionada = Convert.ToInt32(Console.ReadLine());
        VerificarOpcao(opcaoSelecionada);
        _opcao = opcaoSelecionada;
    }

    private void VerificarOpcao(int opcao)
    {
        if (opcao < 0 || opcao > 7)
        {
            throw new Exception("Opção do menu inválida!");
        }
    }
}
