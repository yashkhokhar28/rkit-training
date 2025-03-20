using System;

namespace CRUDDemo.Models.POCO
{
    /// <summary>
    /// Represents the Employee entity with necessary properties.
    /// </summary>
    public class EMP01
    {
        /// <summary>
        /// Gets or sets the Employee ID.
        /// </summary>
        public int P01F01 { get; set; }

        /// <summary>
        /// Gets or sets the Employee Name.
        /// </summary>
        public string P01F02 { get; set; }

        /// <summary>
        /// Gets or sets the Employee Age.
        /// </summary>
        public int P01F03 { get; set; }

        /// <summary>
        /// Gets or sets the Employee Status.
        /// Status is either "Minor" or "Adult", calculated based on the Employee Age.
        /// </summary>
        public string P01F04 { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the record was created.
        /// </summary>
        public DateTime P01F05 { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the record was last modified.
        /// </summary>
        public DateTime P01F06 { get; set; }
    }
}