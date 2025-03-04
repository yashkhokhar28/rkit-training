import { AuthAPIURL } from "./apiConfig.js";
import { DisplayMessage } from "./utils.js";
import { loadDashboard } from "./tasks.js";
import { setAuthState, getAuthState } from "./utils.js";

const authStore = {
  login: (username, password) => {
    return $.ajax({
      url: `${AuthAPIURL}/login`,
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify({ R01102: username, R01103: password }),
    }).then(
      (result) => {
        if (result.isError) {
          throw new Error(result.message);
        }
        const loginData = result.data[0];
        setAuthState({
          token: loginData.token.token,
          userId: loginData.userID,
          userRole: loginData.role,
        });
        DisplayMessage(result.message, "success", 2000);
        $("#loginPanel").hide();
        $("#dashboard").show();
        loadDashboard();
      },
      (xhr) => {
        DisplayMessage(
          "Login failed: " + (xhr.responseJSON?.Message || "Unknown error"),
          "error",
          3000
        );
        throw "Login failed";
      }
    );
  },
};

export { authStore };
