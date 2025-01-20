using System;

namespace StockPortfolioAPI.Models.POCO
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// Unique identifier for each user.
        /// </summary>
        public int R01F01 { get; set; }

        /// <summary>
        /// The name chosen by the user for login.
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// The unique email address of the user.
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// The hashed password for the user.
        /// </summary>
        public string R01F04 { get; set; }

        /// <summary>
        /// Role for User
        /// </summary>
        public string R01F05 { get; set; }

        /// <summary>
        /// Timestamp when the user was created.
        /// </summary>
        public DateTime R01F06 { get; set; }

        /// <summary>
        /// Timestamp when the user details were last updated.
        /// </summary>
        public DateTime R01F07 { get; set; }
    }
}