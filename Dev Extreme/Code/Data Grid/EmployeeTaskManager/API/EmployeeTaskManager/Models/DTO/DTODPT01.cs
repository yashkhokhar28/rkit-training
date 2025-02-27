using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EmployeeTaskManager.Models.DTO
{
    public class DTODPT01
    {
        /// <summary>
        /// department_id
        /// </summary>
        [JsonProperty("T01101")]
        [JsonPropertyName("T01101")]
        public int T01F01 { get; set; }

        /// <summary>
        /// name
        /// </summary>
        [JsonProperty("T01102")]
        [JsonPropertyName("T01102")]
        public string T01F02 { get; set; }

        /// <summary>
        /// manager_id
        /// </summary>
        [JsonProperty("T01103")]
        [JsonPropertyName("T01103")]
        public int T01F03 { get; set; }
    }
}
