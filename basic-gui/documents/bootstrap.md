# Bootstrap Guide

## 3. Bootstrap

### 3.1 Basics of Bootstrap

#### 3.1.1 Introduction to Bootstrap

- **Bootstrap**: A powerful front-end framework for building responsive and mobile-first web applications. It includes pre-designed components and a grid system that makes development faster and easier.
- **History**: Originally developed by Twitter and released as open-source in 2011. It has evolved to become one of the most popular frameworks for building websites.
- **Current Version**: As of now, Bootstrap 5 is the latest stable version.

---

#### 3.1.2 Use of Bootstrap

- **Purpose**: Bootstrap simplifies the process of designing complex and responsive layouts with minimal custom CSS.
- **Benefits**:
  - **Responsive Design**: Built-in support for a grid system and media queries.
  - **Pre-built Components**: Ready-to-use components like modals, carousels, and navigation bars.
  - **Customizable**: Easily customize Bootstrap using Sass variables and mixins.
- **Example**:

  ```html
  <!DOCTYPE html>
  <html>
    <head>
      <link
        href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"
        rel="stylesheet"
      />
    </head>
    <body>
      <div class="container">
        <h1 class="text-primary">Hello, Bootstrap!</h1>
        <p class="lead">This is a simple Bootstrap example.</p>
        <button class="btn btn-success">Click Me!</button>
      </div>
      <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    </body>
  </html>
  ```

#### 3.1.3 Structure of Bootstrap

- **Basic Structure**: Bootstrap uses a grid system and various classes to style elements. To use Bootstrap, include the CSS and JavaScript files in your project.
- **HTML Template with Bootstrap**:

  ```html
  <!DOCTYPE html>
  <html lang="en">
    <head>
      <meta charset="UTF-8" />
      <meta name="viewport" content="width=device-width, initial-scale=1.0" />
      <title>Bootstrap Template</title>
      <link
        href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"
        rel="stylesheet"
      />
    </head>
    <body>
      <div class="container">
        <h1>Welcome to Bootstrap</h1>
        <p>This is a basic structure using Bootstrap.</p>
      </div>
      <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    </body>
  </html>
  ```

### 3.2 Bootstrap Grid System

- **Grid System**: A responsive layout system based on a 12-column layout. Elements can span multiple columns by specifying classes like `col-`, `col-sm-`, `col-md-`, etc.
- **Example**:

  ```html
  <div class="container">
    <div class="row">
      <div class="col-md-4">Column 1</div>
      <div class="col-md-4">Column 2</div>
      <div class="col-md-4">Column 3</div>
    </div>
  </div>
  ```

- **Explanation**: The `.row` class creates a horizontal group of columns, and `.col-md-4` defines each column to take up 4 out of 12 parts of the row, making them evenly distributed.

### 3.3 Bootstrap Components

- **Alerts**: Provides predefined alert messages.

  ```html
  <div class="alert alert-success" role="alert">
    This is a success alert—check it out!
  </div>
  ```

- **Buttons**: Different button styles using classes like `.btn`, `.btn-primary`, `.btn-danger`.

  ```html
  <button class="btn btn-primary">Primary Button</button>
  <button class="btn btn-danger">Danger Button</button>
  ```

- **Navigation Bar**: Easy-to-create responsive navigation bars.

  ```html
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <div class="container-fluid">
      <a class="navbar-brand" href="#">Navbar</a>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarNav"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item">
            <a class="nav-link active" aria-current="page" href="#">Home</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#">Features</a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
  ```

### 3.4 Bootstrap Utilities

- **Spacing Utilities**: Quickly add margin or padding using classes like `m-3` (margin) or `p-3` (padding).
- **Example**:

  ```html
  <div class="m-3 p-3 bg-light">Content with margin and padding</div>
  ```

- **Text Utilities**: Easily change text alignment, color, and wrapping.
- **Example**:

  ```html
  <p class="text-center text-danger">Centered red text</p>
  ```

### 3.5 Bootstrap Forms

- **Form Controls**: Use classes to style input elements.

  ```html
  <form>
    <div class="mb-3">
      <label for="email" class="form-label">Email address</label>
      <input
        type="email"
        class="form-control"
        id="email"
        placeholder="name@example.com"
      />
    </div>
    <div class="mb-3">
      <label for="message" class="form-label">Message</label>
      <textarea class="form-control" id="message" rows="3"></textarea>
    </div>
    <button type="submit" class="btn btn-primary">Submit</button>
  </form>
  ```

### 3.6 Bootstrap Cards

- **Cards**: A flexible content container with multiple options for headers, footers, and body content.

  ```html
  <div class="card" style="width: 18rem;">
    <img src="https://via.placeholder.com/150" class="card-img-top" alt="..." />
    <div class="card-body">
      <h5 class="card-title">Card Title</h5>
      <p class="card-text">
        Some quick example text to build on the card title.
      </p>
      <a href="#" class="btn btn-primary">Go somewhere</a>
    </div>
  </div>
  ```

### 3.7 Bootstrap Modal

- **Modals**: Used to display content in a popup dialog.

  ```html
  <button
    type="button"
    class="btn btn-primary"
    data-bs-toggle="modal"
    data-bs-target="#exampleModal"
  >
    Launch Modal
  </button>

  <div
    class="modal fade"
    id="exampleModal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">This is the modal content.</div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            Close
          </button>
          <button type="button" class="btn btn-primary">Save changes</button>
        </div>
      </div>
    </div>
  </div>
  ```

### 3.8 Responsive Design

- **Breakpoints**: Bootstrap uses a set of predefined breakpoints to create responsive designs. The common breakpoints are:
  - Extra Small (xs): `<576px`
  - Small (sm): `≥576px`
  - Medium (md): `≥768px`
  - Large (lg): `≥992px`
  - Extra Large (xl): `≥1200px`
- **Example**:

  ```html
  <div class="d-none d-md-block">Visible on medium and larger screens only</div>
  ```

### 3.9 Additional Components

- **Carousel**: For creating image sliders.

  ```html
  <div id="carouselExample" class="carousel slide" data-bs-ride="carousel">
    <div class="carousel-inner">
      <div class="carousel-item active">
        <img
          src="https://via.placeholder.com/800x400"
          class="d-block w-100"
          alt="..."
        />
      </div>
      <div class="carousel-item">
        <img
          src="https://via.placeholder.com/800x400"
          class="d-block w-100"
          alt="..."
        />
      </div>
    </div>
    <button
      class="carousel-control-prev"
      type="button"
      data-bs-target="#carouselExample"
      data-bs-slide="prev"
    >
      <span class="carousel-control-prev-icon" aria-hidden="true"></span>
      <span class="visually-hidden">Previous</span>
    </button>
    <button
      class="carousel-control-next"
      type="button"
      data-bs-target="#carouselExample"
      data-bs-slide="next"
    >
      <span class="carousel-control-next-icon" aria-hidden="true"></span>
      <span class="visually-hidden">Next</span>
    </button>
  </div>
  ```
