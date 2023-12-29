using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace CompanyStatistics.Domain.DTOs.File
{
    public class FileDto
    {
        [BsonId]
        [JsonProperty("file-name")]
        public string FileName { get; set; }
        public int Index { get; set; }
    }
}
