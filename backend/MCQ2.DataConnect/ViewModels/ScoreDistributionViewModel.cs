namespace MCQ3.DataConnect.ViewModels;

public record ScoreDistributionViewModel(
    List<int> Ranges,
    List<int> Counts
);