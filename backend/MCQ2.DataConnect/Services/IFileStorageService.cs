using MCQ3.DataConnect.Enums;
using Microsoft.AspNetCore.Http;

namespace MCQ3.DataConnect.Services;

public interface IFileStorageService
{
    Task<string> UploadAsync(IFormFile file, MediaType mediaType, CancellationToken ct);
    Task DeleteAsync(string storagePath, CancellationToken ct);
    Task<string> GetUrlAsync(string storagePath, CancellationToken ct);
}