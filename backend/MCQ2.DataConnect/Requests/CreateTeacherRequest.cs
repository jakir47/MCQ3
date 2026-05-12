namespace MCQ3.DataConnect.Requests;

public class CreateTeacherRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? NID { get; set; }
}

public class UpdateTeacherRequest
{
    public string? FullName { get; set; }
    public bool? IsActive { get; set; }
    public string? Title { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? NID { get; set; }
}