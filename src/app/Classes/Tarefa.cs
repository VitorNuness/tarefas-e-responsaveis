class Tarefa
{
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
