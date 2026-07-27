using Microsoft.AspNetCore.Http;
using TVT.Web.Models;

namespace TVT.Web.Services;

public interface IFileService
{
    Task<FileUploadResult> UploadAsync(IFormFile? file, string folderName);

    Task DeleteAsync(string? filePath);
}
