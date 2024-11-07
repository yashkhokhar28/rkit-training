document.addEventListener("DOMContentLoaded", () => {
  let QuestionList = [];
  let UserAnswer = {};
  let CurrentQuestionIndex = 0;

  /**
   * Function: FetchQuestions
   * Description: Asynchronously fetches questions from a given URL and stores them in the QuestionList.
   * Shows a spinner while loading and hides it when questions are loaded.
   * Calls DisplayQuestion() and DisplayTotalQuestions() once the questions are loaded.
   * Parameters: None
   * Returns: None
   */

  const FetchQuestions = async () => {
    const spinner = document.querySelector(".spinner-border");
    const questionContainer = document.getElementById("question-container");

    // Show the spinner and hide the question container initially
    if (spinner) spinner.style.display = "block";
    if (questionContainer) questionContainer.style.display = "none";

    try {
      let response = await fetch(
        "https://gist.githubusercontent.com/yashkhokhar28/ca43e3f93db86ebbb7f9a3c0a8fdf808/raw/question.json"
      );
      QuestionList = await response.json();

      // Hide the spinner and show the question container when data is loaded
      if (spinner) spinner.style.display = "none";
      if (questionContainer) questionContainer.style.display = "block";

      DisplayQuestion();
      DisplayTotalQuestions();
    } catch (error) {
      console.error("Error fetching questions:", error);
      alert("Failed to load questions. Please try again later.");
    }
  };

  /**
   * Function: DisplayQuestion
   * Description: Displays the current question and options from QuestionList in the DOM.
   * Updates the question text and creates option radio buttons dynamically.
   * Parameters: None
   * Returns: None
   */
  const DisplayQuestion = () => {
    const CurrentQuestion = QuestionList[CurrentQuestionIndex];
    const QuestionText = document.getElementById("question-text");
    const OptionContainer = document.getElementById("options-container");

    if (!CurrentQuestion || !QuestionText || !OptionContainer) return;

    // Update question text
    QuestionText.innerHTML = `Q${CurrentQuestionIndex + 1}: ${
      CurrentQuestion.question
    }`;

    // Clear previous options
    OptionContainer.innerHTML = "";

    // Create options
    CurrentQuestion.options.forEach((option, index) => {
      const optionDiv = document.createElement("div");
      optionDiv.classList.add("option");

      const radioInput = document.createElement("input");
      radioInput.type = "radio";
      radioInput.name = "option";
      radioInput.id = `option${index + 1}`;
      radioInput.value = option;

      const label = document.createElement("label");
      label.setAttribute("for", `option${index + 1}`);
      label.textContent = option;

      // Set checked if the user has answered this question
      if (UserAnswer[CurrentQuestionIndex] === option) {
        radioInput.checked = true;
      }

      optionDiv.appendChild(radioInput);
      optionDiv.appendChild(label);
      OptionContainer.appendChild(optionDiv);
    });
  };

  /**
   * Function: ProgressBar
   * Description: Updates the progress bar to reflect the user's current progress in the quiz.
   * Parameters: None
   * Returns: None
   */
  const ProgressBar = () => {
    const ProgressBar = document.getElementById("progress-bar");
    const ProgressBarContainer = document.getElementById(
      "progress-bar-container"
    );
    if (!ProgressBar || !ProgressBarContainer) return;

    const Progress = (CurrentQuestionIndex / QuestionList.length) * 100;
    ProgressBar.style.width = `${Progress}%`;
    ProgressBarContainer.setAttribute("aria-valuenow", Progress);
    ProgressBarContainer.setAttribute("aria-valuemin", 0);
    ProgressBarContainer.setAttribute("aria-valuemax", 100);
  };

  /**
   * Function: DisplayTotalQuestions
   * Description: Updates the DOM to show the total number of questions and the current question number.
   * Parameters: None
   * Returns: None
   */
  const DisplayTotalQuestions = () => {
    const TotalQuestions = document.getElementById("total-questions");
    if (TotalQuestions) {
      TotalQuestions.innerHTML = `Question ${CurrentQuestionIndex + 1} of ${
        QuestionList.length
      }`;
      TotalQuestions.setAttribute("class", "text-center");
    }
  };

  /**
   * Function: CalculateResult
   * Description: Calculates the user's total score by comparing their answers with the correct answers.
   * Parameters: None
   * Returns: (number) The user's score.
   */
  const CalculateResult = () => {
    let Score = 0;
    QuestionList.forEach((question, index) => {
      if (question.correctAnswer === UserAnswer[index]) {
        Score++;
      }
    });
    return Score;
  };

  /**
   * Function: DisplayResult
   * Description: Displays the user's score, total questions, and percentage in a result container.
   * Also includes a button to redirect to the home page.
   * Parameters: None
   * Returns: None
   */
  const DisplayResult = () => {
    const ResultContainer = document.querySelector(".result");
    if (!ResultContainer) return;

    const Score = CalculateResult();
    const TotalQuestions = QuestionList.length;

    ResultContainer.innerHTML = `
      <div class="score-container text-center d-flex justify-content-center align-items-center">
        <div class="score-box">
          <h1 class="score-heading">Quiz Completed!</h1>
          <p>You scored ${Score} out of ${TotalQuestions}.</p>
          <p>Percentage: ${(Score / TotalQuestions) * 100}%</p>
          <button onclick="window.location.href='./index.html'" class="btn btn-primary mt-3">Go to Home</button>
        </div>
      </div>
    `;
  };

  // Event listeners for navigation buttons

  /**
   * Event Listener: Next Button
   * Description: Moves to the next question if an option is selected. Otherwise, alerts the user to select an option.
   * Updates the progress bar and question display. If it's the last question, it shows the result.
   * Parameters: None
   * Returns: None
   */
  document.getElementById("next-btn")?.addEventListener("click", () => {
    let SelectedOption = document.querySelector("input[name='option']:checked");
    if (!SelectedOption) {
      alert("Please select an option before proceeding.");
      return;
    }

    UserAnswer[CurrentQuestionIndex] = SelectedOption.value;

    if (CurrentQuestionIndex < QuestionList.length - 1) {
      CurrentQuestionIndex++;
      DisplayQuestion();
      ProgressBar();
      DisplayTotalQuestions();
    } else {
      DisplayResult();
    }
  });

  /**
   * Event Listener: Previous Button
   * Description: Moves to the previous question and updates the progress bar and question display.
   * Parameters: None
   * Returns: None
   */
  document.getElementById("prev-btn")?.addEventListener("click", () => {
    if (CurrentQuestionIndex > 0) {
      CurrentQuestionIndex--;
      DisplayQuestion();
      ProgressBar();
      DisplayTotalQuestions();
    }
  });

  // Initial call to fetch and display questions
  FetchQuestions();
});
