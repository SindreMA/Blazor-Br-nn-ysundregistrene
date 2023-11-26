using System.Text.Json.Serialization;

namespace BrregFrontend.DTOs
{
    public class ListResponse<T>
    {
        [JsonPropertyName("items")]
        public List<T> Items { get; set; }
        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }
        [JsonPropertyName("start")]
        public int Start { get; set; }
        [JsonPropertyName("end")]
        public int End { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
