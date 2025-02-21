import {
  changeHandler,
  copyHandler,
  cutHandler,
  contentReadyHandler,
  disposeHandler,
  enterKeyHandler,
  focusInHandler,
  focusOutHandler,
  initializedHandler,
  inputHandler,
  keyDownHandler,
  keyUpHandler,
  keyPressHandler,
  optionChangedHandler,
  valueChangedHandler,
  pasteHandler,
} from "../Events.js";

// Import utility methods from Methods.js
import { element, option, registerKeyHandler, instance } from "../Methods.js";

// Document ready event handler using jQuery
$(document).ready(() => {
  // Log confirmation when document is fully loaded
  console.log("Document is Ready!!");

  // Initialize TextArea widget with detailed configuration
  var TextAreaInstance = $("#TextArea")
    .dxTextArea({
      // Shortcut key for focusing (Alt+T)
      accessKey: "T",

      // Enable active state styling when focused/clicked
      activeStateEnabled: true,

      // Enable automatic resizing based on content
      autoResizeEnabled: true,

      // Set widget enabled state
      disabled: false,

      // Custom attributes for the textarea element
      elementAttr: {
        // Custom element ID
        id: "text-area-id",

        // Custom CSS class
        class: "text-area-class",
      },

      // Enable focus state styling
      focusStateEnabled: true,

      // Enable hover state styling
      hoverStateEnabled: true,

      // Tooltip text on hover
      hint: "This is a text area",

      // Maximum height in pixels
      maxHeight: 500,

      // Maximum character length
      maxLength: 1000000,

      // Minimum height in pixels
      minHeight: 100,

      // Attributes for the underlying input element
      inputAttr: {
        // Disable browser autocomplete
        autocomplete: "off",
      },

      // Initial validation state
      isValid: true,

      // Placeholder text when empty
      placeholder: "Enter Text Here....",

      // Allow text editing
      readOnly: false,

      // Enable browser spell checking
      spellCheck: true,

      // Disable right-to-left layout
      rtlEnabled: false,

      // Set visual styling mode
      stylingMode: "underlined",

      // Initial validation status
      validationStatus: "valid",

      // Control widget visibility
      visible: true,

      // ==========================
      // Event Handlers Section
      // ==========================

      // Handle value change events
      onChange: changeHandler,

      // Handle copy operations
      onCopy: copyHandler,

      // Handle cut operations
      onCut: cutHandler,

      // Handle content ready state
      onContentReady: contentReadyHandler,

      // Handle widget disposal
      onDisposing: disposeHandler,

      // Handle Enter key press
      onEnterKey: enterKeyHandler,

      // Handle focus gained
      onFocusIn: focusInHandler,

      // Handle focus lost
      onFocusOut: focusOutHandler,

      // Handle widget initialization
      onInitialized: initializedHandler,

      // Handle user input
      onInput: inputHandler,

      // Handle key down events
      onKeyDown: keyDownHandler,

      // Handle key press events
      onKeyPress: keyPressHandler,

      // Handle key up events
      onKeyUp: keyUpHandler,

      // Handle option changes
      onOptionChanged: optionChangedHandler,

      // Handle value changes by user
      onValueChanged: valueChangedHandler,

      // Handle paste operations
      onPaste: pasteHandler,
    })
    // Get the widget instance
    .dxTextArea("instance");

  // ==========================
  // Widget Methods Section
  // ==========================

  // Get the root HTML element of the TextArea
  element(TextAreaInstance);

  // Get the widget instance (redundant but included for completeness)
  instance(TextAreaInstance);

  // Retrieve and log the accessKey option value
  console.log(option(TextAreaInstance, "accessKey"));

  // Register custom Enter key handler
  registerKeyHandler(TextAreaInstance, "enter", () => {
    alert("Enter key pressed!");
  });
});
