using MCQ3.DataConnect.Enums;

namespace MCQ3.DataConnect.Entities;

public class MediaFile
{
    public Guid Id { get; set; }
    public string OriginalFileName { get; set; } = string.Empty;
    public string StoragePath { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public MediaType MediaType { get; set; }
    public long FileSizeBytes { get; set; }
    public string? AltText { get; set; }
    public string? VideoLinkUrl { get; set; }
    public Guid UploadedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedByUserId { get; set; }

    public UserAccount UploadedBy { get; set; } = null!;
    public UserAccount? DeletedBy { get; set; }
}