class MenuListar
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
| LISTAR TAREFAS |
+----------------+

1 - Listar tarefas
2 - Listar tarefas pendentes
3 - Listar tarefas concluídas
0 - Sair
");
        int opcaoSelecionada = Convert.ToInt32(Console.ReadLine());
        VerificarOpcao(opcaoSelecionada);
        _opcao = opcaoSelecionada;
    }

    private void VerificarOpcao(int opcao)
    {
        if (opcao < 0 || opcao > 3)
        {
            throw new Exception("Opção do menu inválida!");
        }
    }
}
