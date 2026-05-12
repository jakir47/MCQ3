using MCQ3.DataConnect.Enums;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MCQ3.DataConnect.Services;

public class LocalFileStorageService(
    IHostEnvironment env,
    ILogger<LocalFileStorageService> logger) : IFileStorageService
{
    private static readonly Dictionary<MediaType, string> FolderMap = new()
    {
        { MediaType.Image, "image" },
        { MediaType.Audio, "audio" },
        { MediaType.Document, "document" }
    };

    public async Task<string> UploadAsync(IFormFile file, MediaType mediaType, CancellationToken ct)
    {
        var folder = FolderMap.GetValueOrDefault(mediaType, "other");
        var year = DateTime.UtcNow.Year;
        var dir = Path.Combine(env.ContentRootPath, "uploads", folder, year.ToString());
        Directory.CreateDirectory(dir);

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var name = $"{Guid.NewGuid()}{ext}";
        var path = Path.Combine(dir, name);

        await using var fs = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fs, ct);

        return Path.Combine("uploads", folder, year.ToString(), name).Replace("\\", "/");
    }

    public Task DeleteAsync(string storagePath, CancellationToken ct)
    {
        var fullPath = Path.Combine(env.ContentRootPath, storagePath.Replace("/", "\\"));
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        else
        {
            logger.LogWarning("File not found for deletion: {Path}", fullPath);
        }
        return Task.CompletedTask;
    }

    public Task<string> GetUrlAsync(string storagePath, CancellationToken ct)
    {
        var url = "/" + storagePath.Replace("\\", "/");
        return Task.FromResult(url);
    }
}