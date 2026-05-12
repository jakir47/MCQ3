using MCQ3.DataConnect.Enums;

namespace MCQ3.DataConnect.ViewModels;

public record MediaFileDto(
    Guid Id,
    string OriginalFileName,
    string? FileType,
    string Url,
    string MimeType,
    MediaType MediaType,
    long FileSizeBytes,
    string? AltText,
    string? VideoLinkUrl,
    DateTime CreatedAt,
    bool IsDeleted,
    DateTime? DeletedAt
);

public record BulkUploadResultDto(
    IReadOnlyList<MediaFileDto> Succeeded,
    IReadOnlyList<BulkUploadError> Failed
);

public record BulkUploadError(string FileName, string Error);

public record PagedResult<T>(
    IReadOnlyList<T> Items,
    int Page,
    int PageSize,
    int TotalCount
)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasNext => Page < TotalPages;
    public bool HasPrev => Page > 1;
}