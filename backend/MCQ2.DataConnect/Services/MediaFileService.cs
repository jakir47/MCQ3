using MCQ3.DataConnect.Data;
using MCQ3.DataConnect.Entities;
using MCQ3.DataConnect.Enums;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MCQ3.DataConnect.Services;

public class MediaFileService(
    AppDbContext db,
    IFileStorageService storage,
    ILogger<MediaFileService> logger)
{
    private static readonly Dictionary<string, MediaType> MimeToType = new(StringComparer.OrdinalIgnoreCase)
    {
        { "image/jpeg", MediaType.Image }, { "image/png", MediaType.Image }, { "image/webp", MediaType.Image },
        { "image/gif", MediaType.Image }, { "image/bmp", MediaType.Image }, { "image/tiff", MediaType.Image }, { "image/svg+xml", MediaType.Image },
        { "audio/mpeg", MediaType.Audio }, { "audio/wav", MediaType.Audio }, { "audio/ogg", MediaType.Audio },
        { "audio/aac", MediaType.Audio }, { "audio/flac", MediaType.Audio }, { "audio/x-m4a", MediaType.Audio },
        { "application/pdf", MediaType.Document }, { "application/msword", MediaType.Document },
        { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", MediaType.Document },
        { "application/vnd.ms-excel", MediaType.Document },
        { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", MediaType.Document },
        { "application/vnd.ms-powerpoint", MediaType.Document },
        { "application/vnd.openxmlformats-officedocument.presentationml.presentation", MediaType.Document },
        { "text/plain", MediaType.Document }, { "text/csv", MediaType.Document },
        { "application/zip", MediaType.Document }, { "application/x-rar-compressed", MediaType.Document }
    };

    private static readonly Dictionary<MediaType, long> SizeLimits = new()
    {
        { MediaType.Image, 10 * 1024 * 1024 },
        { MediaType.Audio, 50 * 1024 * 1024 },
        { MediaType.Document, 20 * 1024 * 1024 }
    };

    private static readonly Dictionary<string, string[]> ExtensionMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { "image/jpeg", new[] { ".jpg", ".jpeg" } }, { "image/png", new[] { ".png" } },
        { "image/webp", new[] { ".webp" } }, { "image/gif", new[] { ".gif" } },
        { "image/bmp", new[] { ".bmp" } }, { "image/tiff", new[] { ".tiff", ".tif" } }, { "image/svg+xml", new[] { ".svg" } },
        { "audio/mpeg", new[] { ".mp3" } }, { "audio/wav", new[] { ".wav" } }, { "audio/ogg", new[] { ".ogg" } },
        { "audio/aac", new[] { ".aac" } }, { "audio/flac", new[] { ".flac" } }, { "audio/x-m4a", new[] { ".m4a" } },
        { "application/pdf", new[] { ".pdf" } }, { "application/msword", new[] { ".doc" } },
        { "application/vnd.openxmlformats-officedocument.wordprocessingml.document", new[] { ".docx" } },
        { "application/vnd.ms-excel", new[] { ".xls" } },
        { "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", new[] { ".xlsx" } },
        { "application/vnd.ms-powerpoint", new[] { ".ppt" } },
        { "application/vnd.openxmlformats-officedocument.presentationml.presentation", new[] { ".pptx" } },
        { "text/plain", new[] { ".txt" } }, { "text/csv", new[] { ".csv" } },
        { "application/zip", new[] { ".zip" } }, { "application/x-rar-compressed", new[] { ".rar" } }
    };

    public FileValidationResult Validate(IFormFile file, MediaType? declaredType = null)
    {
        if (file == null || file.Length == 0)
            return new FileValidationResult(false, "File is null or empty.");

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var mime = file.ContentType;

        if (!MimeToType.ContainsKey(mime))
            return new FileValidationResult(false, $"MIME type '{mime}' is not allowed.");

        var allowedExts = ExtensionMap.GetValueOrDefault(mime, Array.Empty<string>());
        if (!allowedExts.Contains(ext))
            return new FileValidationResult(false, $"Extension '{ext}' does not match MIME type '{mime}'.");

        var detectedType = MimeToType[mime];
        var sizeLimit = SizeLimits.GetValueOrDefault(detectedType, 0);
        if (file.Length > sizeLimit)
            return new FileValidationResult(false, $"File size exceeds {sizeLimit / 1024 / 1024} MB limit for {detectedType}.");

        return new FileValidationResult(true);
    }

    public async Task<(bool Success, MediaFileDto? Dto, string? Error)> UploadVideoLinkAsync(
        string videoUrl, string? title, Guid uploadedByUserId, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(videoUrl))
            return (false, null, "Video URL is required.");

        var isValid = videoUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                      videoUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
        if (!isValid)
            return (false, null, "Invalid video URL.");

        var mediaFile = new MediaFile
        {
            Id = Guid.NewGuid(),
            OriginalFileName = title ?? "Video Link",
            StoragePath = string.Empty,
            MimeType = "video/link",
            MediaType = MediaType.VideoLink,
            FileSizeBytes = 0,
            AltText = title,
            VideoLinkUrl = videoUrl,
            UploadedByUserId = uploadedByUserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.MediaFile.Add(mediaFile);
        await db.SaveChangesAsync(ct);

        return (true, new MediaFileDto(
            mediaFile.Id, mediaFile.OriginalFileName, string.Empty, videoUrl, mediaFile.MimeType,
            mediaFile.MediaType, mediaFile.FileSizeBytes, mediaFile.AltText,
            mediaFile.VideoLinkUrl, mediaFile.CreatedAt, mediaFile.IsDeleted, mediaFile.DeletedAt
        ), null);
    }

    public async Task<(bool Success, MediaFileDto? Dto, string? Error)> UploadAsync(
        IFormFile file, Guid uploadedByUserId, CancellationToken ct)
    {
        var validation = Validate(file);
        if (!validation.IsValid)
            return (false, null, validation.ErrorMessage);

        var mediaType = MimeToType[file.ContentType];
        string storagePath;

        try
        {
            storagePath = await storage.UploadAsync(file, mediaType, ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Storage upload failed for {FileName}", file.FileName);
            return (false, null, "Failed to save file to storage.");
        }

        var mediaFile = new MediaFile
        {
            Id = Guid.NewGuid(),
            OriginalFileName = file.FileName,
            StoragePath = storagePath,
            MimeType = file.ContentType,
            MediaType = mediaType,
            FileSizeBytes = file.Length,
            UploadedByUserId = uploadedByUserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        try
        {
            db.MediaFile.Add(mediaFile);
            await db.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DB persist failed for {FileName}, compensating storage delete", file.FileName);
            await storage.DeleteAsync(storagePath, ct);
            return (false, null, "Failed to save file record to database.");
        }

        var url = await storage.GetUrlAsync(storagePath, ct);
        return (true, new MediaFileDto(
            mediaFile.Id, mediaFile.OriginalFileName, Path.GetExtension(mediaFile.OriginalFileName), url, mediaFile.MimeType,
            mediaFile.MediaType, mediaFile.FileSizeBytes, mediaFile.AltText,
            mediaFile.VideoLinkUrl, mediaFile.CreatedAt, mediaFile.IsDeleted, mediaFile.DeletedAt
        ), null);
    }

    public async Task<BulkUploadResultDto> UploadBulkAsync(
        IReadOnlyList<IFormFile> files, Guid uploadedByUserId, CancellationToken ct)
    {
        var succeeded = new List<MediaFileDto>();
        var failed = new List<BulkUploadError>();

        foreach (var file in files)
        {
            var validation = Validate(file);
            if (!validation.IsValid)
            {
                failed.Add(new BulkUploadError(file.FileName, validation.ErrorMessage!));
                continue;
            }

            var (success, dto, error) = await UploadAsync(file, uploadedByUserId, ct);
            if (success && dto != null)
                succeeded.Add(dto);
            else
                failed.Add(new BulkUploadError(file.FileName, error ?? "Unknown error"));
        }

        return new BulkUploadResultDto(succeeded, failed);
    }

    public async Task<MediaFileDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var file = await db.MediaFile.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (file == null) return null;

        var url = await storage.GetUrlAsync(file.StoragePath, ct);
        return new MediaFileDto(
            file.Id, file.OriginalFileName, Path.GetExtension(file.OriginalFileName), url, file.MimeType,
            file.MediaType, file.FileSizeBytes, file.AltText,
            file.VideoLinkUrl, file.CreatedAt, file.IsDeleted, file.DeletedAt
        );
    }

    public async Task<PagedResult<MediaFileDto>> GetPagedAsync(
        MediaType? filterByType, int page, int pageSize, CancellationToken ct)
    {
        pageSize = Math.Min(Math.Max(pageSize, 1), 100);
        var query = db.MediaFile.AsQueryable();

        if (filterByType.HasValue)
            query = query.Where(x => x.MediaType == filterByType.Value);

        var total = await query.CountAsync(ct);
        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        var dtos = new List<MediaFileDto>();
        foreach (var file in items)
        {
            var url = await storage.GetUrlAsync(file.StoragePath, ct);
            dtos.Add(new MediaFileDto(
                file.Id, file.OriginalFileName, Path.GetExtension(file.OriginalFileName), url, file.MimeType,
                file.MediaType, file.FileSizeBytes, file.AltText,
                file.VideoLinkUrl, file.CreatedAt, file.IsDeleted, file.DeletedAt
            ));
        }

        return new PagedResult<MediaFileDto>(dtos, page, pageSize, total);
    }

    public async Task<(bool Success, string? Error)> SoftDeleteAsync(
        Guid id, Guid deletedByUserId, CancellationToken ct)
    {
        var file = await db.MediaFile.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (file == null) return (false, "File not found.");
        if (file.IsDeleted) return (false, "File is already deleted.");

        file.IsDeleted = true;
        file.DeletedAt = DateTime.UtcNow;
        file.DeletedByUserId = deletedByUserId;
        file.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<MediaFileDto?> RestoreAsync(Guid id, CancellationToken ct)
    {
        var file = await db.MediaFile.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (file == null) return null;
        if (!file.IsDeleted) return null;

        file.IsDeleted = false;
        file.DeletedAt = null;
        file.DeletedByUserId = null;
        file.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(ct);

        var url = await storage.GetUrlAsync(file.StoragePath, ct);
        return new MediaFileDto(
            file.Id, file.OriginalFileName, Path.GetExtension(file.OriginalFileName), url, file.MimeType,
            file.MediaType, file.FileSizeBytes, file.AltText,
            file.VideoLinkUrl, file.CreatedAt, file.IsDeleted, file.DeletedAt
        );
    }

    public async Task<(bool Success, string? Error)> HardDeleteAsync(Guid id, CancellationToken ct)
    {
        var file = await db.MediaFile.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (file == null) return (false, "File not found.");
        if (!file.IsDeleted) return (false, "Soft-delete first before purging.");

        await storage.DeleteAsync(file.StoragePath, ct);
        db.MediaFile.Remove(file);
        await db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<MediaFileDto?> UpdateAltTextAsync(Guid id, string? altText, CancellationToken ct)
    {
        var file = await db.MediaFile.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (file == null) return null;

        file.AltText = altText;
        file.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(ct);

        var url = await storage.GetUrlAsync(file.StoragePath, ct);
        return new MediaFileDto(
            file.Id, file.OriginalFileName, Path.GetExtension(file.OriginalFileName), url, file.MimeType,
            file.MediaType, file.FileSizeBytes, file.AltText,
            file.VideoLinkUrl, file.CreatedAt, file.IsDeleted, file.DeletedAt
        );
    }
}