/**
 * Class: Auth
 * Description: Manages user authentication functionalities, including sign-up,
 * sign-in, and authentication checks. Uses browser cookies for storage.
 */

import { Cookies } from "./Cookies.js";

export class Auth {
  /**
   * Function: SignUp
   * Description: Saves user credentials (username and password) in cookies
   * with a 1-day expiry.
   * Called in: Sign-up page (via form submission).
   * Parameters:
   *   - {UserNameValue: string, PasswordValue: string}: Object containing the user's username and password.
   * Returns: None.
   */
  static SignUp({ UserNameValue, PasswordValue }) {
    let d = new Date();
    d.setTime(d.getTime() + 24 * 60 * 60 * 1000); // Adds 1 day in milliseconds
    let expires = "expires=" + d.toUTCString();
    document.cookie = "username=" + UserNameValue + ";" + expires + ";path=/";
    document.cookie = "password=" + PasswordValue + ";" + expires + ";path=/";
  }

  /**
   * Function: SignIn
   * Description: Validates user credentials against the values stored in cookies.
   * If valid, redirects the user to the quiz page. Otherwise, shows an alert.
   * Called in: Sign-in page (via form submission).
   * Parameters:
   *   - {UserNameValue: string, PasswordValue: string}: Object containing the user's entered username and password.
   * Returns: None.
   */
  static SignIn({ UserNameValue, PasswordValue }) {
    let cookieData = Cookies.GetCookie({
      UserNameKey: "username",
      PasswordKey: "password",
    });

    let { username, password } = cookieData || {};

    if (
      UserNameValue.trim() === username &&
      PasswordValue.trim() === password
    ) {
      window.location.href = "question.html";
    } else {
      alert("Invalid username or password");
    }
  }

  /**
   * Function: CheckAuth
   * Description: Checks if valid user credentials (username and password)
   * are stored in cookies.
   * Called in: Multiple pages for authentication checks.
   * Parameters: None.
   * Returns:
   *   - {boolean}: True if both username and password exist in cookies; false otherwise.
   */
  static CheckAuth() {
    let { username, password } = Cookies.GetCookie({
      UserNameKey: "username",
      PasswordKey: "password",
    });

    return !!(username && password); // Returns true if both values are present
  }
}
