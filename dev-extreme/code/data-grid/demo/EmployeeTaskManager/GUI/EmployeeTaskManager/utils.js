import { loadDashboard } from "./tasks.js";

let authState = {
  token: null,
  userRole: null,
  userId: null,
};

/**
 * Function: setAuthState
 * Description: Updates the authentication state and persists it to sessionStorage.
 * Called in: Login success or app initialization.
 * Parameters:
 *   - {{token: string, userRole: string, userId: string}}: Object containing auth details.
 * Returns: None.
 */
export const setAuthState = ({ token, userRole, userId }) => {
  authState.token = token;
  authState.userRole = userRole;
  authState.userId = userId;
  sessionStorage.setItem("jwtToken", token || "");
  sessionStorage.setItem("userRole", userRole || "");
  sessionStorage.setItem("userId", userId || "");
};

/**
 * Function: getAuthState
 * Description: Retrieves the current authentication state.
 * Called in: Various components needing auth information.
 * Parameters: None.
 * Returns: Object containing current auth state.
 */
export const getAuthState = () => authState;

/**
 * Function: getAuthHeader
 * Description: Generates an Authorization header with Bearer token if available.
 * Called in: API requests requiring authentication.
 * Parameters: None.
 * Returns: Object with Authorization header or empty object.
 */
export const getAuthHeader = () => {
  return authState.token ? { Authorization: `Bearer ${authState.token}` } : {};
};

/**
 * Function: DisplayMessage
 * Description: Shows a notification message to the user.
 * Called in: Various success or error scenarios.
 * Parameters:
 *   - {message: string}: The message to display.
 *   - {type: string}: The type of message (e.g., "success", "error").
 *   - {displayTime: number}: Duration in milliseconds to show the message.
 * Returns: None.
 */
export const DisplayMessage = (message, type, displayTime) => {
  DevExpress.ui.notify({
    message: message,
    type: type,
    displayTime: displayTime,
  });
};

let isDarkTheme = true;

/**
 * Function: toggleTheme
 * Description: Switches between dark and light themes and updates UI.
 * Called in: Theme toggle button click.
 * Parameters: None.
 * Returns: None.
 */
export const toggleTheme = () => {
  const stylesheet = $("#themeStylesheet");
  isDarkTheme = !isDarkTheme;
  const newTheme = isDarkTheme
    ? "../Content/dx.material.blue.dark.css"
    : "../Content/dx.material.blue.light.css";
  stylesheet.attr("href", newTheme);
  sessionStorage.setItem("theme", isDarkTheme ? "dark" : "light");
  DisplayMessage(
    `Switched to ${isDarkTheme ? "Dark" : "Light"} Theme`,
    "success",
    1000
  );
};

const savedTheme = sessionStorage.getItem("theme");
if (savedTheme) {
  isDarkTheme = savedTheme === "dark";
  $("#themeStylesheet").attr(
    "href",
    isDarkTheme
      ? "../Content/dx.material.blue.dark.css"
      : "../Content/dx.material.blue.light.css"
  );
}

/**
 * Function: initializeApp
 * Description: Initializes the application state based on stored auth data.
 * Called in: Document ready event.
 * Parameters: None.
 * Returns: None.
 */
export const initializeApp = () => {
  const token = sessionStorage.getItem("jwtToken");
  const userRole = sessionStorage.getItem("userRole");
  const userId = sessionStorage.getItem("userId");
  if (token) {
    setAuthState({ token, userRole, userId });
    $("#loginPanel").hide();
    $("#dashboard").show();
    loadDashboard();
  } else {
    $("#loginPanel").show();
    $("#dashboard").hide();
  }
};

$(() => {
  initializeApp();
});
