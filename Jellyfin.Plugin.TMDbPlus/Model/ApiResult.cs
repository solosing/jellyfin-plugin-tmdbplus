using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.TMDbPlus.Model;

public class ApiResult
{
    [JsonPropertyName("code")]
    public int Code { get; set; }
    [JsonPropertyName("msg")]
    public string Msg { get; set; } = string.Empty;

    public ApiResult(int code, string msg = "")
    {
        this.Code = code;
        this.Msg = msg;
    }
}