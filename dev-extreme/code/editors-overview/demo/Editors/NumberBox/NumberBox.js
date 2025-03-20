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

  // ==========================
  // ** Initialize dxNumberBox Widget **
  // ==========================

  var NumberBoxInstance = $("#NumberBox")
    .dxNumberBox({
      // Shortcut key for focusing on the widget (Alt+N)
      accessKey: "N",

      // Enables the active state when the widget is focused or clicked
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

      // Enables hover effect when the widget is hovered
      hoverStateEnabled: true,

      // Specifies the input type (fixed point for decimal numbers)
      type: "fixedPoint",

      // Tooltip hint text when hovering over the widget
      hint: "this is number box",

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

      // Set default value
      value: "1",

      // ==========================
      // ** Event Handlers **
      // ==========================

      // Fires when the value changes
      onChange: changeHandler,

      // Fires when text is copied from the input field
      onCopy: copyHandler,

      // Fires when text is cut from the input field
      onCut: cutHandler,

      // Fires when the widget content is ready
      onContentReady: contentReadyHandler,

      // Fires when the widget is disposed of
      onDisposing: disposeHandler,

      // Fires when the user presses the Enter key
      onEnterKey: enterKeyHandler,

      // Fires when the input field gains focus
      onFocusIn: focusInHandler,

      // Fires when the input field loses focus
      onFocusOut: focusOutHandler,

      // Fires when the widget is initialized
      onInitialized: initializedHandler,

      // Fires when the user types into the input field
      onInput: inputHandler,

      // Fires when a key is pressed down
      onKeyDown: keyDownHandler,

      // Fires when a key is pressed
      onKeyPress: keyPressHandler,

      // Fires when a key is released
      onKeyUp: keyUpHandler,

      // Fires when an option is changed
      onOptionChanged: optionChangedHandler,

      // Fires when the value is changed by the user
      onValueChanged: valueChangedHandler,

      // Fires when text is pasted into the input field
      onPaste: pasteHandler,
    })
    .dxNumberBox("instance");

  // ==========================
  // ** Methods **
  // ==========================

  // Access the root HTML element of the NumberBox widget
  element(NumberBoxInstance);

  // Access the instance of the widget
  instance(NumberBoxInstance);

  // Retrieve and log the spin-up button inside the widget
  var button = getButton(NumberBoxInstance, "spinUp");
  console.log(button);

  // Get and log the "accessKey" option of the NumberBox widget
  console.log(option(NumberBoxInstance, "accessKey"));

  // Register a custom key handler for the "Enter" key
  registerKeyHandler(NumberBoxInstance, "enter", () => {
    alert("Enter key pressed!");
  });
});
