using System;
using System.ComponentModel.DataAnnotations;

namespace StockPortfolioAPI.Models.DTO
{
    public class DTOUSR02
    {
        /// <summary>
        /// The unique email address of the user.
        /// </summary>
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string R01F03 { get; set; }

        /// <summary>
        /// The hashed password for the user.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        public string R01F04 { get; set; }
    }
}
