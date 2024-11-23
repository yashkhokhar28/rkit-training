/**
 * Class: Questions
 * Description: Handles quiz-related functionalities, including fetching questions,
 * displaying them, saving user answers, updating progress, and submitting the quiz.
 */
import { Cookies } from "./Cookies.js";
import { Auth } from "./Auth.js";

export class Questions {
  static QuestionList = [];
  static UserAnswer = {};
  static CurrentQuestionIndex = 0;

  /**
   * Function: FetchQuestions
   * Description: Fetches quiz questions from the API, displays a loading spinner,
   * and initializes the quiz once the data is retrieved.
   * Called in: Init() function of this class.
   * Parameters: None.
   * Returns: None.
   */
  static FetchQuestions = async () => {
    const Spinner = document.querySelector(".spinner-border");
    const QuestionContainer = document.getElementById("question-container");

    if (Spinner) Spinner.style.display = "block";
    if (QuestionContainer) QuestionContainer.style.display = "none";

    try {
      let Response = await fetch(
        "https://673596e65995834c8a934a4d.mockapi.io/questions"
      );
      Questions.QuestionList = await Response.json();

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
   * Description: Displays the current question and its options based on the current index.
   * Called in: FetchQuestions(), NextQuestion(), PreviousQuestion() functions of this class.
   * Parameters: None.
   * Returns: None.
   */
  static DisplayQuestion = () => {
    const CurrentQuestion =
      Questions.QuestionList[Questions.CurrentQuestionIndex];
    const QuestionText = document.getElementById("question-text");
    const OptionContainer = document.getElementById("options-container");

    if (!CurrentQuestion || !QuestionText || !OptionContainer) return;

    QuestionText.innerHTML = `Q${Questions.CurrentQuestionIndex + 1}: ${
      CurrentQuestion.question
    }`;
    OptionContainer.innerHTML = "";

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
   * Description: Updates the progress bar to reflect the user's progress in the quiz.
   * Called in: NextQuestion(), PreviousQuestion() functions of this class.
   * Parameters: None.
   * Returns: None.
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
   * Description: Updates the DOM to display the current question number and total number of questions.
   * Called in: FetchQuestions(), NextQuestion(), PreviousQuestion() functions of this class.
   * Parameters: None.
   * Returns: None.
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
   * Description: Saves the user's selected answer for the current question.
   * Called in: NextQuestion(), PreviousQuestion(), SubmitQuiz() functions of this class.
   * Parameters: None.
   * Returns: None.
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
   * Description: Advances to the next question if the user has selected an answer.
   * Updates the question display, progress bar, and total questions display.
   * Called in: EventListeners() function of this class.
   * Parameters: None.
   * Returns: None.
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
   * Description: Returns to the previous question, updating the display and progress bar.
   * Called in: EventListeners() function of this class.
   * Parameters: None.
   * Returns: None.
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
   * Description: Submits the user's answers, calculates the score, and updates the database.
   * Called in: EventListeners() function of this class.
   * Parameters: None.
   * Returns: None.
   */
  static SubmitQuiz = () => {
    Questions.SaveUserAnswer();
    let Score = Questions.CalculateResult();

    let cookieData = Cookies.GetCookie({
      UserNameKey: "username",
      PasswordKey: "password",
    });

    let { username, password } = cookieData || {};

    fetch(
      `https://673596e65995834c8a934a4d.mockapi.io/score?username=${username}`
    )
      .then((response) => response.json())
      .then((data) => {
        if (data.length > 0) {
          let existingRecord = data[0];
          let updatedData = {
            username: existingRecord.username,
            score: Score,
          };

          fetch(
            `https://673596e65995834c8a934a4d.mockapi.io/score/${existingRecord.id}`,
            {
              method: "PUT",
              headers: { "Content-Type": "application/json" },
              body: JSON.stringify(updatedData),
            }
          )
            .then(() => (window.location.href = "leaderboard.html"))
            .catch((error) => console.error("Error updating score:", error));
        } else {
          fetch("https://673596e65995834c8a934a4d.mockapi.io/score", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, score: Score }),
          })
            .then(() => (window.location.href = "leaderboard.html"))
            .catch((error) => console.error("Error submitting quiz:", error));
        }
      })
      .catch((error) => console.error("Error checking user score:", error));
  };

  /**
   * Function: CalculateResult
   * Description: Compares the user's answers with correct answers to calculate the total score.
   * Called in: SubmitQuiz() function of this class.
   * Parameters: None.
   * Returns: (number) The user's total score.
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
   * Description: Adds event listeners for the quiz navigation buttons (Next, Previous, Submit).
   * Called in: Init() function of this class.
   * Parameters: None.
   * Returns: None.
   */
  static EventListeners = () => {
    $(document).ready(() => {
      $("#next-btn").click(() => Questions.NextQuestion());
      $("#prev-btn").click(() => Questions.PreviousQuestion());
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
