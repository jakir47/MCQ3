using MCQ3.DataConnect.Enums;

namespace MCQ3.DataConnect.Requests;

public class BankSearchParams
{
    public string? q { get; set; }
    public string? tags { get; set; }
    public Difficulty? difficulty { get; set; }
    public int page { get; set; } = 1;
    public int pageSize { get; set; } = 20;
}