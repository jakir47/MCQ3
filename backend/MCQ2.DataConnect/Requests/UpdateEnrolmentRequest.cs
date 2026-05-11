namespace MCQ3.DataConnect.Requests;

public class UpdateEnrolmentRequest
{
    public DateTime? ExpiresAt { get; set; }
    public Guid? TransferToChapterId { get; set; }
}