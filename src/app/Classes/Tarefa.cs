class Tarefa
{
    public Tarefa(int id, string titulo, DateTime dataLimite, EStatus status, EPrioridade prioridade, Responsavel responsavel)
    {
        this._id = id;
        this._titulo = titulo;
        this._dataLimite = dataLimite;
        this._status = status;
        this._prioridade = prioridade;
        this._responsavel = responsavel;
    }

    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    private string _titulo;
    public string Titulo
    {
        get => _titulo;
        set => _titulo = value;
    }

    private DateTime _dataLimite;
    public DateTime DataLimite
    {
        get => _dataLimite;
        set => _dataLimite = value;
    }

    private EStatus _status;
    public EStatus Status
    {
        get => _status;
        set => _status = value;
    }

    private EPrioridade _prioridade;
    public EPrioridade Prioridade
    {
        get => _prioridade;
        set => _prioridade = value;
    }

    private Responsavel _responsavel;
    public Responsavel Responsavel
    {
        get => _responsavel;
        set => _responsavel = value;
    }

    private void ValidaTitulo(string titulo)
    {
        if (titulo == "" || titulo == " ")
        {
            throw new Exception("Título inválido.");
        }
    }

    private void ValidaData(DateTime data)
    {
        int diferencaDeDatas = DateTime.Compare(data, DateTime.Today);
        if (_prioridade == EPrioridade.ALTA && diferencaDeDatas > 7)
        {
            throw new Exception("Uma tarefa de prioridade alta, deve ser concluída em menos de uma semana (7 dias).");
        }
    }
}
