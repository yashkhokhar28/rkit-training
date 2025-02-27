using System.Text.Json.Serialization;
using EmployeeTaskManager.Models.ENUM;
using Newtonsoft.Json;

namespace EmployeeTaskManager.Models.DTO
{
    public class DTOTSK01
    {
        /// <summary>
        /// task_id
        /// </summary>
        [JsonProperty("K01101")]
        [JsonPropertyName("K01101")]
        public int K01F01 { get; set; }

        /// <summary>
        /// title
        /// </summary>
        [JsonProperty("K01102")]
        [JsonPropertyName("K01102")]
        public string K01F02 { get; set; }

        /// <summary>
        /// description
        /// </summary>
        [JsonProperty("K01103")]
        [JsonPropertyName("K01103")]
        public string K01F03 { get; set; }

        /// <summary>
        /// assigned_to
        /// </summary>
        [JsonProperty("K01104")]
        [JsonPropertyName("K01104")]
        public int K01F04 { get; set; }

        /// <summary>
        /// department_id
        /// </summary>
        [JsonProperty("K01105")]
        [JsonPropertyName("K01105")]
        public int K01F05 { get; set; }

        /// <summary>
        /// status
        /// </summary>
        [JsonProperty("K01106")]
        [JsonPropertyName("K01106")]
        public EnmStatus K01F06 { get; set; }

        /// <summary>
        /// priority
        /// </summary>
        [JsonProperty("K01107")]
        [JsonPropertyName("K01107")]
        public EnmPriority K01F07 { get; set; }

        /// <summary>
        /// due_date
        /// </summary>
        [JsonProperty("K01108")]
        [JsonPropertyName("K01108")]
        public DateTime K01F08 { get; set; }
    }
}
