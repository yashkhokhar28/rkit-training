# HTML Guide

## 1. HTML

### 1.1 Basics of HTML

#### 1.1.1 What is HTML, Use of HTML, and Different Web Browsers

- **HTML (HyperText Markup Language)**: A standard language for creating web pages. It uses a series of tags to structure content, such as headings, paragraphs, images, and links. HTML serves as the skeleton of a web page.

- **Use of HTML**: It defines the layout and content of a web page, such as placing text, images, tables, and interactive forms.

- **Different Web Browsers**: HTML content is displayed on various browsers like Chrome, Firefox, Safari, and Edge. These browsers interpret the HTML to render the webpage for the user.

- **Example**:

```html
<!DOCTYPE html>
<html>
  <head>
    <title>Sample Page</title>
  </head>
  <body>
    <h1>Welcome to HTML!</h1>
    <p>This is a paragraph.</p>
  </body>
</html>
```

**1.1.2 HTML Version**

• HTML has evolved over time, with major versions like **HTML 4.01**, **XHTML**, and **HTML5**.

• **HTML5** (current version) introduced new features for better multimedia support, semantic elements like `<article>`, `<section>`, and improved APIs.

• **Example**:

```html
<section>
  <h2>HTML5 Section</h2>
  <p>HTML5 introduced new semantic tags for better content structure.</p>
</section>
```

**1.1.3 Structure of HTML**

• **Structure**: The basic structure includes `<!DOCTYPE html>`, `<html>`, `<head>`, and `<body>` tags.

• **Example**:

```html
<!DOCTYPE html>
<html>
  <head>
    <meta charset="UTF-8" />
    <title>Page Structure</title>
  </head>
  <body>
    <h1>Main Content</h1>
  </body>
</html>
```

**1.2 Basic Controls**

**1.2.1 Form**

• **Form**: Used to collect user input. Attributes include method (HTTP method: GET or POST) and action (URL to submit data).

• **Example**:

```html
<form method="post" action="submit.php">
  <label for="name">Name:</label>

  <input type="text" id="name" name="name" />
</form>
```

**1.2.2 Input**

• **Input**: Collects user data, with types like text, password, email, number, etc.

• **Example**:

```html
<input type="text" name="username" placeholder="Enter your name" />
```

**1.2.3 Text Area**

• **Text Area**: Multiline text input field.

• **Example**:

```html
<textarea name="message" rows="5" cols="30">Enter your message here</textarea>
```

**1.2.4 Select Box**

• **Select Box**: Drop-down list to choose options.

• **Example**:

```html
<select name="cars">
  <option value="volvo">Volvo</option>

  <option value="bmw">BMW</option>
</select>
```

**1.2.5 Checkbox**

• **Checkbox**: Select one or more options.

• **Example**:

```html
<input type="checkbox" id="agree" name="terms" />

<label for="agree">I agree to the terms and conditions</label>
```

**1.2.6 Radio Button**

• **Radio Button**: Choose one option from a set.

• **Example**:

```html
<input type="radio" name="gender" value="male" /> Male

<input type="radio" name="gender" value="female" /> Female
```

**1.2.7 Button**

• **Button**: Triggers an action when clicked.

• **Example**:

```html
<button type="button" onclick="alert('Hello!')">Click Me!</button>
```

**1.2.8 Submit Input**

• **Submit Input**: Submits the form data.

• **Example**:

```html
<input type="submit" value="Submit" />
```

**1.2.9 File Control with Attributes**

• **File Input**: Used for file uploads. Attributes like accept specify file types.

• **Example**:

```html
<input type="file" name="upload" accept=".jpg, .png, .pdf" />
```

**1.3 Control’s Attributes**

**1.3.1 Name**

• **Name Attribute**: Specifies the name of an input element, used in form data submission.

• **Example**:

```html
<input type="text" name="username" />
```

**1.3.2 ID**

• **ID Attribute**: Unique identifier used for JavaScript and CSS targeting.

• **Example**:

```html
<input type="text" id="userId" />
```

**1.3.3 Value**

• **Value Attribute**: Defines the default value for an input element.

• **Example**:

```html
<input type="text" value="Default Text" />
```

**1.3.4 Class**

• **Class Attribute**: Defines a class name for styling with CSS.

• **Example**:

```html
<div class="container">Content here</div>
```

**1.4 Basic Tags with Attributes**

**1.4.1 Image (img) Tag and Anchor (a) Tag**

• **img Tag**: Embeds an image. Attributes: src (source), alt (text if image fails to load), width, height.

• **Example**:

```html
<img src="image.jpg" alt="Sample Image" width="500" height="300" />
```

• **a Tag**: Creates a hyperlink. Attributes: href (destination URL), target (specifies where to open the link).

• **Example**:

```html
<a href="https://www.example.com" target="_blank">Visit Example</a>
```

**1.4.2 What is a Meta Tag, Use of Meta Tag**

• **Meta Tag**: Provides metadata about the HTML document, used for SEO, character set, and viewport settings.

• **Example**:

```html
<meta charset="UTF-8" />

<meta name="description" content="Free tutorials on web development" />
```

**1.4.3 What is a Responsive Website, How to Make It Responsive**

• **Responsive Website**: Adapts layout to different screen sizes using techniques like flexible grids, images, and CSS media queries.

• **How to Make It Responsive**: Use the <meta name="viewport"> tag and CSS media queries.

• **Example**:

```html
<meta name="viewport" content="width=device-width, initial-scale=1.0" />

<style>
  @media (max-width: 600px) {
    body {
      background-color: lightblue;
    }
  }
</style>
```
