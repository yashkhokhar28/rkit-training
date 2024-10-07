const project1 = document.getElementById("project-1");
const project2 = document.getElementById("project-2");
const project3 = document.getElementById("project-3");

project1.addEventListener("click", () => {
  const res = confirm("Are you sure you want to go to project 1?");
  if (res) {
    window.open(
      "https://play.google.com/store/apps/details?id=com.vh.vehiclemanager&pcampaignid=web_share",
      "_blank"
    );
  } else {
    alert("You chose not to go to project 1.");
  }
});

project2.addEventListener("click", () => {
  const res = confirm("Are you sure you want to go to project 2?");
  if (res) {
    window.open(
      "https://play.google.com/store/apps/details?id=com.aswdc_quotationgenerator&pcampaignid=web_share",
      "_blank"
    );
  } else {
    alert("You chose not to go to project 2.");
  }
});

project3.addEventListener("click", () => {
  const res = confirm("Are you sure you want to go to project 3?");
  if (res) {
    window.open("http://shoppers.somee.com", "_blank");
  } else {
    alert("You chose not to go to project 3.");
  }
});

document
  .getElementById("contact-form")
  .addEventListener("submit", function (e) {
    // Stop form submission
    e.preventDefault();

    // Validate Name
    let name = document.getElementById("name").value.trim();
    if (name === "") {
      alert("Please enter your name.");
      return false;
    }

    // Validate Email
    let email = document.getElementById("email").value.trim();
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (email === "" || !emailPattern.test(email)) {
      alert("Please enter a valid email address.");
      return false;
    }

    // Validate Phone
    let phone = document.getElementById("phone").value.trim();
    if (phone === "" || isNaN(phone) || phone.length < 10) {
      alert("Please enter a valid phone number (minimum 10 digits).");
      return false;
    }

    // Validate Radio Button for User Type
    let userTypeSelected = document.querySelector(
      'input[name="user-type"]:checked'
    );
    if (!userTypeSelected) {
      alert("Please select a user type.");
      return false;
    }

    // Validate Dropdown for Work Type
    let workType = document.getElementById("floatingSelect").value;
    if (workType === "Open this select menu") {
      alert("Please select your work type.");
      return false;
    }

    // Validate Message (Optional, but checking if it exists)
    let message = document.getElementById("message").value.trim();
    if (message === "") {
      alert("Please enter a message.");
      return false;
    }

    // If all validations pass, allow form submission
    alert("Form submitted successfully!");
  });

const saveFormData = () => {
  const formData = {
    name: document.getElementById("name").value,
    email: document.getElementById("email").value,
    phone: document.getElementById("phone").value,
    userType: document.querySelector('input[name="user-type"]:checked').value,
    workType: document.getElementById("floatingSelect").value,
    message: document.getElementById("message").value,
  };
  localStorage.setItem("formData", JSON.stringify(formData));
};

// Populate the modal with localStorage data
const populateModal = () => {
  const formData = JSON.parse(localStorage.getItem("formData"));
  if (formData) {
    const reviewData = `
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

// Save form data to localStorage on form submission (example usage)
document
  .getElementById("contact-form")
  .addEventListener("submit", function (e) {
    e.preventDefault(); // Prevent actual submission for demonstration
    saveFormData(); // Save data to localStorage
    alert("Data Saved to LocalStorage");
  });

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
