﻿namespace ECommercePortal.Models
{
    /// <summary>
    /// Represents the model for user login data.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the username provided by the user during login.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password provided by the user during login.
        /// </summary>
        public string Password { get; set; }
    }
}