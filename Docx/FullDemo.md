# Flow of Function Calls in Quiz Application

This document outlines the flow of events and function calls in the quiz application script.

## Flow of Function Calls

1. **DOMContentLoaded Event**

   - Triggered when the DOM is fully loaded.
   - Calls `FetchQuestions()` to initiate the process of fetching quiz questions.

2. **FetchQuestions Function**

   - Displays a loading spinner to indicate that the data is being fetched.
   - Fetches questions asynchronously from the provided URL.
   - Updates the `QuestionList` with the fetched questions.
   - Hides the spinner and displays the question container when data is loaded.
   - Calls `DisplayQuestion()` to display the first question.
   - Calls `DisplayTotalQuestions()` to show the total number of questions and the current question number.

3. **Next Button Event Listener**

   - Waits for the user to click the "Next" button.
   - Checks if an option has been selected:
     - If not, alerts the user to select an option.
     - If selected, saves the user's answer in `UserAnswer`.
   - If there are more questions:
     - Increments `CurrentQuestionIndex`.
     - Calls `DisplayQuestion()` to show the next question.
     - Calls `ProgressBar()` to update the progress bar.
     - Calls `DisplayTotalQuestions()` to update the question count.
   - If it is the last question:
     - Calls `DisplayResult()` to show the quiz results.

4. **Previous Button Event Listener**

   - Waits for the user to click the "Previous" button.
   - If `CurrentQuestionIndex` is greater than 0:
     - Decrements `CurrentQuestionIndex`.
     - Calls `DisplayQuestion()` to show the previous question.
     - Calls `ProgressBar()` to update the progress bar.
     - Calls `DisplayTotalQuestions()` to update the question count.

5. **DisplayQuestion Function**

   - Retrieves the current question from `QuestionList`.
   - Updates the DOM with the question text and dynamically creates radio buttons for the options.

6. **ProgressBar Function**

   - Updates the width of the progress bar to reflect the user's progress.
   - Updates ARIA attributes for accessibility.

7. **DisplayTotalQuestions Function**

   - Updates the DOM to display the current question number and total questions.

8. **DisplayResult Function**

   - Calls `CalculateResult()` to compute the user's score.
   - Updates the DOM to display the user's score, total number of questions, and the percentage score.
   - Includes a button to redirect the user back to the home page.

9. **CalculateResult Function**
   - Iterates through the `QuestionList` and compares each answer with the user's answers in `UserAnswer`.
   - Calculates and returns the total score. Flow of Function Calls in Quiz Application
