namespace TVT.Web.Models;

public class FileUploadResult
{
    public bool Success { get; set; }

    public string? FilePath { get; set; }

    public string? ErrorMessage { get; set; }

    public static FileUploadResult Successful(string filePath)
    {
        return new FileUploadResult
        {
            Success = true,
            FilePath = filePath
        };
    }

    public static FileUploadResult Failed(string errorMessage)
    {
        return new FileUploadResult
        {
            Success = false,
            ErrorMessage = errorMessage
        };
    }
}
