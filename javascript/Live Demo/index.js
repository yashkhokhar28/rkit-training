const project1 = document.getElementById("project-1");
const project2 = document.getElementById("project-2");
const project3 = document.getElementById("project-3");

project1.addEventListener("click", () => {
  const res = confirm("Are you sure you want to go to project 1?");
  if (res) {
    window.location.href =
      "https://play.google.com/store/apps/details?id=com.vh.vehiclemanager&pcampaignid=web_share";
    window.location.target = "_blank";
  } else {
    alert("You chose not to go to project 1.");
  }
});

project2.addEventListener("click", () => {
  const res = confirm("Are you sure you want to go to project 2?");
  if (res) {
    window.location.href =
      "https://play.google.com/store/apps/details?id=com.aswdc_quotationgenerator&pcampaignid=web_share";
    window.location.target = "_blank";
  } else {
    alert("You chose not to go to project 2.");
  }
});

project3.addEventListener("click", () => {
  const res = confirm("Are you sure you want to go to project 3?");
  if (res) {
    window.location.href = "http://shoppers.somee.com";
    window.location.target = "_blank";
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
    const name = document.getElementById("name").value.trim();
    if (name === "") {
      alert("Please enter your name.");
      return false;
    }

    // Validate Email
    const email = document.getElementById("email").value.trim();
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (email === "" || !emailPattern.test(email)) {
      alert("Please enter a valid email address.");
      return false;
    }

    // Validate Phone
    const phone = document.getElementById("phone").value.trim();
    if (phone === "" || isNaN(phone) || phone.length < 10) {
      alert("Please enter a valid phone number (minimum 10 digits).");
      return false;
    }

    // Validate Radio Button for User Type
    const userTypeSelected = document.querySelector(
      'input[name="user-type"]:checked'
    );
    if (!userTypeSelected) {
      alert("Please select a user type.");
      return false;
    }

    // Validate Dropdown for Work Type
    const workType = document.getElementById("floatingSelect").value;
    if (workType === "") {
      alert("Please select your work type.");
      return false;
    }

    // Validate Message (Optional, but checking if it exists)
    const message = document
      .getElementById("exampleFormControlTextarea1")
      .value.trim();
    if (message === "") {
      alert("Please enter a message.");
      return false;
    }

    // If all validations pass, allow form submission
    alert("Form submitted successfully!");
    e.target.submit();
  });

// Array of words or phrases to cycle through
const words = [
  "Full Stack Developer",
  "JavaScript Enthusiast",
  "UI/UX Designer",
  "Backend Expert",
  "Tech Innovator",
];

let index = 0; // Start index for the array
let charIndex = 0; // Character index to type out each word
let typingSpeed = 100; // Speed of typing (in milliseconds)
let deletingSpeed = 50; // Speed of deleting (in milliseconds)
let delayBetweenWords = 1500; // Delay after a word is fully typed (in milliseconds)
let isDeleting = false; // Flag to determine whether typing or deleting

// Function to handle typing animation
function typeText() {
  const textElement = document.getElementById("dynamicText");
  const currentWord = words[index]; // Get the current word

  if (isDeleting) {
    // If deleting, remove characters
    textElement.innerHTML = currentWord.substring(0, charIndex - 1);
    charIndex--;
  } else {
    // If typing, add characters
    textElement.innerHTML = currentWord.substring(0, charIndex + 1);
    charIndex++;
  }

  // If the word is completely typed out, start deleting after delay
  if (!isDeleting && charIndex === currentWord.length) {
    isDeleting = true;
    setTimeout(typeText, delayBetweenWords); // Pause before deleting
  }
  // If the word is completely deleted, move to the next word
  else if (isDeleting && charIndex === 0) {
    isDeleting = false;
    index = (index + 1) % words.length; // Move to the next word, loop back to start
    setTimeout(typeText, typingSpeed);
  } else {
    // Continue typing or deleting
    const speed = isDeleting ? deletingSpeed : typingSpeed;
    setTimeout(typeText, speed);
  }
}

// Start the typing animation
document.addEventListener("DOMContentLoaded", function () {
  typeText();
});
