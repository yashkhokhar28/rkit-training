import { GetCookies } from "./GetCookies.js";

$(document).ready(() => {
  $("#sign-in").submit((e) => {
    e.preventDefault();
    let userNameValue = document.getElementById("username").value;
    let passwordValue = document.getElementById("password").value;

    let cookieData = GetCookies.GetCookie({
      UserNameKey: "username",
      PasswordKey: "password",
    });

    let { username, password } = cookieData || {};

    if (
      userNameValue.trim() === username &&
      passwordValue.trim() === password
    ) {
      window.location.href = "question.html";
    } else {
      alert("Invalid username or password");
    }
  });
});
