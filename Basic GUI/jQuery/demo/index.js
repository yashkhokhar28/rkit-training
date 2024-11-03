$(document).ready(function () {
  // Project links with confirm dialogues
  $("#project-1").click(() => {
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

  $("#project-2").click(() => {
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

  $("#project-3").click(() => {
    const res = confirm("Are you sure you want to go to project 3?");
    if (res) {
      window.open("http://shoppers.somee.com", "_blank");
    } else {
      alert("You chose not to go to project 3.");
    }
  });

  $(document).ready(function () {
    $("#contact-form").on("submit", function (e) {
      e.preventDefault();

      // Initialize an empty array to store error messages
      let errors = [];

      // Form field values
      const name = $("#name").val().trim();
      const email = $("#email").val().trim();
      const phone = $("#phone").val().trim();
      const userType = $("input[name='user-type']:checked").val();
      const workType = $("#floatingSelect").val();
      const message = $("#message").val().trim();

      // Email validation pattern
      const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

      // Validation checks with error messages added to the array
      if (!name) {
        errors.push("Name is required.");
      }
      if (!email || !emailPattern.test(email)) {
        errors.push("A valid email is required.");
      }
      if (!phone || isNaN(phone) || phone.length < 10) {
        errors.push("A valid phone number (at least 10 digits) is required.");
      }
      if (!userType) {
        errors.push("Please select a user type.");
      }
      if (!workType) {
        errors.push("Please select a work type.");
      }
      if (!message) {
        errors.push("Message cannot be empty.");
      }

      // Display all errors or submit the form if no errors
      if (errors.length > 0) {
        alert("Please fix the following errors:\n- " + errors.join("\n- "));
      } else {
        alert("Form submitted successfully!");
        this.submit(); // Proceed with form submission
      }
    });
  });
});

$(document).ready(function () {
  // Initialize jQuery Validation on the form
  $("#contact-form").validate({
    // Specify validation rules
    rules: {
      name: {
        required: true,
        minlength: 3,
      },
      email: {
        required: true,
        email: true,
      },
      phone: {
        required: true,
        digits: true,
        minlength: 10,
        maxlength: 10,
      },
      "user-type": {
        required: true,
      },
      "work-type": {
        required: true,
      },
      message: {
        required: true,
        minlength: 5,
      },
    },
    // Specify validation error messages
    messages: {
      name: {
        required: "Please enter your name",
        minlength: "Your name must consist of at least 3 characters",
      },
      email: {
        required: "Please enter a valid email address",
      },
      phone: {
        required: "Please provide a phone number",
        digits: "Please enter only digits",
        minlength: "Your phone number must be at least 10 digits",
        maxlength: "Your phone number must be no more than 10 digits",
      },
      "user-type": {
        required: "Please select a user type",
      },
      "work-type": {
        required: "Please select your work type",
      },
      message: {
        required: "Please enter a message",
        minlength: "Your message must be at least 5 characters",
      },
    },
    // Display all errors together in a designated container
    errorContainer: "#error-messages",
    errorLabelContainer: "#error-messages",
    wrapper: "li", // Wrap each error message in a list item
    // Custom submission handler
    submitHandler: function (form) {
      alert("Form submitted successfully!");
      form.submit(); // Proceed with form submission
    },
  });
});

const fetchUserData = () => {
  return $.ajax({
    url: "https://jsonplaceholder.typicode.com/users/1",
    method: "GET",
  });
};

const fetchUserPosts = () => {
  return $.ajax({
    url: "https://jsonplaceholder.typicode.com/posts?userId=1",
    method: "GET",
  });
};

const fetchUserAndPosts = () => {
  const userDeferred = $.Deferred();
  const postsDeferred = $.Deferred();

  // Fetch user data
  fetchUserData()
    .done((userData) => {
      userDeferred.resolve(userData); // Resolve user data
    })
    .fail(() => {
      userDeferred.reject("Failed to fetch user data"); // Reject on error
    });

  // Fetch user posts
  fetchUserPosts()
    .done((postsData) => {
      postsDeferred.resolve(postsData); // Resolve posts data
    })
    .fail(() => {
      postsDeferred.reject("Failed to fetch posts data"); // Reject on error
    });

  // Combine promises
  $.when(userDeferred.promise(), postsDeferred.promise()).done(
    (userData, postsData) => {
      console.log("User Data:", userData);
      console.log("User Posts:", postsData);
    }
  );

  $.when(userDeferred.promise(), postsDeferred.promise())
    .done((userData, postsData) => {
      console.log("User Data:", userData);
      console.log("User Posts:", postsData[0]);
    })
    .fail((error) => {
      console.error("Failed to fetch user and posts data:", error);
    });
};

// Call the function to fetch data
fetchUserAndPosts();
