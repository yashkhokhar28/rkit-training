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

  var TextBoxInstance = $("#TextBox")
    .dxTextBox({
      // Shortcut key for focusing on the widget (Alt+X)
      accessKey: "X",

      // Enables the active state when the widget is focused or clicked
      activeStateEnabled: true,

      // The widget is not disabled
      disabled: false,

      elementAttr: {
        // Custom ID for the widget's root element
        id: "text-box-id",

        // Custom class for the widget's root element
        class: "text-box-class",
      },

      // Enables focus state when the widget is focused
      focusStateEnabled: true,

      // Enables hover effect when the widget is hovered
      hoverStateEnabled: true,

      // Tooltip hint text when hovering over the widget
      hint: "This is a text box",

      maxLength: 1000, // More practical value for maxLength

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
      stylingMode: "filled",

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
    .dxTextBox("instance");

  // ==========================
  // ** Methods **
  // ==========================

  // Access the root HTML element of the TextBox widget
  element(TextBoxInstance);

  // Access the instance of the widget
  instance(TextBoxInstance);

  // Register a custom key handler for the "Enter" key
  registerKeyHandler(TextBoxInstance, "enter", () => {
    alert("Enter key pressed!");
  });

  // Phone number mask
  $("#PhoneNumberTextBox").dxTextBox({
    mask: "(+91) 9999-9999-99",
    maskChar: "_",
    hint: "Enter your phone number",
    useMaskedValue: true,
  });

  // Date mask
  $("#DateInputTextBox").dxTextBox({
    mask: "99/99/9999",
    maskChar: "_",
    hint: "Enter date (MM/DD/YYYY)",
    showMaskMode: "always",
    useMaskedValue: true,
  });

  // Credit card mask
  $("#CreditCardTextBox").dxTextBox({
    mask: "9999 9999 9999 9999",
    maskChar: "_",
    hint: "Enter your credit card number",
    showMaskMode: "always",
    useMaskedValue: true,
  });

  // Currency mask
  $("#CurrencyTextBox").dxTextBox({
    mask: "$999,999.99",
    maskChar: "_",
    hint: "Enter amount",
    showMaskMode: "always",
    useMaskedValue: true,
  });

  // Time mask
  $("#TimeInputTextBox").dxTextBox({
    mask: "99:99",
    maskChar: "_",
    hint: "Enter time (HH:MM)",
    showMaskMode: "always",
    useMaskedValue: true,
  });

  const passwordEditor = $("#password")
    .dxTextBox({
      placeholder: "password",
      mode: "password",
      inputAttr: { "aria-label": "Password" },
      stylingMode: "filled",
      buttons: [
        {
          name: "password",
          location: "after",
          options: {
            icon: "eyeopen",
            stylingMode: "text",
            onClick() {
              passwordEditor.option(
                "mode",
                passwordEditor.option("mode") === "text" ? "password" : "text"
              );
            },
          },
        },
      ],
    })
    .dxTextBox("instance");
});
