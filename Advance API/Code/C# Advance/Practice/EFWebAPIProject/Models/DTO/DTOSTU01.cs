using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public int U01F01 { get; set; }

        /// <summary>
        /// Gets or sets the student name (U01F02).
        /// This field represents the name of the student.
        /// It will be serialized as "U01102" in the JSON format.
        /// </summary>
        [JsonProperty("U01102")]
        public string U01F02 { get; set; }

        /// <summary>
        /// Gets or sets the student age (U01F03).
        /// This field represents the age of the student.
        /// It will be serialized as "U01103" in the JSON format.
        /// </summary>
        [JsonProperty("U01103")]
        public int U01F03 { get; set; }
    }
}