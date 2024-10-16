// Display the name from local storage on page load
document.getElementById("before").innerHTML = localStorage.getItem("name");

// Function to set the name in local storage
const SetLocalStorage = (event) => {
  event.preventDefault(); // Prevent the default form submission behavior
  const value = document.getElementById("name").value;
  localStorage.setItem("name", value); // Store the name in local storage

  // Update the displayed name after setting it
  if (localStorage.getItem("name")) {
    document.getElementById("after").innerHTML = localStorage.getItem("name");
  }
};

// Function to clear local storage
const ClearLocalStorage = () => {
  localStorage.clear(); // Clear local storage
  alert("Local Storage Cleared");
  window.location.reload(); // Reload the page to reflect changes
};

// Function to set a cookie with a 1-day expiration
const SetCookie = () => {
  const name = prompt("Please enter your name:");
  if (!name) {
    alert("Name must be filled out");
    return;
  }
  const d = new Date();
  d.setTime(d.getTime() + 24 * 60 * 60 * 1000); // Adds 1 day in milliseconds
  const expires = "expires=" + d.toUTCString(); // Formats the expiration date
  document.cookie = "name=" + name + ";" + expires + ";path=/"; // Set the cookie
};

// Function to check if the cookie exists, otherwise set it
const CheckCookie = () => {
  const name = getCookie("name"); // Check if "name" cookie exists
  if (name) {
    alert("Welcome again " + name); // If cookie exists, greet the user
  } else {
    setCookie(); // Otherwise, set the cookie
  }
};

// Function to retrieve a specific cookie by name
const GetCookie = (cname) => {
  const name = cname + "=";
  const ca = document.cookie.split(";"); // Split cookies by ';'
  for (let i = 0; i < ca.length; i++) {
    let c = ca[i].trim(); // Remove leading spaces
    if (c.indexOf(name) === 0) {
      document.getElementById("cookie").innerHTML = c.substring(name.length);
      return c.substring(name.length); // Return the cookie value
    }
  }
  return ""; // Return empty string if cookie not found
};

// Function to delete the cookie
const DeleteCookie = () => {
  const ca = document.cookie.split(";"); // Split cookies by ';'
  for (let i = 0; i < ca.length; i++) {
    const c = ca[i].trim(); // Remove leading spaces
    document.cookie = c + "; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;"; // Delete the cookie
  }
  alert("Cookie Deleted");
  window.location.reload(); // Reload the page to reflect changes
};
