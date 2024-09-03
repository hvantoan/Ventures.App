using Newtonsoft.Json;

namespace CB.Domain.Common;

public class Result {

    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
    public string? Message { get; set; }

    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public object? Data { get; set; }

    public static Result Ok(object? data = null) {
        return new() { Success = true, Data = data };
    }

    public static Result Fail(string? message = null) {
        return new() { Message = message };
    }

    public override string ToString() {
        return JsonConvert.SerializeObject(this);
    }
}



public class FileResult {
    public string FileName { get; set; } = string.Empty;
    public byte[] ByteArray { get; set; } = [];
}
