class TarefaCSV : CSV
{
    public TarefaCSV()
    {
        this._TAREFAS_FILE = DOCS_FOLDER_PATH + "tarefas.csv";
    }

    private string? _TAREFAS_FILE;
    public string? TAREFAS_FILE
    {   
        get => _TAREFAS_FILE;
    }

    public void EscreverDados(List<Tarefa> tarefas)
    {
        using (StreamWriter writer = new StreamWriter(TAREFAS_FILE, false))
        {
            foreach (Tarefa tarefa in tarefas)
            {
                writer.WriteLine($"{tarefa.Id};{tarefa.Titulo};{tarefa.DataLimite};{(int)tarefa.Status};{(int)tarefa.Prioridade};{tarefa.Responsavel.id};");
            }
        }
    }

    public void LerDados(List<Tarefa> lista, List<Responsavel> responsaveis)
    {
        lista.Clear();
        using (StreamReader reader = new StreamReader(TAREFAS_FILE))
        {
            string linha;
            while ((linha = reader.ReadLine()) != null)
            {
                string[] valores = linha.Split(";");
                int id = Convert.ToInt32(valores[0]);
                string titulo = valores[1];
                DateTime dataLimite = DateTime.Parse(valores[2]);
                EStatus status = (EStatus)Convert.ToInt32(valores[3]);
                EPrioridade prioridade = (EPrioridade)Convert.ToInt32(valores[4]);
                int responsavelId = Convert.ToInt32(valores[5]);
                Responsavel responsavel = responsaveis.FirstOrDefault(r => r.id == responsavelId);
                Tarefa tarefa = new Tarefa(id, titulo, dataLimite, status, prioridade, responsavel);
                lista.Add(tarefa);
            }
        }
    }
}
