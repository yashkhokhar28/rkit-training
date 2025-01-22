using Newtonsoft.Json;
using StockPortfolioAPI.Models.ENUM;
using System;
using System.ComponentModel.DataAnnotations;

namespace StockPortfolioAPI.Models.DTO
{
    /// <summary>
    /// model for register
    /// </summary>
    public class DTOUSR01
    {
        /// <summary>
        /// Unique identifier for each user.
        /// </summary>
        [JsonProperty("R01101")]
        [Range(1, int.MaxValue, ErrorMessage = "ID must be Positive.")]
        public int R01F01 { get; set; }

        /// <summary>
        /// The name chosen by the user for login.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        [JsonProperty("R01102")]
        public string R01F02 { get; set; }

        /// <summary>
        /// The unique email address of the user.
        /// </summary>
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [JsonProperty("R01103")]
        public string R01F03 { get; set; }

        /// <summary>
        /// The hashed password for the user.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}|<>?;:,.]).+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one number, and one special character.")]
        [JsonProperty("R01104")]
        public string R01F04 { get; set; }
    }
}