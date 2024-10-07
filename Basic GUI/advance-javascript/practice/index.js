document.getElementById("before").innerHTML = localStorage.getItem("name");
const setLocalStorage = (event) => {
  event.preventDefault();
  var value = document.getElementById("name").value;
  localStorage.setItem("name", value);
  if (localStorage.getItem("name")) {
    document.getElementById("after").innerHTML = localStorage.getItem("name");
  }
};
const clearLocalStorage = () => {
  localStorage.clear();
  alert("Local Storage Cleared");
  window.location.reload();
};

// Set a cookie with a 1-day expiration
const setCookie = () => {
  var name = prompt("Please enter your name:");
  if (name == null || name == "") {
    alert("Name must be filled out");
    return false;
  }
  const d = new Date();
  d.setTime(d.getTime() + 24 * 60 * 60 * 1000); // Adds 1 day in milliseconds
  let expires = "expires=" + d.toUTCString(); // Formats the expiration date
  console.log(expires);
  document.cookie = "name=" + name + ";" + expires + ";path=/"; // Set the cookie
};

// Check if the cookie exists, otherwise set it
const checkCookie = () => {
  let name = getCookie("name"); // Check if "name" cookie exists
  if (name) {
    alert("Welcome again " + name); // If cookie exists, greet the user
  } else {
    setCookie(); // Otherwise, set the cookie
  }
};

// Retrieve a specific cookie by name
const getCookie = (cname) => {
  let name = cname + "=";
  let ca = document.cookie.split(";"); // Split cookies by ';'
  for (let i = 0; i < ca.length; i++) {
    let c = ca[i].trim(); // Remove leading spaces
    if (c.indexOf(name) == 0) {
      document.getElementById("cookie").innerHTML = c.substring(
        name.length,
        c.length
      );
      return c.substring(name.length, c.length); // Return the cookie value
    }
  }
  return ""; // Return empty string if cookie not found
};

const deleteCookie = () => {
  let ca = document.cookie.split(";"); // Split cookies by ';'
  for (let i = 0; i < ca.length; i++) {
    let c = ca[i].trim(); // Remove leading spaces
    document.cookie = c + "; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;"; // Delete the cookie
  }
  alert("Cookie Deleted");
  window.location.reload();
};
