namespace MCQ3.DataConnect.Entities;

public class EmailVerification : BaseEntity
{
    public Guid UserId { get; set; }
    public UserAccount User { get; set; } = null!;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }
}

public class PasswordReset : BaseEntity
{
    public Guid UserId { get; set; }
    public UserAccount User { get; set; } = null!;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime? UsedAt { get; set; }
}

public class RefreshToken : BaseEntity
{
    public Guid UserId { get; set; }
    public UserAccount User { get; set; } = null!;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; } = false;
}