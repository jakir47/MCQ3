namespace MCQ3.DataConnect.Responses;

public record ApiResponse<T>(bool Success, T? Data, ApiError? Error = null);