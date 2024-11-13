$(document).ready(() => {
  $("#sign-up").validate({
    rules: {
      username: {
        required: true,
        minlength: 3,
      },
      password: {
        required: true,
        minlength: 8,
      },
    },
    messages: {
      username: {
        required: "Please enter a username",
        minlength: "Your username must consist of at least 3 characters",
      },
      password: {
        required: "Please provide a password",
        minlength: "Your password must be at least 8 characters long",
      },
    },
    errorContainer: "#error-messages",
    errorLabelContainer: "#error-messages",
    wrapper: "li",
    submitHandler: function (form) {
      let userName = document.getElementById("username").value;
      let password = document.getElementById("password").value;
      let d = new Date();
      d.setTime(d.getTime() + 24 * 60 * 60 * 1000); // Adds 1 day in milliseconds
      let expires = "expires=" + d.toUTCString();
      document.cookie = "username=" + userName + ";" + expires + ";path=/";
      document.cookie = "password=" + password + ";" + expires + ";path=/";
      alert("Form submitted");

      // Redirect to sign-in.html after form submission
      window.location.href = "sign-in.html";
    },
  });
});
