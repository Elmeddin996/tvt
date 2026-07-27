namespace TVT.Web.Settings;

public class FileSettings
{
    public long MaxFileSize { get; set; }

    public List<string> AllowedExtensions { get; set; } = new();
}
