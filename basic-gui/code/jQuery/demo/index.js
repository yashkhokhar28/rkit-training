$(document).ready(function () {
  // Project links with confirmation dialogues
  $("#project-1").click(() =>
    HandleProjectClick(
      1,
      "https://play.google.com/store/apps/details?id=com.vh.vehiclemanager&pcampaignid=web_share"
    )
  );
  $("#project-2").click(() =>
    HandleProjectClick(
      2,
      "https://play.google.com/store/apps/details?id=com.aswdc_quotationgenerator&pcampaignid=web_share"
    )
  );
  $("#project-3").click(() =>
    HandleProjectClick(3, "http://shoppers.somee.com")
  );

  /**
   * Function: HandleProjectClick
   * Description: Displays a confirmation dialog to the user before navigating to a project URL.
   * Called in: Click event of each project link.
   * Parameters:
   *   - projectNumber: The project number to display in the confirmation.
   *   - url: The URL to navigate to if the user confirms.
   * Returns: None.
   */
  function HandleProjectClick(projectNumber, url) {
    const res = confirm(
      `Are you sure you want to go to project ${projectNumber}?`
    );
    if (res) {
      window.open(url, "_blank");
    } else {
      alert(`You chose not to go to project ${projectNumber}.`);
    }
  }

  // Contact Form Validation
  $("#contact-form").validate({
    rules: {
      name: { required: true, minlength: 3 },
      email: { required: true, email: true },
      phone: { required: true, digits: true, minlength: 10, maxlength: 10 },
      "user-type": { required: true },
      "work-type": { required: true },
      message: { required: true, minlength: 5 },
    },
    messages: {
      name: {
        required: "Please enter your name",
        minlength: "Your name must be at least 3 characters",
      },
      email: { required: "Please enter a valid email address" },
      phone: {
        required: "Please provide a valid 10-digit phone number",
        digits: "Only numbers allowed",
      },
      "user-type": { required: "Please select a user type" },
      "work-type": { required: "Please select your work type" },
      message: {
        required: "Please enter a message",
        minlength: "Message must be at least 5 characters",
      },
    },
    errorPlacement: function (error, element) {
      if (element.attr("name") === "user-type") {
        // Place error after the last radio button
        $("#user-type-error").html(error);
      } else if (element.attr("name") === "work-type") {
        // Place error after the select box
        $("#work-type-error").html(error);
      } else {
        let id = element.attr("id") + "-error"; // Assign error message to correct span
        $("#" + id).html(error);
      }
    },
    submitHandler: function (form) {
      alert("Form submitted successfully!");
      form.submit(); // Submit the form
    },
  });

  /**
   * Function: FetchUserData
   * Description: Fetches user data from the API.
   * Called in: FetchUserAndPosts function to retrieve user data.
   * Parameters: None.
   * Returns: jQuery AJAX promise with user data.
   */
  function FetchUserData() {
    return $.ajax({
      url: "https://jsonplaceholder.typicode.com/users/1",
      method: "GET",
    });
  }

  /**
   * Function: FetchUserPosts
   * Description: Fetches posts data of the user from the API.
   * Called in: FetchUserAndPosts function to retrieve posts data.
   * Parameters: None.
   * Returns: jQuery AJAX promise with posts data.
   */
  function FetchUserPosts() {
    return $.ajax({
      url: "https://jsonplaceholder.typicode.com/posts?userId=1",
      method: "GET",
    });
  }

  /**
   * Function: FetchUserAndPosts
   * Description: Fetches both user data and posts concurrently using AJAX promises.
   * Logs the user data and posts to the console once fetched.
   * Called in: After the page loads, it initiates the fetching process.
   * Parameters: None.
   * Returns: None.
   */
  function FetchUserAndPosts() {
    $.when(FetchUserData(), FetchUserPosts())
      .done((userData, postsData) => {
        console.log("User Data:", userData);
        console.log("User Posts:", postsData[0]);
      })
      .fail((error) => console.error("Failed to fetch data:", error));
  }

  FetchUserAndPosts();

  /**
   * Function: SquareNumbers
   * Description: Squares each number in the numbers array using $.map().
   * Called in: jQuery utility methods section.
   * Parameters: None.
   * Returns: Array of squared numbers.
   */
  const numbers = [1, 2, 3, 4, 5];
  const squares = $.map(numbers, (num) => num * num);
  console.log("Squares:", squares); // Output: [1, 4, 9, 16, 25]

  /**
   * Function: FilterEvenNumbers
   * Description: Filters out even numbers from the numbers array using $.grep().
   * Called in: jQuery utility methods section.
   * Parameters: None.
   * Returns: Array of even numbers.
   */
  const evens = $.grep(numbers, (num) => num % 2 === 0);
  console.log("Even Numbers:", evens); // Output: [2, 4]

  /**
   * Function: MergeArrays
   * Description: Merges two arrays using $.merge().
   * Called in: jQuery utility methods section.
   * Parameters: None.
   * Returns: Merged array.
   */
  const mergedArray = $.merge([1, 2, 3], [4, 5, 6]);
  console.log("Merged Array:", mergedArray); // Output: [1, 2, 3, 4, 5, 6]

  /**
   * Function: ExtendObjects
   * Description: Extends one object with properties from another using $.extend().
   * Called in: jQuery utility methods section.
   * Parameters: None.
   * Returns: Extended object with properties from both objects.
   */
  const defaultConfig = {
    backgroundColor: "white",
    fontSize: "14px",
    fontFamily: "Arial",
  };
  const userConfig = { fontSize: "16px", fontFamily: "Verdana" };
  const finalConfig = $.extend({}, defaultConfig, userConfig);
  console.log("Final Config:", finalConfig); // Output: { backgroundColor: "white", fontSize: "16px", fontFamily: "Verdana" }

  /**
   * Function: IterateOverArray
   * Description: Iterates over an array of colors using $.each().
   * Called in: jQuery utility methods section.
   * Parameters: None.
   * Returns: None.
   */
  const colors = ["red", "green", "blue"];
  $.each(colors, (index, color) =>
    console.log(`Color at index ${index} is ${color}`)
  );
});
