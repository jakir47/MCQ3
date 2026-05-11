using System.ComponentModel;

namespace MCQ3.DataConnect.Requests;

public class LoginRequest
{
    [DefaultValue("admin@mcq2.com")]
    public string Email { get; set; } = string.Empty;
    [DefaultValue("admin123")]
    public string Password { get; set; } = string.Empty;
}