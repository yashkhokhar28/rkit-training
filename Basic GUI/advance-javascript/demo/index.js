/**
 * Function: CheckCookie
 * Description: Checks if the "name" cookie exists in the browser.
 * If found, it alerts a welcome message with the name. If not,
 * it calls SetCookie() to prompt the user for a name and sets the cookie.
 * Called in: <body onload="CheckCookie()"> in the HTML file.
 * Parameters: None
 */
let CheckCookie = () => {
  let name = GetCookie("name");
  if (name) {
    alert("Welcome again " + name);
  } else {
    SetCookie();
  }
};

/**
 * Function: GetCookie
 * Description: Retrieves the value of a specific cookie by its name.
 * Loops through all available cookies, and if the cookie with the
 * given name exists, it returns its value.
 * Called in: CheckCookie() function in this JavaScript file.
 * Parameters:
 *   - cname (string): The name of the cookie to retrieve.
 * Returns: The value of the cookie or an empty string if not found.
 */
let GetCookie = (cname) => {
  let name = cname + "=";
  let cookieAll = document.cookie.split(";");
  for (let i = 0; i < cookieAll.length; i++) {
    let c = cookieAll[i].trim();
    if (c.indexOf(name) == 0) {
      return c.substring(name.length, c.length);
    }
  }
  return ""; // Return empty string if cookie not found
};

/**
 * Function: SetCookie
 * Description: Prompts the user to enter their name and sets a cookie
 * with that name that expires in 1 day. The cookie is stored in the
 * user's browser with a path of '/'.
 * Called in: CheckCookie() function in this JavaScript file.
 * Parameters: None
 * Returns: None
 */
let SetCookie = () => {
  var name = prompt("Please enter your name:");
  if (name == null || name == "") {
    alert("Name must be filled out");
    return false;
  }
  let d = new Date();
  d.setTime(d.getTime() + 24 * 60 * 60 * 1000); // Adds 1 day in milliseconds
  let expires = "expires=" + d.toUTCString(); // Formats the expiration date
  console.log(expires);
  document.cookie = "name=" + name + ";" + expires + ";path=/"; // Set the cookie
};

/**
 * Function: SaveFormData
 * Description: Collects the form data (name, email, phone, user type,
 * work type, and message) from the DOM and saves it to both
 * localStorage and sessionStorage as a JSON string.
 * Called in: <form id="contact-form"> submit event listener in this JavaScript file.
 * Parameters: None
 * Returns: None
 */
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

/**
 * Function: PopulateModal
 * Description: Retrieves the form data from localStorage, formats it
 * into an HTML list, and populates the modal with this data. If no data
 * is found, an alert is shown.
 * Called in: <button onclick="PopulateModal()"> in the HTML file.
 * Parameters: None
 * Returns: None
 */
let PopulateModal = () => {
  let formData = JSON.parse(localStorage.getItem("formData"));
  if (formData) {
    let reviewData = `
      <ul class="list-group">
          <li class="list-group-item"><strong>Name:</strong> ${formData.name}</li>
          <li class="list-group-item"><strong>Email:</strong> ${formData.email}</li>
          <li class="list-group-item"><strong>Phone:</strong> ${formData.phone}</li>
          <li class="list-group-item"><strong>User Type:</strong> ${formData.userType}</li>
          <li class="list-group-item"><strong>Work Type:</strong> ${formData.workType}</li>
          <li class="list-group-item"><strong>Message:</strong> ${formData.message}</li>
      </ul>
    `;
    document.getElementById("reviewData").innerHTML = reviewData;
  } else {
    alert("No Data Found");
  }
};

/**
 * Function: ClearLocalStorage
 * Description: Clears all data from localStorage and reloads the page.
 * It also shows an alert confirming the clearance of local storage.
 * Called in: <button onclick="ClearLocalStorage()"> in the HTML file.
 * Parameters: None
 * Returns: None
 */
let ClearLocalStorage = () => {
  localStorage.clear();
  alert("Local Storage Cleared");
  window.location.reload();
};

/**
 * Function: DeleteCookie
 * Description: Deletes all cookies by setting their expiration date
 * to January 1, 1970, which effectively removes them from the browser.
 * Called in: Not explicitly called in the HTML file.
 * Parameters: None
 * Returns: None
 */
let DeleteCookie = () => {
  let cookieAll = document.cookie.split(";"); // Split cookies by ';'
  for (let i = 0; i < cookieAll.length; i++) {
    let c = cookieAll[i].trim(); // Remove leading spaces
    document.cookie = c + "; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;"; // Delete the cookie
  }
  alert("All Cookies Deleted");
};

/**
 * Event Listener: Project1
 * Description: Prompts the user with a confirmation to open project 1.
 * If confirmed, it opens the project in a new tab.
 * Called in: <img id="project-1"> in the HTML file.
 * Parameters: None
 * Returns: None
 */
Project1.addEventListener("click", () => {
  let res = confirm("Are you sure you want to go to project 1?");
  if (res) {
    window.open(
      "https://play.google.com/store/apps/details?id=com.vh.vehiclemanager&pcampaignid=web_share",
      "_blank"
    );
  } else {
    alert("You chose not to go to project 1.");
  }
});

/**
 * Event Listener: Project2
 * Description: Prompts the user with a confirmation to open project 2.
 * If confirmed, it opens the project in a new tab.
 * Called in: <img id="project-2"> in the HTML file.
 * Parameters: None
 * Returns: None
 */
Project2.addEventListener("click", () => {
  let res = confirm("Are you sure you want to go to project 2?");
  if (res) {
    window.open(
      "https://play.google.com/store/apps/details?id=com.aswdc_quotationgenerator&pcampaignid=web_share",
      "_blank"
    );
  } else {
    alert("You chose not to go to project 2.");
  }
});

/**
 * Event Listener: Project3
 * Description: Prompts the user with a confirmation to open project 3.
 * If confirmed, it opens the project in a new tab.
 * Called in: <img id="project-3"> in the HTML file.
 * Parameters: None
 * Returns: None
 */
Project3.addEventListener("click", () => {
  let res = confirm("Are you sure you want to go to project 3?");
  if (res) {
    window.open("http://shoppers.somee.com", "_blank");
  } else {
    alert("You chose not to go to project 3.");
  }
});
