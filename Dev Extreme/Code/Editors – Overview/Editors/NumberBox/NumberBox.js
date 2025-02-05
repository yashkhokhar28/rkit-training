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

import {
  element,
  getButton,
  option,
  registerKeyHandler,
  instance,
} from "../Methods.js";

$(document).ready(() => {
  console.log("Document is Ready!!");

  // Initialize dxNumberBox widget with various configuration options
  var NumberBoxInstance = $("#NumberBox")
    .dxNumberBox({
      // Shortcut key for focusing on the widget (Alt+N)
      accessKey: "N",

      // Enables the active state when the widget is focused
      activeStateEnabled: true,

      // The widget is not disabled
      disabled: false,

      elementAttr: {
        // Custom ID for the widget's root element
        id: "number-box-id",

        // Custom class for the widget's root element
        class: "number-box-class",
      },

      // Enables focus state when the widget is focused
      focusStateEnabled: true,

      // Specifies the input type (fixed point for decimal numbers)
      type: "fixedPoint",

      // Hint text to be shown on hover
      hint: "this is number box",

      // Enables hover state when the widget is hovered over
      hoverStateEnabled: true,

      inputAttr: {
        // Disables autocomplete on the input field
        autocomplete: "off",
      },

      // Error message for invalid input
      invalidValueMessage: "invalid number",

      // Marks the widget as valid initially
      isValid: true,

      // Sets the maximum value the user can enter
      max: 1000000000,

      // Sets the minimum value the user can enter
      min: 1,

      // Input mode for numeric input
      mode: "number",

      // Placeholder text for the input field
      placeholder: "Enter Number....",

      // The widget is not read-only
      readOnly: false,

      // Disables the right-to-left layout
      rtlEnabled: false,

      // Displays a "Clear" button inside the input field
      showClearButton: true,

      // Displays spin buttons for adjusting the value
      showSpinButtons: true,

      // Defines the step for value increment/decrement
      step: 10000,

      // Styling mode for the widget
      stylingMode: "underlined",

      // Use large spin buttons (for better UI on large devices)
      useLargeSpinButtons: true,

      // Validation status of the widget
      validationStatus: "valid",

      // Visibility of the widget
      visible: true,

      // Set default Value
      value: "1",

      // Event handlers
      onChange: changeHandler,
      onCopy: copyHandler,
      onCut: cutHandler,
      onContentReady: contentReadyHandler,
      onDisposing: disposeHandler,
      onEnterKey: enterKeyHandler,
      onFocusIn: focusInHandler,
      onFocusOut: focusOutHandler,
      onInitialized: initializedHandler,
      onInput: inputHandler,
      onKeyDown: keyDownHandler,
      onKeyPress: keyPressHandler,
      onKeyUp: keyUpHandler,
      onOptionChanged: optionChangedHandler,
      onValueChanged: valueChangedHandler,
      onPaste: pasteHandler,
    })
    .dxNumberBox("instance");

  // Access the root HTML element of the NumberBox widget
  element(NumberBoxInstance);

  // Focus on the NumberBox widget
  focus(NumberBoxInstance);

  var button = getButton(NumberBoxInstance, "spinUp");
  console.log(button); // Log the spinUp button object

  // Access the instance of the widget
  instance(NumberBoxInstance);

  // Get and log the "accessKey" option of the NumberBox widget
  console.log(option(NumberBoxInstance, "accessKey"));

  // Register a custom key handler for the "enter" key
  registerKeyHandler(NumberBoxInstance, "enter", () => {
    alert("Enter key pressed!");
  });
});
