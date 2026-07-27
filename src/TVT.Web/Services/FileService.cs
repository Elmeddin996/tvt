using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TVT.Web.Models;
using TVT.Web.Settings;

namespace TVT.Web.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;
    private readonly FileSettings _settings;

    public FileService(
        IWebHostEnvironment environment,
        IOptions<FileSettings> options)
    {
        _environment = environment;
        _settings = options.Value;
    }

    public async Task<FileUploadResult> UploadAsync(IFormFile? file, string folderName)
    {
        if (file == null || file.Length == 0)
        {
            return FileUploadResult.Failed("Please select a file.");
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!_settings.AllowedExtensions.Contains(extension))
        {
            return FileUploadResult.Failed(
                $"Only {string.Join(", ", _settings.AllowedExtensions)} files are allowed.");
        }

        if (file.Length > _settings.MaxFileSize)
        {
            var maxSizeMb = _settings.MaxFileSize / 1024 / 1024;

            return FileUploadResult.Failed(
                $"Maximum file size is {maxSizeMb} MB.");
        }

        var uploadsPath = Path.Combine(
            _environment.WebRootPath,
            "uploads",
            folderName);

        if (!Directory.Exists(uploadsPath))
        {
            Directory.CreateDirectory(uploadsPath);
        }

        var fileName = $"{Guid.NewGuid()}{extension}";

        var filePath = Path.Combine(uploadsPath, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return FileUploadResult.Successful(
            $"/uploads/{folderName}/{fileName}");
    }

    public Task DeleteAsync(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return Task.CompletedTask;

        var fullPath = Path.Combine(
            _environment.WebRootPath,
            filePath.TrimStart('/')
                    .Replace('/', Path.DirectorySeparatorChar));

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }
}
