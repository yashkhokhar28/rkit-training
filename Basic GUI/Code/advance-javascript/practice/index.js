// Display the name from local storage on page load
document.getElementById("before").innerHTML = localStorage.getItem("name");

/**
 * Function: SetLocalStorage
 * Description: Sets the name from the input field into local storage.
 * This function is triggered on form submission, preventing the default
 * behavior, and updates the displayed name on the page.
 * Parameters:
 *   - event (Event): The form submission event.
 * Returns: None
 */
const SetLocalStorage = (event) => {
  event.preventDefault();
  const value = document.getElementById("name").value;
  localStorage.setItem("name", value);

  // Update the displayed name after setting it
  if (localStorage.getItem("name")) {
    document.getElementById("after").innerHTML = localStorage.getItem("name");
  }
};

/**
 * Function: ClearLocalStorage
 * Description: Clears all entries in local storage and reloads the page
 * to reflect the changes. Alerts the user that local storage has been cleared.
 * Parameters: None
 * Returns: None
 */
const ClearLocalStorage = () => {
  localStorage.clear();
  alert("Local Storage Cleared");
  window.location.reload();
};

/**
 * Function: SetCookie
 * Description: Prompts the user for their name and sets a cookie with
 * that name, expiring in one day.
 * Parameters: None
 * Returns: None
 */
const SetCookie = () => {
  const name = prompt("Please enter your name:");
  if (!name) {
    alert("Name must be filled out");
    return;
  }
  const d = new Date();
  d.setTime(d.getTime() + 24 * 60 * 60 * 1000);
  const expires = "expires=" + d.toUTCString();
  document.cookie = "name=" + name + ";" + expires + ";path=/";
};

/**
 * Function: CheckCookie
 * Description: Checks if a specific cookie (name) exists. If it does,
 * greets the user, otherwise prompts to set a new cookie.
 * Parameters: None
 * Returns: None
 */
const CheckCookie = () => {
  const name = GetCookie("name");
  if (name) {
    alert("Welcome again " + name);
  } else {
    SetCookie();
  }
};

/**
 * Function: GetCookie
 * Description: Retrieves the value of a specified cookie by its name.
 * Parameters:
 *   - cname (string): The name of the cookie to retrieve.
 * Returns:
 *   - string: The value of the cookie, or an empty string if not found.
 */
const GetCookie = (cname) => {
  const name = cname + "=";
  const ca = document.cookie.split(";");
  for (let i = 0; i < ca.length; i++) {
    let c = ca[i].trim();
    if (c.indexOf(name) === 0) {
      document.getElementById("cookie").innerHTML = c.substring(name.length);
      return c.substring(name.length);
    }
  }
  return "";
};

/**
 * Function: DeleteCookie
 * Description: Deletes the specified cookie by setting its expiration
 * date to the past. Alerts the user that the cookie has been deleted.
 * Parameters: None
 * Returns: None
 */
const DeleteCookie = () => {
  const ca = document.cookie.split(";");
  for (let i = 0; i < ca.length; i++) {
    const c = ca[i].trim();
    document.cookie = c + "; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
  }
  alert("Cookie Deleted");
  window.location.reload();
};
