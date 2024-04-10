class Responsavel
{
    public Responsavel(int id, string nome, string email)
    {
        Responsavel.ValidaNome(nome);
        Responsavel.ValidaEmail(email);

        this._id = id;
        this._nome = nome;
        this._email = email;
    }

    public List<Tarefa> tarefas = new List<Tarefa>();

    private int _id;
    public int id
    {
        get => _id;
        set => _id = value;
    }

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
