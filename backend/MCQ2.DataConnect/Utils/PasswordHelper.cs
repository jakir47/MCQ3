namespace MCQ3.DataConnect.Utils;

public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    public static string GenerateSecureToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}