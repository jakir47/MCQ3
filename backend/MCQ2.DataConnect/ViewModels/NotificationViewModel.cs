namespace MCQ3.DataConnect.ViewModels;

public record NotificationViewModel(
    Guid Id,
    string Type,
    string Title,
    string Body,
    bool IsRead,
    DateTime CreatedAt
);