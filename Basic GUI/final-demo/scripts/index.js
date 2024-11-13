import { GetCookies } from "./GetCookies.js";

window.CheckCookie = () => {
  let { userName, password } = GetCookies.GetCookie({
    UserNameKey: "username",
    PasswordKey: "password",
  });

  if (userName && password) {
    window.location.href = "sign-in.html";
  } else {
    window.location.href = "sign-up.html";
  }
};

let SaveFormData = () => {
  let formData = {
    name: document.getElementById("name").value,
    email: document.getElementById("email").value,
    phone: document.getElementById("phone").value,
    userType: document.querySelector('input[name="user-type"]:checked').value,
    workType: document.getElementById("floatingSelect").value,
    message: document.getElementById("message").value,
  };
  localStorage.setItem("formData", JSON.stringify(formData));
  sessionStorage.setItem("formData", JSON.stringify(formData));
};

let ClearLocalStorage = () => {
  localStorage.clear();
  alert("Local Storage Cleared");
  window.location.reload();
};

let DeleteCookie = () => {
  let cookieAll = document.cookie.split(";"); // Split cookies by ';'
  for (let i = 0; i < cookieAll.length; i++) {
    let c = cookieAll[i].trim(); // Remove leading spaces
    document.cookie = c + "; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;"; // Delete the cookie
  }
  alert("All Cookies Deleted");
};
