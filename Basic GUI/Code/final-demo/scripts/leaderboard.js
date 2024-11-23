$(document).ready(() => {
  /**
   * Function: AJAX GET Request to Fetch Scores
   * Description: Fetches the list of scores from the API, sorts them in descending order,
   * and dynamically updates the HTML elements to display the rank, name, and score.
   * Called in: Automatically executed when the document is fully loaded.
   * Parameters: None.
   * Returns: None. Updates the HTML content dynamically based on API data.
   */
  $.ajax({
    url: "https://673596e65995834c8a934a4d.mockapi.io/score",
    type: "GET",
    success: function (data) {
      console.log(data);

      let name = document.getElementById("name");
      let score = document.getElementById("score");
      let rank = document.getElementById("rank");

      // Sorts the fetched data in descending order of scores.
      data = data.sort((a, b) => b["score"] - a["score"]);

      for (let i = 0; i < data.length; i++) {
        rank.innerHTML += i + 1 + "<br>";
        name.innerHTML += data[i]["username"] + "<br>";
        score.innerHTML += data[i]["score"] + "<br>";
      }
    },
    error: function (error) {
      /**
       * Error Handling: Logs any errors encountered during the AJAX request.
       * Called in: Automatically triggered if the API request fails.
       * Parameters:
       *   - error (object): Contains details about the error.
       * Returns: None. Logs error details for debugging purposes.
       */
      console.log(error); // Log the error for debugging.
    },
  });
});
