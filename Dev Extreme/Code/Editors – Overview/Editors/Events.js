// Fires when the value of the widget changes
const changeHandler = (e) => console.log("Changed", e);

// Fires when the dropdown is closed
const closedHandler = (e) => console.log(e.component.NAME + " closed");

// Fires when the widget content is fully loaded and ready
const contentReadyHandler = (e) => console.log("Content is ready");

// Fires when the user copies text from the input field
const copyHandler = () => alert("Copied");

// Fires when the user cuts text from the input field
const cutHandler = () => alert("Cut");

// Fires when the widget is being removed/disposed
const disposeHandler = (e) => alert("Disposing", e);

// Fires when the Enter key is pressed
const enterKeyHandler = () => alert("Value will be selected");

// Fires when the widget gains focus
const focusInHandler = (e) => console.log("Focused in", e.event.type);

// Fires when the widget loses focus
const focusOutHandler = (e) => console.log("Focused out", e.event.type);

// Fires when the widget is initialized
const initializedHandler = (e) => console.log("Initialized");

// Fires when the user types in the input field
const inputHandler = (e) =>
  console.log("Input received", e.event.currentTarget.value);

// Fires when a key is pressed down inside the widget input
const keyDownHandler = (e) =>
  console.log("Key down", e.event.key, e.event.keyCode);

// Fires when a key is released inside the widget input
const keyUpHandler = (e) => console.log("Key up", e.event.key, e.event.keyCode);

// Fires when a key is pressed and released (deprecated in modern browsers)
const keyPressHandler = (e) =>
  console.log("Key press", e.event.key, e.event.keyCode);

// Fires when the dropdown is opened
const openedHandler = (e) => console.log(e.component.NAME + " opened");

// Fires when a widget option is changed dynamically
const optionChangedHandler = (e) =>
  console.log("Option changed", e.name, e.value);

// Fires when the value of the widget is changed (with previous and new value)
const valueChangedHandler = (e) =>
  console.log(`Value changed "${e.previousValue}" to "${e.value}"`);

// Fires when the user pastes text into the input field
const pasteHandler = () => alert("Pasted");

export {
  changeHandler,
  closedHandler,
  contentReadyHandler,
  copyHandler,
  cutHandler,
  disposeHandler,
  enterKeyHandler,
  focusInHandler,
  focusOutHandler,
  initializedHandler,
  inputHandler,
  keyDownHandler,
  keyUpHandler,
  keyPressHandler,
  openedHandler,
  optionChangedHandler,
  valueChangedHandler,
  pasteHandler,
};
