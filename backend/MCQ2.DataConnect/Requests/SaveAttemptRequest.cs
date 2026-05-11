namespace MCQ3.DataConnect.Requests;

public class SaveAttemptRequest
{
    public Dictionary<Guid, Guid> Answers { get; set; } = new();
    public int RemainingSecs { get; set; }
    public List<Guid>? QuestionOrder { get; set; }
    public Dictionary<Guid, List<Guid>>? OptionOrders { get; set; }
}