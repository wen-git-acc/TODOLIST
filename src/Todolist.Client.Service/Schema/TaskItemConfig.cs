using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TodoList.Service.Schema
{
    public class TaskItemConfig
    {
        [JsonPropertyName("uniqueId")]
        public string UniqueId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("dueDate")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("status")]
        public TaskStatus? Status { get; set; }
    }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskStatus
    {
        [EnumMember(Value = "notstarted")]
        notstarted,

        [EnumMember(Value = "inprogress")]
        inprogress,

        [EnumMember(Value = "completed")]
        completed
    }
}
