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

import { element, option, registerKeyHandler, instance } from "../Methods.js";

$(document).ready(() => {
  console.log("Document is Ready!!");

  var TextAreaInstance = $("#TextArea")
    .dxTextArea({
      // Shortcut key for focusing on the widget (Alt+T)
      accessKey: "T",

      // Enables the active state when the widget is focused or clicked
      activeStateEnabled: true,

      autoResizeEnabled: true,

      // The widget is not disabled
      disabled: false,

      elementAttr: {
        // Custom ID for the widget's root element
        id: "text-area-id",

        // Custom class for the widget's root element
        class: "text-area-class",
      },

      // Enables focus state when the widget is focused
      focusStateEnabled: true,

      // Enables hover effect when the widget is hovered
      hoverStateEnabled: true,

      // Tooltip hint text when hovering over the widget
      hint: "This is a text area",

      maxHeight: 500,

      maxLength: 1000000,

      minHeight: 100,

      inputAttr: {
        // Disables autocomplete on the input field
        autocomplete: "off",
      },

      // Marks the widget as valid initially
      isValid: true,

      // Placeholder text for the input field
      placeholder: "Enter Text Here....",

      // The widget is not read-only
      readOnly: false,

      spellcheck: true,

      // Disables the right-to-left layout
      rtlEnabled: false,

      // Styling mode for the widget
      stylingMode: "underlined",

      // Validation status of the widget
      validationStatus: "valid",

      // Visibility of the widget
      visible: true,

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
    .dxTextArea("instance");

  // ==========================
  // ** Methods **
  // ==========================

  // Access the root HTML element of the TextArea widget (not NumberBox)
  element(TextAreaInstance);

  // Access the instance of the widget
  instance(TextAreaInstance);

  // Get and log the "accessKey" option of the TextArea widget (not NumberBox)
  console.log(option(TextAreaInstance, "accessKey"));

  // Register a custom key handler for the "Enter" key
  registerKeyHandler(TextAreaInstance, "enter", () => {
    alert("Enter key pressed!");
  });
});
