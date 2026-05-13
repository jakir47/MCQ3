namespace MCQ3.DataConnect.Responses;

public record TeacherStatsResponse(
    int TotalSubjects,
    int TotalChapters,
    int TotalStudents,
    int ActiveExams,
    int TotalQuestions,
    int TotalAttempts
);