class Responsavel
{
    public Responsavel(string nome, string email)
    {
        Responsavel.ValidaNome(nome);
        Responsavel.ValidaEmail(email);

        this._nome = nome;
        this._email = email;
    }

    List<Tarefa> tarefas = new List<Tarefa>();

    private string _nome;
    public string nome
    {
        get => _nome;
        set
        {
            ValidaNome(value);
            _nome = value;
        }
    }

    private string _email;
    public string email
    {
        get => _email;
        set
        {
            ValidaEmail(value);
            _email = value;
        }
    }

    private static void ValidaNome(string nome)
    {
        if (nome == "" || nome == " " || nome.Length < 3)
        {
            throw new Exception("Nome inválido.");
        }
    }

    private static void ValidaEmail(string email)
    {
        if (email == "" || email == " " || email.Contains("@") == false)
        {
            throw new Exception("E-mail inválido.");
        }
    }

    public void AdicionarTarefa(Tarefa tarefa)
    {
        tarefas.Add(tarefa);
    }
}
