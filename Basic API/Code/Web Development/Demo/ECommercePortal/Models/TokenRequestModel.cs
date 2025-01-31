namespace ECommercePortal.Models
{
    /// <summary>
    /// Model representing the request containing a JWT token.
    /// </summary>
    public class TokenRequestModel
    {
        /// <summary>
        /// Gets or sets the JWT token that is sent in the request.
        /// </summary>
        /// <value>
        /// The JWT token as a string.
        /// </value>
        public string Token { get; set; }
    }
}