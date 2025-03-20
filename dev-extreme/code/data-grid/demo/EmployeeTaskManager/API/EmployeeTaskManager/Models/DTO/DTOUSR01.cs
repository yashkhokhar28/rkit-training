using System.Text.Json.Serialization;
using Newtonsoft.Json;
using EmployeeTaskManager.Models.ENUM;

namespace EmployeeTaskManager.Models.DTO
{
    /// <summary>
    /// Represents a user/employee data transfer object in the EmployeeTaskManager system.
    /// </summary>
    public class DTOUSR01
    {
        /// <summary>
        /// User/Employee ID (Primary Key)
        /// </summary>
        [JsonProperty("R01101")]
        [JsonPropertyName("R01101")]
        public int R01F01 { get; set; }

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

        /// <summary>
        /// Role (Admin, Manager, Employee)
        /// </summary>
        [JsonProperty("R01104")]
        [JsonPropertyName("R01104")]
        public EnmRole R01F04 { get; set; } = EnmRole.Employee;

        /// <summary>
        /// First Name
        /// </summary>
        [JsonProperty("R01105")]
        [JsonPropertyName("R01105")]
        public string R01F05 { get; set; } = null!; 

        /// <summary>
        /// Last Name
        /// </summary>
        [JsonProperty("R01106")]
        [JsonPropertyName("R01106")]
        public string R01F06 { get; set; } = null!;

        /// <summary>
        /// Email (Unique)
        /// </summary>
        [JsonProperty("R01107")]
        [JsonPropertyName("R01107")]
        public string R01F07 { get; set; } = null!;

        /// <summary>
        /// Department ID (Foreign Key to DPT01.T01F01)
        /// </summary>
        [JsonProperty("R01108")]
        [JsonPropertyName("R01108")]
        public int R01F08 { get; set; }

        /// <summary>
        /// Hire Date
        /// </summary>
        [JsonProperty("R01109")]
        [JsonPropertyName("R01109")]
        public DateTime R01F09 { get; set; }
    }
}