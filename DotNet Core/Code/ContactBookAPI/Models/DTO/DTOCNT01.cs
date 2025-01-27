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
        public int T01F01 { get; set; }

        /// <summary>
        /// First Name
        /// </summary>

        [Required(ErrorMessage = "First Name Is Required")]
        public string T01F02 { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        
        [Required(ErrorMessage = "Last Name Is Required")]
        public string T01F03 { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        
        [JsonProperty("T01104")]
        [JsonPropertyName("T01104")]
        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress(ErrorMessage = "Enter Correct Email Format")]
        public string T01F04 { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        
        [Required(ErrorMessage = "Phone Nuber Is Required")]
        [Phone(ErrorMessage = "Enter Valid Phone Number")]
        public string T01F05 { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        
        [Required(ErrorMessage = "Address Is Required")]
        public string T01F06 { get; set; }
    }
}
