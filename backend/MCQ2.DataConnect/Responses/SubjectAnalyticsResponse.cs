namespace MCQ3.DataConnect.Responses;

public record SubjectAnalyticsResponse(
    int TotalStudents,
    int TotalExams,
    decimal AvgScore,
    int PassCount,
    int TotalAttempts,
    decimal PassRate,
    List<ScoreRangeCount> ScoreDistribution,
    List<TopPerformer> TopPerformers
);

public record ScoreRangeCount(string Range, int Count);
public record TopPerformer(string StudentId, string StudentName, decimal AvgScore);