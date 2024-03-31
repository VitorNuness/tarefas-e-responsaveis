class CSV
{
    private string? _BASE_PATH;
    public string? BASE_PATH
    {
        get => _BASE_PATH;
        set
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            string path2 = path.Remove(path.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
            string path3 = path2.Remove(path2.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
            string path4 = path3.Remove(path3.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
            string path5 = path4.Remove(path4.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
            _BASE_PATH = path5.Remove(path5.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
        }
    }

    private string? _DOCS_FOLDER_PATH;
    public string? DOCS_FOLDER_PATH
    {
        get => _DOCS_FOLDER_PATH;
        set
        {
            _DOCS_FOLDER_PATH = _BASE_PATH + "/Docs/";
        }
    }

    public void EscreverDados(string arquivo, object dado)
    {
        using (StreamWriter writer = new StreamWriter("Docs/" + arquivo, true))
        {
            writer.WriteLine(dado);
        }
    }

    public void LerDados(string arquivo, List<Responsavel> lista)
    {
        using (StreamReader reader = new StreamReader("Docs/" + arquivo))
        {
            Responsavel linha;
            // while ((linha = reader.ReadLine()) != null)
            // {
            //     lista.Add(linha);
            // }
        }
    }
}
