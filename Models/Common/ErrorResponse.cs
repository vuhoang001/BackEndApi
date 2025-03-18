using System.Text.Json.Serialization;

namespace BackEndApi.Models.Common;

public class ErrorResponse
{
    [JsonPropertyName("code")] public string Code { get; set; } = "<unknown>";
    [JsonPropertyName("httpStatusCode")] public int HttpStatusCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("__messageForDev__")]
    public object? MessageDev { get; set; }
}