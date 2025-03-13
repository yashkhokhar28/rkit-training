import { AuthAPIURL } from "./apiConfig.js";
import { DisplayMessage, setAuthState } from "./utils.js";
import { loadDashboard } from "./tasks.js";

const authStore = {
  /**
   * Function: login
   * Description: Handles user login by sending credentials to the authentication API
   * and managing the response to set auth state and update UI.
   * Called in: Login form submission.
   * Parameters:
   *   - {username: string}: The user's username.
   *   - {password: string}: The user's password.
   * Returns: Promise resolving with login result or rejecting with error.
   */
  login: (username, password) => {
    return $.ajax({
      url: `${AuthAPIURL}/login`,
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify({ R01102: username, R01103: password }),
    })
      .then((result) => {
        if (result.isError) {
          throw new Error(result.message || "Login failed due to server error");
        }
        if (
          !result.data ||
          !Array.isArray(result.data) ||
          result.data.length === 0
        ) {
          throw new Error("Invalid login response format or missing data");
        }
        const loginData = result.data[0];
        if (!loginData.token?.token || !loginData.userID || !loginData.role) {
          throw new Error("Incomplete login data received");
        }
        setAuthState({
          token: loginData.token.token,
          userId: loginData.userID,
          userRole: loginData.role,
        });
        DisplayMessage(result.message || "Login successful", "success", 2000);
        $("#loginPanel").hide();
        $("#dashboard").show();
        loadDashboard();
      })
      .catch((xhr) => {
        console.error("Login error:", xhr);
        const errorMessage =
          xhr.responseJSON?.Message || xhr.statusText || "Unknown error";
        DisplayMessage(`Login failed: ${errorMessage}`, "error", 3000);
        throw new Error(errorMessage);
      });
  },
};

export { authStore };
