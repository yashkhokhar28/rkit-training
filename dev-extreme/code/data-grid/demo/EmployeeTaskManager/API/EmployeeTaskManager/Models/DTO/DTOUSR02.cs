using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EmployeeTaskManager.Models.DTO
{
    /// <summary>
    /// Represents a user data transfer object used for login in the EmployeeTaskManager system.
    /// </summary>
    public class DTOUSR02
    {
        /// <summary>
        /// Username (Unique)
        /// </summary>
        [JsonProperty("R01102")]
        [JsonPropertyName("R01102")]
        public string R01F02 { get; set; } = null!; 

        /// <summary>
        /// Password Hash
        /// </summary>
        [JsonProperty("R01103")]
        [JsonPropertyName("R01103")]
        public string R01F03 { get; set; } = null!; 
    }
}