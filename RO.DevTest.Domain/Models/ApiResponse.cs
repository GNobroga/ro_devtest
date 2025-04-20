using System.Net;
using System.Text.Json.Serialization;

namespace RO.DevTest.Domain.Models;

public class ApiResponse<T>(int statusCode, T? data, string[]? messages) {
    public int StatusCode { get; set; } = statusCode;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Messages { get; set; } = messages;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; } = data;

    public ApiResponse(int statusCode, T data): this(statusCode, data, default) {}
    public ApiResponse(int statusCode, params string[] messages): this(statusCode, default, messages) {}

    public static ApiResponse<T> FromSuccess(T data, int statusCode = (int) HttpStatusCode.OK) => new(statusCode, data);
    public static ApiResponse<T> FromFailure(int statusCode = (int) HttpStatusCode.OK, params string[] messages) => new(statusCode, messages);
}