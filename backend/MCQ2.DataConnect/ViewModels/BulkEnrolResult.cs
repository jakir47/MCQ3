namespace MCQ3.DataConnect.ViewModels;

public record BulkEnrolResult(
    List<string> Enrolled,
    List<string> Skipped,
    List<string> Unmatched
);