namespace MCQ3.DataConnect.Requests;

public class CreateStudentRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Code { get; set; }
    public string? NID { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? FatherName { get; set; }
    public string? FatherContact { get; set; }
    public string? MotherName { get; set; }
    public string? MotherContact { get; set; }
}

public class UpdateStudentRequest
{
    public string? FullName { get; set; }
    public bool? IsActive { get; set; }
    public string? Code { get; set; }
    public string? NID { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? FatherName { get; set; }
    public string? FatherContact { get; set; }
    public string? MotherName { get; set; }
    public string? MotherContact { get; set; }
}