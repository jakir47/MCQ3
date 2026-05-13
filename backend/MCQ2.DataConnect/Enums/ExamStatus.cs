using System.Text.Json.Serialization;

namespace MCQ3.DataConnect.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExamStatus
{
    Draft,
    Published,
    Archived
}