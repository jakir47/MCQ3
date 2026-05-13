namespace MCQ3.DataConnect.Responses;

public record StatsResponse(
    int TotalUsers,
    int TotalSubjects,
    int TotalExams,
    int TotalAttempts
);