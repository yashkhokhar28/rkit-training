using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EmployeeTaskManager.Models.DTO
{
    public class DTOEMP01
    {
        /// <summary>
        /// employee_id
        /// </summary>
        [JsonProperty("P01101")]
        [JsonPropertyName("P01101")]
        public int P01F01 { get; set; }

        /// <summary>
        /// first_name
        /// </summary>
        [JsonProperty("P01102")]
        [JsonPropertyName("P01102")]
        public string P01F02 { get; set; }

        /// <summary>
        /// last_name
        /// </summary>
        [JsonProperty("P01103")]
        [JsonPropertyName("P01103")]
        public string P01F03 { get; set; }

        /// <summary>
        /// email
        /// </summary>
        [JsonProperty("P01104")]
        [JsonPropertyName("P01104")]
        public string P01F04 { get; set; }

        /// <summary>
        /// role
        /// </summary>
        [JsonProperty("P01105")]
        [JsonPropertyName("P01105")]
        public string P01F05 { get; set; }

        /// <summary>
        /// department_id
        /// </summary>
        [JsonProperty("P01106")]
        [JsonPropertyName("P01106")]
        public int P01F06 { get; set; }

        /// <summary>
        /// hire_date
        /// </summary>
        [JsonProperty("P01107")]
        [JsonPropertyName("P01107")]
        public DateTime P01F07 { get; set; }
    }
}
