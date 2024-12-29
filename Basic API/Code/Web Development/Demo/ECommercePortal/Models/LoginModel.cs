using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommercePortal.Models
{
    /// <summary>
    /// Represents the model for user login data.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the username provided by the user during login.
        /// </summary>
        /// <value>The username as a string.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password provided by the user during login.
        /// </summary>
        /// <value>The password as a string.</value>
        public string Password { get; set; }
    }
}