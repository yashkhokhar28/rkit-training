using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace EFWebAPIProject.Models.DTO
{
    /// <summary>
    /// The DTOSTU01 class represents a data transfer object (DTO) for a student.
    /// This is used for transferring student data between layers or over a network.
    /// </summary>
    public class DTOSTU01
    {
        /// <summary>
        /// Gets or sets the student ID (U01F01).
        /// This field represents the unique identifier for the student.
        /// It will be serialized as "U01101" in the JSON format.
        /// </summary>
        [JsonProperty("U01101")]
        [Range(1, int.MaxValue, ErrorMessage = "Student ID must be a positive number.")]
        public int U01F01 { get; set; }

        /// <summary>
        /// Gets or sets the student name (U01F02).
        /// This field represents the name of the student.
        /// It will be serialized as "U01102" in the JSON format.
        /// </summary>
        [JsonProperty("U01102")]
        [Required(ErrorMessage = "Student name is required.")]
        [StringLength(100, ErrorMessage = "Student name cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Student name must only contain letters and spaces.")]
        public string U01F02 { get; set; }

        /// <summary>
        /// Gets or sets the student age (U01F03).
        /// This field represents the age of the student.
        /// It will be serialized as "U01103" in the JSON format.
        /// </summary>
        [JsonProperty("U01103")]
        [Range(1, 150, ErrorMessage = "Age must be between 1 and 150.")]
        public int U01F03 { get; set; }
    }
}