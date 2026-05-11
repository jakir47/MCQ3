namespace MCQ3.DataConnect.ViewModels;

public record EnrolmentAnalyticsViewModel(
    int TotalStudents,
    int ActiveStudents,
    int ExpiredStudents,
    int RemovedStudents,
    List<MonthlyEnrolment> MonthlyEnrolments
);