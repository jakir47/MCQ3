using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExamSummaryViewModel = MCQ3.DataConnect.Services.ExamSummaryResponse;
using QuestionAnalyticsViewModel = MCQ3.DataConnect.Services.QuestionAnalyticsResponse;
using ScoreDistributionViewModel = MCQ3.DataConnect.Services.ScoreDistributionResponse;
using StudentProgressViewModel = MCQ3.DataConnect.Services.StudentProgressResponse;
using TrendViewModel = MCQ3.DataConnect.Services.TrendResponse;
using EnrolmentAnalyticsViewModel = MCQ3.DataConnect.Services.EnrolmentAnalyticsResponse;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/analytics")]
[Authorize(Roles = "Teacher,Admin")]
public class AnalyticsController([FromServices] AnalyticsService analyticsService) : ControllerBase
{
    [HttpGet("exams/{examId}/summary")]
    public async Task<IActionResult> GetExamSummary(Guid examId)
    {
        var summary = await analyticsService.GetExamSummaryAsync(examId);
        return Ok(new ApiResponse<ExamSummaryViewModel>(true, summary));
    }

    [HttpGet("exams/{examId}/questions")]
    public async Task<IActionResult> GetQuestionAnalytics(Guid examId)
    {
        var analytics = await analyticsService.GetQuestionAnalyticsAsync(examId);
        return Ok(new ApiResponse<IEnumerable<QuestionAnalyticsViewModel>>(true, analytics));
    }

    [HttpGet("exams/{examId}/distribution")]
    public async Task<IActionResult> GetScoreDistribution(Guid examId)
    {
        var distribution = await analyticsService.GetScoreDistributionAsync(examId);
        return Ok(new ApiResponse<ScoreDistributionViewModel>(true, distribution));
    }

    [HttpGet("chapters/{chapterId}/students")]
    public async Task<IActionResult> GetChapterStudents(Guid chapterId)
    {
        var students = await analyticsService.GetChapterStudentsAsync(chapterId);
        return Ok(new ApiResponse<IEnumerable<StudentProgressViewModel>>(true, students));
    }

    [HttpGet("chapters/{chapterId}/trends")]
    public async Task<IActionResult> GetChapterTrends(Guid chapterId)
    {
        var trends = await analyticsService.GetChapterTrendsAsync(chapterId);
        return Ok(new ApiResponse<IEnumerable<TrendViewModel>>(true, trends));
    }

    [HttpGet("chapters/{chapterId}/enrolments")]
    public async Task<IActionResult> GetEnrolmentAnalytics(Guid chapterId)
    {
        var analytics = await analyticsService.GetEnrolmentAnalyticsAsync(chapterId);
        return Ok(new ApiResponse<EnrolmentAnalyticsViewModel>(true, analytics));
    }
}