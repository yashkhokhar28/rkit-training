import {
  contentReadyHandler,
  disposeHandler,
  initializedHandler,
  optionChangedHandler,
  valueChangedHandler,
} from "../Events.js";

$(document).ready(() => {
  console.log("Document is Ready!!");

  $("#RadioGroup").dxRadioGroup({
    // Shortcut key for focusing on the widget (Alt+R)
    accessKey: "R",

    // Enables the active state when the widget is focused or clicked
    activeStateEnabled: true,

    // Data source for radio group options
    dataSource: [
      { id: 1, gender: "Male" },
      { id: 2, gender: "Female" },
    ],

    // Specifies which property is displayed in the UI
    displayExpr: "gender",

    // Specifies which property holds the actual value
    valueExpr: "id",

    // Sets the default selected value (Male)
    value: 1,

    // The widget is not disabled
    disabled: false,

    elementAttr: {
      // Custom ID for the widget's root element
      id: "radio-group-id",

      // Custom class for the widget's root element
      class: "radio-group-class",
    },

    // Enables focus state when the widget is focused
    focusStateEnabled: true,

    // Enables hover effect when the widget is hovered
    hoverStateEnabled: true,

    // Tooltip hint text when hovering over the widget
    hint: "This is a radio group",

    // Defines the layout of radio buttons (horizontal/vertical)
    layout: "horizontal",

    // Marks the widget as valid initially
    isValid: true,

    // Disables the right-to-left layout
    rtlEnabled: false,

    // Validation status of the widget
    validationStatus: "valid",

    // Always show validation messages
    validationMessageMode: "always",

    // Visibility of the widget
    visible: true,

    // ==========================
    // ** Event Handlers **
    // ==========================

    // Fires when the widget content is ready
    onContentReady: contentReadyHandler,

    // Fires when the widget is disposed of
    onDisposing: disposeHandler,

    // Fires when the widget is initialized
    onInitialized: initializedHandler,

    // Fires when an option is changed
    onOptionChanged: optionChangedHandler,

    // Fires when the value is changed by the user
    onValueChanged: valueChangedHandler,
  });
});
