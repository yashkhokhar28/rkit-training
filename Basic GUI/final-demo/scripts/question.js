let QuestionList = [];
let UserAnswer = {};
const FetchQuestions = async () => {
  let response = await fetch(
    "https://gist.githubusercontent.com/yashkhokhar28/ca43e3f93db86ebbb7f9a3c0a8fdf808/raw/question.json"
  );
  QuestionList = await response.json();
  DisplayQuestion();
  DisplayTotalQuestions();
};

let CurrentQuestionIndex = 0;

// Display question based on current index
const DisplayQuestion = () => {
  const CurrentQuestion = QuestionList[CurrentQuestionIndex];
  const QuestionText = document.getElementById("question-text");
  const OptionContainer = document.getElementById("options-container");

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

FetchQuestions();

// Event listeners for navigation buttons
document.getElementById("next-btn").addEventListener("click", () => {
  let SelectedOption = document.querySelector("input[name='option']:checked");
  if (SelectedOption === null) {
    alert("Please select an option before proceeding.");
    return; // Stop execution if no option is selected
  }

  UserAnswer[CurrentQuestionIndex] = SelectedOption.value;

  if (CurrentQuestionIndex < QuestionList.length - 1) {
    CurrentQuestionIndex++;
    DisplayQuestion();
    ProgressBar();
    DisplayTotalQuestions();
  }
});

document.getElementById("prev-btn").addEventListener("click", () => {
  if (CurrentQuestionIndex > 0) {
    CurrentQuestionIndex--;
    DisplayQuestion();
    ProgressBar();
    DisplayTotalQuestions();
  }
});

const ProgressBar = () => {
  const ProgressBar = document.getElementById("progress-bar");
  const ProgressBarContainer = document.getElementById(
    "progress-bar-container"
  );
  const Progress = (CurrentQuestionIndex / QuestionList.length) * 100;
  ProgressBar.style.width = `${Progress}%`;
  ProgressBarContainer.setAttribute("aria-valuenow", Progress);
  ProgressBarContainer.setAttribute("aria-valuemin", 0);
  ProgressBarContainer.setAttribute("aria-valuemax", 100);
};

const DisplayTotalQuestions = () => {
  const TotalQuestions = document.getElementById("total-questions");
  TotalQuestions.innerHTML = `Question ${CurrentQuestionIndex + 1} of ${
    QuestionList.length
  }`;
  TotalQuestions.setAttribute("class", "text-center");
};
