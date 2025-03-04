import { loadDashboard } from "./tasks.js";

// Centralized state for auth
let authState = {
  token: null,
  userRole: null,
  userId: null,
};

export const setAuthState = ({ token, userRole, userId }) => {
  authState.token = token;
  authState.userRole = userRole;
  authState.userId = userId;
  // Save to sessionStorage
  sessionStorage.setItem("jwtToken", token || "");
  sessionStorage.setItem("userRole", userRole || "");
  sessionStorage.setItem("userId", userId || "");
};

export const getAuthState = () => authState;

export const getAuthHeader = () => {
  return authState.token ? { Authorization: `Bearer ${authState.token}` } : {};
};

export const DisplayMessage = (message, type, displayTime) => {
  DevExpress.ui.notify({
    message: message,
    type: type,
    displayTime: displayTime,
  });
};

let isDarkTheme = true;

// Theme Toggling Function
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

// Load saved theme preference from sessionStorage
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

// Initialize app state based on stored token in sessionStorage
export const initializeApp = () => {
  const token = sessionStorage.getItem("jwtToken");
  const userRole = sessionStorage.getItem("userRole");
  const userId = sessionStorage.getItem("userId");

  if (token) {
    setAuthState({ token, userRole, userId });
    $("#loginPanel").hide();
    $("#dashboard").show();
    loadDashboard(); // Ensure dashboard is loaded if authenticated
  } else {
    $("#loginPanel").show();
    $("#dashboard").hide();
  }
};

// Call initializeApp when the module loads
$(() => {
  initializeApp();
});
