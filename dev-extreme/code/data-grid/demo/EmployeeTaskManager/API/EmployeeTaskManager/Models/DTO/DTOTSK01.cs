using System.Text.Json.Serialization;
using Newtonsoft.Json;
using EmployeeTaskManager.Models.ENUM;

namespace EmployeeTaskManager.Models.DTO
{
    /// <summary>
    /// Represents a task data transfer object in the EmployeeTaskManager system.
    /// </summary>
    public class DTOTSK01
    {
        /// <summary>
        /// Task ID (Primary Key)
        /// </summary>
        [JsonProperty("K01101")]
        [JsonPropertyName("K01101")]
        public int K01F01 { get; set; }

        /// <summary>
        /// Task Title
        /// </summary>
        [JsonProperty("K01102")]
        [JsonPropertyName("K01102")]
        public string K01F02 { get; set; } = null!; 

        /// <summary>
        /// Task Description
        /// </summary>
        [JsonProperty("K01103")]
        [JsonPropertyName("K01103")]
        public string? K01F03 { get; set; } 

        /// <summary>
        /// Assigned To (Foreign Key to USR01.R01F01)
        /// </summary>
        [JsonProperty("K01104")]
        [JsonPropertyName("K01104")]
        public int K01F04 { get; set; } 

        /// <summary>
        /// Department ID (Foreign Key to DPT01.T01F01)
        /// </summary>
        [JsonProperty("K01105")]
        [JsonPropertyName("K01105")]
        public int K01F05 { get; set; } 

        /// <summary>
        /// Task Status
        /// </summary>
        [JsonProperty("K01106")]
        [JsonPropertyName("K01106")]
        public EnmStatus K01F06 { get; set; } = EnmStatus.Pending; 

        /// <summary>
        /// Task Priority
        /// </summary>
        [JsonProperty("K01107")]
        [JsonPropertyName("K01107")]
        public EnmPriority K01F07 { get; set; } = EnmPriority.Medium; 

        /// <summary>
        /// Due Date
        /// </summary>
        [JsonProperty("K01108")]
        [JsonPropertyName("K01108")]
        public DateTime? K01F08 { get; set; } 
    }
}