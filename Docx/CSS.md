# CSS Guide

## 2. CSS

### 2.1 Basics of CSS

#### 2.1.1 CSS Introduction

- **CSS (Cascading Style Sheets)**: A language used for describing the presentation of a document written in HTML or XML. It controls the layout, colors, fonts, and overall visual aesthetics of a webpage.
- **Why Use CSS?**: CSS makes it easier to change the appearance of web pages by separating the content (HTML) from the presentation (CSS). This results in cleaner code and more efficient design processes.
- **Example**:

  ```html
  <!DOCTYPE html>
  <html>
    <head>
      <style>
        body {
          background-color: #f0f8ff;
        }
        h1 {
          color: #333;
          font-size: 24px;
        }
        p {
          color: #666;
          line-height: 1.6;
        }
      </style>
    </head>
    <body>
      <h1>Welcome to CSS</h1>
      <p>
        CSS allows you to style and design web pages beautifully and
        effectively.
      </p>
    </body>
  </html>
  ```

#### 2.1.2 External, Internal, and Inline Style Sheets

- **External Style Sheet**: CSS is written in a separate file (e.g., styles.css) and linked to the HTML document using the `<link>` tag in the `<head>`.

  - **Example**:

    ```html
    <!-- HTML File -->
    <!DOCTYPE html>
    <html>
      <head>
        <link rel="stylesheet" href="styles.css" />
      </head>
      <body>
        <h1>External CSS Example</h1>
        <p>Styles are applied from an external stylesheet.</p>
      </body>
    </html>
    ```

    ```css
    /* styles.css */
    h1 {
      color: navy;
    }
    p {
      font-size: 16px;
    }
    ```

- **Internal Style Sheet**: CSS is written within a `<style>` tag in the `<head>` section of the HTML file.

  - **Example**:

    ```html
    <!DOCTYPE html>
    <html>
      <head>
        <style>
          body {
            background-color: #eee;
          }
          h1 {
            color: #0066cc;
          }
        </style>
      </head>
      <body>
        <h1>Internal CSS Example</h1>
        <p>This example uses an internal stylesheet.</p>
      </body>
    </html>
    ```

- **Inline Style**: CSS is applied directly within an HTML element using the `style` attribute.

  - **Example**:

    ```html
    <h1 style="color: red; font-size: 20px;">Inline CSS Example</h1>
    <p style="background-color: yellow;">This paragraph has inline styling.</p>
    ```

#### 2.1.3 CSS Syntax

- **CSS Syntax**: Consists of a selector and a declaration block.
- **Selector**: Specifies the HTML element to style.
- **Declaration Block**: Contains one or more declarations enclosed in curly braces `{}`. Each declaration includes a CSS property and a value, separated by a colon, and multiple declarations are separated by semicolons.
- **Example**:

  ```css
  p {
    color: blue;
    font-size: 14px;
  }
  ```

- **Explanation**: The `p` selector targets all `<p>` elements, making the text color blue and the font size 14px.

#### 2.1.4 CSS Selectors

- **CSS Selectors**: Used to select and style HTML elements.
- **Element Selector**: Targets all elements of a specified type.
  - **Example**: `p { color: green; }` (styles all `<p>` elements)
- **ID Selector**: Targets a single element with a specific id. Use `#` followed by the ID name.
  - **Example**: `#header { font-size: 24px; }` (styles the element with `id="header"`)
- **Class Selector**: Targets elements with a specific class. Use `.` followed by the class name.
  - **Example**: `.highlight { background-color: yellow; }` (styles all elements with `class="highlight"`)
- **Attribute Selector**: Selects elements based on an attribute value.
  - **Example**: `a[target="_blank"] { color: red; }` (styles links that open in a new tab)
- **Combined Example**:

  ```html
  <style>
    #main-title {
      color: purple;
      text-align: center;
    }
    .content {
      font-size: 18px;
      margin: 10px;
    }
    a[href^="https"] {
      text-decoration: none;
      color: green;
    }
  </style>

  <h1 id="main-title">CSS Selectors Example</h1>
  <p class="content">This is a paragraph with a class selector.</p>
  <a href="https://www.example.com" target="_blank">Secure Link</a>
  ```

#### 2.1.5 CSS Basic Properties

- **Common CSS Properties**:

  - **Color**: Defines the text color.
    - **Example**: `color: red;`
  - **Background**: Sets the background color or image.
    - **Example**: `background-color: lightblue;`
  - **Font**: Controls text appearance, such as font size and font family.
    - **Example**: `font-size: 16px; font-family: Arial, sans-serif;`
  - **Margin & Padding**: Defines space outside (margin) and inside (padding) an element.
    - **Example**: `margin: 20px; padding: 10px;`
  - **Example**:

    ```css
    body {
      font-family: "Helvetica", sans-serif;
      background-color: #fafafa;
    }
    h2 {
      color: #333;
      margin-bottom: 15px;
    }
    .box {
      border: 1px solid #ddd;
      padding: 20px;
      margin: 10px;
    }
    ```

#### 2.1.6 Example

- **Practical Example**:

  ```html
  <!DOCTYPE html>
  <html>
    <head>
      <style>
        body {
          font-family: "Verdana", sans-serif;
          background-color: #e0f7fa;
        }
        .container {
          width: 80%;
          margin: 0 auto;
          padding: 20px;
          background-color: white;
          border-radius: 5px;
          box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        h1 {
          color: #00796b;
          text-align: center;
        }
        p {
          font-size: 16px;
          color: #555;
        }
        .note {
          color: #d32f2f;
          font-weight: bold;
        }
      </style>
    </head>
    <body>
      <div class="container">
        <h1>CSS Example</h1>
        <p>This is a simple demonstration of CSS styling.</p>
        <p class="note">
          Important Note: Pay attention to CSS properties and their effects.
        </p>
      </div>
    </body>
  </html>
  ```

This structured and example-rich content should help you understand CSS concepts more effectively. CSS Guide
