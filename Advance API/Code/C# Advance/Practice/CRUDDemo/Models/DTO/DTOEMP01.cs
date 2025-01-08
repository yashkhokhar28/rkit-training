using Newtonsoft.Json;

namespace CRUDDemo.Models.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for Employee.
    /// </summary>
    public class DTOEMP01
    {
        /// <summary>
        /// Represents the Employee ID.
        /// </summary>
        [JsonProperty("P01101")]
        public int P01F01 { get; set; }

        /// <summary>
        /// Represents the Employee Name.
        /// </summary>
        [JsonProperty("P01102")]
        public string P01F02 { get; set; }

        /// <summary>
        /// Represents the Employee Age.
        /// </summary>
        [JsonProperty("P01103")]
        public int P01F03 { get; set; }
    }
}