import { Cookies } from "./Cookies.js";

export class Auth {
  static SignUp({ UserNameValue, PasswordValue }) {
    let d = new Date();
    d.setTime(d.getTime() + 24 * 60 * 60 * 1000); // Adds 1 day in milliseconds
    let expires = "expires=" + d.toUTCString();
    document.cookie = "username=" + UserNameValue + ";" + expires + ";path=/";
    document.cookie = "password=" + PasswordValue + ";" + expires + ";path=/";
  }

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

  static CheckAuth() {
    let { username, password } = Cookies.GetCookie({
      UserNameKey: "username",
      PasswordKey: "password",
    });

    return !!(username && password); // Returns true if both values are present
  }
}
