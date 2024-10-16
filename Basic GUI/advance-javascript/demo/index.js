// Function: CheckCookie
// Called in: <body onload="CheckCookie()"> in the HTML file
let CheckCookie = () => {
  let name = GetCookie("name");
  if (name) {
    alert("Welcome again " + name);
  } else {
    SetCookie();
  }
};

// Function: GetCookie
// Called in: CheckCookie() function in this JavaScript file
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

// Function: SetCookie
// Called in: CheckCookie() function in this JavaScript file
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

// Function: SaveFormData
// Called in: <form id="contact-form"> submit event listener in this JavaScript file
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

// Function: PopulateModal
// Called in: <button onclick="PopulateModal()"> in the HTML file
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

// Function: ClearLocalStorage
// Called in: <button onclick="ClearLocalStorage()"> in the HTML file
let ClearLocalStorage = () => {
  localStorage.clear();
  alert("Local Storage Cleared");
  window.location.reload();
};

// Function: DeleteCookie
// Called in: Not called explicitly in the HTML file
let DeleteCookie = () => {
  let cookieAll = document.cookie.split(";"); // Split cookies by ';'
  for (let i = 0; i < cookieAll.length; i++) {
    let c = cookieAll[i].trim(); // Remove leading spaces
    document.cookie = c + "; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;"; // Delete the cookie
  }
  alert("All Cookies Deleted");
};

// Project click event listeners
// Called in: <img id="project-1">, <img id="project-2">, <img id="project-3"> in the HTML file
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

// Project click event listeners
// Called in: <img id="project-2"> in the HTML file
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

// Project click event listeners
// Called in: <img id="project-3"> in the HTML file
Project3.addEventListener("click", () => {
  let res = confirm("Are you sure you want to go to project 3?");
  if (res) {
    window.open("http://shoppers.somee.com", "_blank");
  } else {
    alert("You chose not to go to project 3.");
  }
});
