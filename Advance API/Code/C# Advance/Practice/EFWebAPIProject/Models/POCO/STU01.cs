using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFWebAPIProject.Models.POCO
{
    /// <summary>
    /// The STU01 class represents a student with basic properties such as ID, name, and age.
    /// </summary>
    public class STU01
    {
        /// <summary>
        /// Gets or sets the student ID (U01F01).
        /// This field represents the unique identifier for the student.
        /// </summary>
        public int U01F01 { get; set; }

        /// <summary>
        /// Gets or sets the student name (U01F02).
        /// This field represents the name of the student.
        /// </summary>
        public string U01F02 { get; set; }

        /// <summary>
        /// Gets or sets the student age (U01F03).
        /// This field represents the age of the student.
        /// </summary>
        public int U01F03 { get; set; }
    }
}