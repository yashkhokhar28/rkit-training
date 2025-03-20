using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EmployeeTaskManager.Models.DTO
{
    /// <summary>
    /// Represents a department data transfer object in the EmployeeTaskManager system.
    /// </summary>
    public class DTODPT01
    {
        /// <summary>
        /// Department ID (Primary Key)
        /// </summary>
        [JsonProperty("T01101")]
        [JsonPropertyName("T01101")]
        public int T01F01 { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        [JsonProperty("T01102")]
        [JsonPropertyName("T01102")]
        public string T01F02 { get; set; } = null!; 

        /// <summary>
        /// Manager ID (Foreign Key to USR01.R01F01)
        /// </summary>
        [JsonProperty("T01103")]
        [JsonPropertyName("T01103")]
        public int T01F03 { get; set; } 
    }
}