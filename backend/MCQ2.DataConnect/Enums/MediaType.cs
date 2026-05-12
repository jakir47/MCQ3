using System.Text.Json.Serialization;

namespace MCQ3.DataConnect.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MediaType
{
    Text,
    Image,
    Audio,
    Video,
    VideoLink,
    Document
}