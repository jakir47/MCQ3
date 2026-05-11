using MCQ3.DataConnect.ViewModels;

namespace MCQ3.DataConnect.Responses;

public class StartExamResult
{
    public bool Success { get; set; }
    public AttemptViewModel? Attempt { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ErrorCode { get; set; }

    public static StartExamResult Ok(AttemptViewModel attempt) => new()
    {
        Success = true,
        Attempt = attempt
    };

    public static StartExamResult Fail(string errorCode, string message) => new()
    {
        Success = false,
        ErrorCode = errorCode,
        ErrorMessage = message
    };
}