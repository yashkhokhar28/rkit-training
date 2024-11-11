# JavaScript Guide

## 4. JavaScript

### 4.1 Basics of JavaScript

#### 4.1.1 JavaScript Introduction

- **JavaScript**: A versatile, high-level, interpreted programming language used primarily for adding interactivity to web pages.
- **Features**:
  - **Client-Side Scripting**: Runs directly in the browser, enhancing user experience.
  - **Object-Based**: Provides built-in objects like `window` and `document`.
- **Example**:

  ```html
  <script>
    alert("Welcome to JavaScript!");
  </script>
  ```

- **Usage**: All modern browsers support JavaScript, making it essential for web development.

#### 4.1.2 Use of JavaScript

- **Enhance Interactivity**: Create dynamic content such as form validation, animations, and event handling.
- **Example**: Show an alert when a button is clicked.

  ```html
  <button onclick="showMessage()">Click Me</button>

  <script>
    function showMessage() {
      alert("Button clicked!");
    }
  </script>
  ```

#### 4.1.3 Ways to Include JavaScript

- **Inline JavaScript**: Directly in the HTML tag.

  ```html
  <button onclick="alert('Hello!')">Click Me</button>
  ```

- **Internal JavaScript**: Inside a `<script>` tag within the HTML document.

  ```html
  <script>
    console.log("Internal JavaScript example");
  </script>
  ```

- **External JavaScript**: In a separate file with a .js extension.
- **Example**: `script.js`

  ```javascript
  console.log("External JavaScript example");
  ```

- **Include in HTML**:

  ```html
  <script src="script.js"></script>
  ```

#### 4.1.4 Syntax of JavaScript

- **Statements**: Instructions like `let x = 5;`.
- **Variables**: Using `let`, `const`, or `var` to store data.

  ```javascript
  let name = "John";
  const age = 30;
  var isStudent = false;
  ```

- **Comments**:
  - Single-line: `// This is a comment`
  - Multi-line: `/* This is a multi-line comment */`

#### 4.1.5 Basic Events in JavaScript

- **Events**: Actions that occur in the browser, like clicks, mouse movement, or form submission.
- **Common Event Handlers**:

  - `onclick`: Triggers when an element is clicked.

    ```html
    <button onclick="alert('Clicked!')">Click Me</button>
    ```

  - `onmouseover`: Triggers when the mouse is over an element.

    ```html
    <div onmouseover="console.log('Mouse is over!')">Hover over me</div>
    ```

  - `onload`: Fires when the page has fully loaded.

    ```html
    <body onload="console.log('Page loaded!')"></body>
    ```

#### 4.1.6 Basic Validation with JavaScript

- **Form Validation**: Used to ensure that user input meets specific criteria before submitting.
- **Example**: Check if an input field is empty.

  ```html
  <form onsubmit="return validateForm()">
    <label for="name">Name:</label>
    <input type="text" id="name" name="name" />
    <input type="submit" value="Submit" />
  </form>

  <script>
    function validateForm() {
      let name = document.getElementById("name").value;
      if (name === "") {
        alert("Name must be filled out");
        return false;
      }
      return true;
    }
  </script>
  ```

- **Explanation**: If the input field name is empty, an alert appears, and the form is not submitted. JavaScript Guide
