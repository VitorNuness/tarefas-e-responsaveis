class CSV
{
    public CSV()
    {
        this._BASE_PATH = getBasePath();
        this._DOCS_FOLDER_PATH = BASE_PATH + "Docs" + System.IO.Path.DirectorySeparatorChar;
    }

    private string? _BASE_PATH;
    public string? BASE_PATH
    {
        get => _BASE_PATH;
    }

    private string? _DOCS_FOLDER_PATH;
    public string? DOCS_FOLDER_PATH
    {
        get => _DOCS_FOLDER_PATH;
    }

    static string getBasePath()
    {
        string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        string path2 = path.Remove(path.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
        string path3 = path2.Remove(path2.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
        string path4 = path3.Remove(path3.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
        return path4.Remove(path4.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
    }
}
