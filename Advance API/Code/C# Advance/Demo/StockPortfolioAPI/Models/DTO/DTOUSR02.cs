using System.ComponentModel.DataAnnotations;

namespace StockPortfolioAPI.Models.DTO
{
    /// <summary>
    /// model for login
    /// </summary>
    public class DTOUSR02
    {
        /// <summary>
        /// The unique email address of the user.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        public string R01F02 { get; set; }

        /// <summary>
        /// The hashed password for the user.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        public string R01F04 { get; set; }
    }
}
