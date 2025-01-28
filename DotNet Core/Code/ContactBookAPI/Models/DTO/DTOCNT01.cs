using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactBookAPI.Models.DTO
{
    public class DTOCNT01
    {
        /// <summary>
        /// Contact ID
        /// </summary>
        [Required(ErrorMessage = "ID Is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "ID Must Be Positive")]
        [JsonPropertyName("T01101")]  // Custom name for Swagger
        [JsonProperty("T01101")]  // If you're using Newtonsoft.Json
        public int T01F01 { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [Required(ErrorMessage = "First Name Is Required")]
        [JsonPropertyName("T01102")]  // Custom name for Swagger
        [JsonProperty("T01102")]  // If you're using Newtonsoft.Json
        public string T01F02 { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [Required(ErrorMessage = "Last Name Is Required")]
        [JsonPropertyName("T01103")]  // Custom name for Swagger
        [JsonProperty("T01103")]  // If you're using Newtonsoft.Json
        public string T01F03 { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress(ErrorMessage = "Enter Correct Email Format")]
        [JsonPropertyName("T01104")]  // Custom name for Swagger
        [JsonProperty("T01104")]  // If you're using Newtonsoft.Json
        public string T01F04 { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        [Required(ErrorMessage = "Phone Number Is Required")]
        [Phone(ErrorMessage = "Enter Valid Phone Number")]
        [JsonPropertyName("T01105")]  // Custom name for Swagger
        [JsonProperty("T01105")]  // If you're using Newtonsoft.Json
        public string T01F05 { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [Required(ErrorMessage = "Address Is Required")]
        [JsonPropertyName("T01106")]  // Custom name for Swagger
        [JsonProperty("T01106")]  // If you're using Newtonsoft.Json
        public string T01F06 { get; set; }
    }
}
