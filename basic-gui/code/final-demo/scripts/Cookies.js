import { Auth } from "./Auth.js";

export class Cookies {
  /**
   * Function: GetCookie
   * Description: Retrieves the values of specific cookies (username and password).
   * Loops through all available cookies and extracts the values for the given keys.
   * Called in: CheckCookie() function in this JavaScript file.
   * Parameters:
   *   - UserNameKey (string): The key for the username cookie.
   *   - PasswordKey (string): The key for the password cookie.
   * Returns: An object containing the username and password, or null if not found.
   * Throws: Error if either UserNameKey or PasswordKey is not provided.
   */
  static GetCookie({ UserNameKey, PasswordKey }) {
    if (!UserNameKey || !PasswordKey) {
      throw new Error("Both UserNameKey and PasswordKey are required.");
    }

    // Retrieve all cookies as an array of strings.
    let cookieAll = document.cookie.split(";");
    let username = null;
    let password = null;

    cookieAll.forEach((cookie) => {
      let c = cookie.trim();
      if (c.startsWith(`${UserNameKey}=`)) {
        username = c.substring(`${UserNameKey}=`.length);
      } else if (c.startsWith(`${PasswordKey}=`)) {
        password = c.substring(`${PasswordKey}=`.length);
      }
    });

    return { username, password };
  }

  /**
   * Function: CheckCookie
   * Description: Checks whether cookies for username and password exist.
   * Redirects to the sign-in page if both exist, otherwise to the sign-up page.
   * Called in: Likely invoked on page load to determine redirection.
   * Parameters: None.
   * Returns: None. Performs a redirection based on cookie presence.
   */
  static CheckCookie = () => {
    let { username, password } = Cookies.GetCookie({
      UserNameKey: "username",
      PasswordKey: "password",
    });

    if (username && password) {
      window.location.href = "sign-in.html";
    } else {
      window.location.href = "sign-up.html";
    }
  };
}
