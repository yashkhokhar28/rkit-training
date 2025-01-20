using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a positive number.")]
        public int P01F01 { get; set; }

        /// <summary>
        /// Represents the Employee Name.
        /// </summary>
        [JsonProperty("P01102")]
        [Required(ErrorMessage = "Employee Name is required.")]
        [StringLength(100, ErrorMessage = "Employee Name cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Employee Name must only contain letters and spaces.")]
        public string P01F02 { get; set; }

        /// <summary>
        /// Represents the Employee Age.
        /// </summary>
        [JsonProperty("P01103")]
        [Range(1, 100, ErrorMessage = "Employee Age must be between 18 and 100.")]
        public int P01F03 { get; set; }
    }
}