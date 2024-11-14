namespace CB.Api.Common;

public class PaginatedList<T> {

    [JsonProperty("items")]
    public List<T> Items { get; set; } = [];

    [JsonProperty("count")]
    public int Count { get; set; }
}
