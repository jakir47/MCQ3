namespace MCQ3.DataConnect.Requests;

public class SubmitAttemptRequest
{
    public Dictionary<string, Guid> Answers { get; set; } = new();
    public int TimeSpentSecs { get; set; }
}