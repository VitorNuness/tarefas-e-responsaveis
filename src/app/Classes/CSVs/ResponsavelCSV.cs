class ResponsavelCSV : CSV
{
    public ResponsavelCSV()
    {
        this._RESPONSAVEL_FILE = DOCS_FOLDER_PATH + "responsavel.csv";
    }

    private string? _RESPONSAVEL_FILE;
    public string? RESPONSAVEL_FILE
    {
        get => _RESPONSAVEL_FILE;
    }

    public void EscreverDados(List<Responsavel> responsaveis)
    {
        using (StreamWriter writer = new StreamWriter(RESPONSAVEL_FILE, false))
        {
            foreach (Responsavel responsavel in responsaveis)
            {
                writer.WriteLine($"{responsavel.id};{responsavel.nome};{responsavel.email};");
            }
        }
    }

    public void LerDados(List<Responsavel> lista)
    {
        lista.Clear();
        using (StreamReader reader = new StreamReader(RESPONSAVEL_FILE))
        {
            string linha;
            while ((linha = reader.ReadLine()) != null)
            {
                string[] valores = linha.Split(";");
                int id = Convert.ToInt32(valores[0]);
                string nome = valores[1];
                string email = valores[2];
                Responsavel responsavel = new Responsavel(id, nome, email);
                lista.Add(responsavel);
            }
        }
    }
}
