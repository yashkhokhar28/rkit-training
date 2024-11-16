// Get project elements
const Project1 = document.getElementById("project-1");
const Project2 = document.getElementById("project-2");
const Project3 = document.getElementById("project-3");

// Add click event listeners to project images
Project1.addEventListener("click", () => {
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

Project2.addEventListener("click", () => {
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

Project3.addEventListener("click", () => {
  const res = confirm("Are you sure you want to go to project 3?");
  if (res) {
    window.open("http://shoppers.somee.com", "_blank");
  } else {
    alert("You chose not to go to project 3.");
  }
});

// Form submission handling
document
  .getElementById("contact-form")
  .addEventListener("submit", function (e) {
    e.preventDefault(); // Stop form submission

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
    if (workType === "") {
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
