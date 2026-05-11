namespace MCQ3.DataConnect.Responses;

public record ApiError(string Code, string Message, object? Details = null);