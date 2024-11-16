import { Cookies } from "./Cookies.js";
import { Auth } from "./Auth.js";

export class Questions {
  static QuestionList = [];
  static UserAnswer = {};
  static CurrentQuestionIndex = 0;

  /**
   * Function: FetchQuestions
   * Description: Fetches questions from an API, handles the loading state, and initializes the first question.
   */
  static FetchQuestions = async () => {
    const Spinner = document.querySelector(".spinner-border");
    const QuestionContainer = document.getElementById("question-container");

    // Show the spinner and hide the question container initially
    if (Spinner) Spinner.style.display = "block";
    if (QuestionContainer) QuestionContainer.style.display = "none";

    try {
      let Response = await fetch(
        "https://673596e65995834c8a934a4d.mockapi.io/questions"
      );
      Questions.QuestionList = await Response.json();

      // Hide the spinner and show the question container when data is loaded
      if (Spinner) Spinner.style.display = "none";
      if (QuestionContainer) QuestionContainer.style.display = "block";

      Questions.DisplayQuestion();
      Questions.DisplayTotalQuestions();
    } catch (error) {
      console.error("Error fetching questions:", error);
      alert("Failed to load questions. Please try again later.");
    }
  };

  /**
   * Function: DisplayQuestion
   * Description: Displays the current question and options based on the index.
   */
  static DisplayQuestion = () => {
    const CurrentQuestion =
      Questions.QuestionList[Questions.CurrentQuestionIndex];
    const QuestionText = document.getElementById("question-text");
    const OptionContainer = document.getElementById("options-container");

    if (!CurrentQuestion || !QuestionText || !OptionContainer) return;

    // Update question text
    QuestionText.innerHTML = `Q${Questions.CurrentQuestionIndex + 1}: ${
      CurrentQuestion.question
    }`;

    // Clear previous options
    OptionContainer.innerHTML = "";

    // Create options dynamically
    CurrentQuestion.options.forEach((option, index) => {
      const OptionDiv = document.createElement("div");
      OptionDiv.classList.add("option");

      const RadioInput = document.createElement("input");
      RadioInput.type = "radio";
      RadioInput.name = "option";
      RadioInput.id = `option${index + 1}`;
      RadioInput.value = option;

      const Label = document.createElement("label");
      Label.setAttribute("for", `option${index + 1}`);
      Label.textContent = option;

      // Set the radio button as checked if the user has selected this option
      if (Questions.UserAnswer[Questions.CurrentQuestionIndex] === option) {
        RadioInput.checked = true;
      }

      OptionDiv.appendChild(RadioInput);
      OptionDiv.appendChild(Label);
      OptionContainer.appendChild(OptionDiv);
    });
  };

  /**
   * Function: ProgressBar
   * Description: Updates the progress bar based on the current question index.
   */
  static ProgressBar = () => {
    const ProgressBar = document.getElementById("progress-bar");
    const ProgressBarContainer = document.getElementById(
      "progress-bar-container"
    );
    if (!ProgressBar || !ProgressBarContainer) return;

    const Progress =
      (Questions.CurrentQuestionIndex / Questions.QuestionList.length) * 100;
    ProgressBar.style.width = `${Progress}%`;
    ProgressBarContainer.setAttribute("aria-valuenow", Progress);
    ProgressBarContainer.setAttribute("aria-valuemin", 0);
    ProgressBarContainer.setAttribute("aria-valuemax", 100);
  };

  /**
   * Function: DisplayTotalQuestions
   * Description: Updates the DOM to show the current question number and total questions.
   */
  static DisplayTotalQuestions = () => {
    const TotalQuestions = document.getElementById("total-questions");
    if (TotalQuestions) {
      TotalQuestions.innerHTML = `Question ${
        Questions.CurrentQuestionIndex + 1
      } of ${Questions.QuestionList.length}`;
    }
    TotalQuestions.setAttribute("class", "text-center");
  };

  /**
   * Function: SaveUserAnswer
   * Description: Saves the selected answer for the current question.
   */
  static SaveUserAnswer = () => {
    let Options = document.getElementsByName("option");
    let SelectedOption = Array.from(Options).find((option) => option.checked);
    if (SelectedOption) {
      Questions.UserAnswer[Questions.CurrentQuestionIndex] =
        SelectedOption.value;
    }
  };

  /**
   * Function: NextQuestion
   * Description: Moves to the next question and updates the question display and progress bar.
   */
  static NextQuestion = () => {
    let SelectedOption = document.querySelector("input[name='option']:checked");
    if (!SelectedOption) {
      alert("Please select an option before proceeding.");
      return;
    }
    Questions.SaveUserAnswer();
    if (Questions.CurrentQuestionIndex < Questions.QuestionList.length - 1) {
      Questions.CurrentQuestionIndex++;
      Questions.DisplayQuestion();
      Questions.ProgressBar();
      Questions.DisplayTotalQuestions();
    }
  };

  /**
   * Function: PreviousQuestion
   * Description: Moves to the previous question and updates the question display and progress bar.
   */
  static PreviousQuestion = () => {
    Questions.SaveUserAnswer();
    if (Questions.CurrentQuestionIndex > 0) {
      Questions.CurrentQuestionIndex--;
      Questions.DisplayQuestion();
      Questions.ProgressBar();
      Questions.DisplayTotalQuestions();
    }
  };

  /**
   * Function: SubmitQuiz
   * Description: Submits the quiz and sends the user's score to the server. If the username exists, update the score; otherwise, create a new entry.
   */
  static SubmitQuiz = () => {
    Questions.SaveUserAnswer();
    let Score = Questions.CalculateResult();

    let cookieData = Cookies.GetCookie({
      UserNameKey: "username",
      PasswordKey: "password",
    });

    let { username, password } = cookieData || {};

    // Check if the username exists in the score database
    fetch(
      `https://673596e65995834c8a934a4d.mockapi.io/score?username=${username}`
    )
      .then((response) => response.json())
      .then((data) => {
        if (data.length > 0) {
          // Username exists, update the score
          let existingRecord = data[0];
          let updatedData = {
            username: existingRecord.username,
            score: Score,
          };

          fetch(
            `https://673596e65995834c8a934a4d.mockapi.io/score/${existingRecord.id}`,
            {
              method: "PUT",
              headers: {
                "Content-Type": "application/json",
              },
              body: JSON.stringify(updatedData),
            }
          )
            .then((response) => response.json())
            .then((data) => {
              window.location.href = "leaderboard.html";
            })
            .catch((error) => {
              console.error("Error updating score:", error);
              alert("Failed to update score. Please try again later.");
            });
        } else {
          // Username doesn't exist, create a new score entry
          let newData = {
            username: username,
            score: Score,
          };

          fetch("https://673596e65995834c8a934a4d.mockapi.io/score", {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(newData),
          })
            .then((response) => response.json())
            .then((data) => {
              window.location.href = "leaderboard.html";
            })
            .catch((error) => {
              console.error("Error submitting quiz:", error);
              alert("Failed to submit quiz. Please try again later.");
            });
        }
      })
      .catch((error) => {
        console.error("Error checking user score:", error);
        alert("Failed to check user score. Please try again later.");
      });
  };

  /**
   * Function: CalculateResult
   * Description: Calculates the user's total score by comparing their answers with the correct answers.
   */
  static CalculateResult = () => {
    let Score = 0;
    Questions.QuestionList.forEach((question, index) => {
      if (Questions.UserAnswer[index] === question.correctAnswer) {
        Score++;
      }
    });
    return Score;
  };

  /**
   * Function: EventListeners
   * Description: Sets up event listeners for the navigation buttons.
   */
  static EventListeners = () => {
    $(document).ready(() => {
      $("#next-btn").click(() => {
        Questions.NextQuestion();
      });

      $("#prev-btn").click(() => {
        Questions.PreviousQuestion();
      });

      $("#submit-btn").click(() => {
        if (
          Questions.CurrentQuestionIndex <
          Questions.QuestionList.length - 1
        ) {
          alert("Please answer all questions before submitting.");
          return;
        }
        Questions.SubmitQuiz();
      });
    });
  };

  /**
   * Function: Init
   * Description: Initializes the quiz by fetching questions and setting up event listeners.
   */
  static Init = () => {
    Questions.FetchQuestions();
    Questions.EventListeners();
  };
}

document.addEventListener("DOMContentLoaded", () => {
  Questions.Init();
});
