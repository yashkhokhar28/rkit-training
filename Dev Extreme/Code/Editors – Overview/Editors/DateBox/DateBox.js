import {
  changeHandler,
  closedHandler,
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
  openedHandler,
  optionChangedHandler,
  valueChangedHandler,
  pasteHandler,
} from "../Events.js";

import {
  content,
  element,
  field,
  open,
  option,
  registerKeyHandler,
} from "../Methods.js";

$(document).ready(() => {
  console.log("Document is Ready");

  // ==========================
  // ** Initialize Time Picker **
  // ==========================

  $("#Time").dxDateBox({
    // Specifies the type of input (time)
    type: "time",

    // Picker type set to "rollers" for time selection
    pickerType: "rollers",

    // Placeholder text displayed in the input field
    placeholder: "Select Time",

    // Displays a "Clear" button to reset the input field
    showClearButton: true,
  });

  // ==========================
  // ** Initialize Date Picker **
  // ==========================

  $("#Date").dxDateBox({
    // Specifies the type of input (date)
    type: "date",

    // Picker type set to "calendar" for date selection
    pickerType: "calendar",

    // Placeholder text displayed in the input field
    placeholder: "Select Date",

    // Displays a "Clear" button to reset the input field
    showClearButton: true,
  });

  // ==========================
  // ** Initialize DateTime Picker **
  // ==========================

  var DateBoxInstance = $("#DateTime")
    .dxDateBox({
      // ==========================
      // ** Properties **
      // ==========================

      // Specifies the type of input (datetime)
      type: "datetime",

      // Allows users to enter custom text instead of selecting a date
      acceptCustomValue: true,

      // Shortcut key (Alt + D) to focus on the DateBox
      accessKey: "D",

      // Forces a calendar popup instead of the native date picker
      pickerType: "calendar",

      // Placeholder text displayed in the input field
      placeholder: "Select a date",

      // Enables styling for active (clicked) state
      activeStateEnabled: true,

      // Enables adaptive rendering for small screens
      adaptivityEnabled: true,

      // Custom text for the Apply button in the calendar popup
      applyButtonText: "Confirm",

      // Defines when the selected date is applied
      // "useButtons" requires clicking the Apply button
      applyValueMode: "useButtons",

      // Customizes the calendar behavior
      calendarOptions: {
        firstDayOfWeek: 1, // Sets Monday as the first day of the week
      },

      // Custom text for the Cancel button
      cancelButtonText: "Close",

      // Defines the minimum and maximum selectable dates
      min: new Date(2024, 0, 1), // January 1, 2024
      max: new Date(2027, 11, 31), // December 31, 2027

      // Custom error message when the selected date is out of range
      dateOutOfRangeMessage: "Date must be within 2027",

      // Specifies the format in which the date value is stored
      dateSerializationFormat: "yyyy-MM-ddTHH:mm:ssZ",

      // Prevents deferred rendering of the calendar
      deferRendering: true,

      // Enables or disables the DateBox
      disabled: false,

      // Disables selection of specific dates (e.g., February 14, 2025)
      disabledDates: [new Date(2025, 1, 14)],

      // Customizes the drop-down button with an event icon
      dropDownButtonTemplate: function () {
        return $("<span>").addClass("dx-icon dx-icon-event");
      },

      // Custom options for the drop-down calendar
      dropDownOptions: {
        // width: 400,
      },

      // Adds a custom attribute to the DateBox container
      elementAttr: { "data-test": "custom-attribute" },

      // Enables styling for the focused state
      focusStateEnabled: true,

      // Enables hover effects on the DateBox
      hoverStateEnabled: true,

      // Custom attributes for the input field (for accessibility)
      inputAttr: { "aria-label": "Date of Birth" },

      // Custom error message for invalid date format
      invalidDateMessage: "Please enter a valid date",

      // Displays a "Clear" button to reset the input field
      showClearButton: true,

      // Specifies the styling mode ("outlined", "underlined", or "filled")
      stylingMode: "underlined",

      // Sets the validation status of the DateBox ("valid", "invalid", "pending")
      validationStatus: "valid",

      // Controls the visibility of the DateBox
      visible: true,

      // Enables input masking for proper formatting
      useMaskBehavior: true,

      // Marks the DateBox as valid
      isValid: true,

      // Restricts input length
      maxLength: 10,

      // Used in form submission
      name: "userBirthday",

      // Sets tab index for keyboard navigation
      tabIndex: 2,

      // Enables right-to-left layout (false by default)
      rtlEnabled: false,

      // Custom validation error message
      validationError: { message: "Invalid date format!" },

      // Defines when value change event is triggered
      valueChangeEvent: "blur",

      // Opens the calendar dropdown by default
      opened: true,

      // Prevents the calendar from opening on field click
      openOnFieldClick: false,

      // ==========================
      // ** Event Handlers **
      // ==========================

      // Fires when the value changes
      onValueChanged: (e) => {
        console.log("Current text:", e.component.option("text"));
        let message = e.value
          ? "Selected Date: " + e.value
          : "Date Not Selected";
        let type = e.value ? "success" : "error";
        DevExpress.ui.notify(message, type, 2000);
      },

      // ==========================
      // ** Event Handlers **
      // ==========================

      // Fires when the value changes
      onChange: changeHandler,

      // Fires when the dropdown is closed
      onClosed: closedHandler,

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

      // Fires when the dropdown is opened
      onOpened: openedHandler,

      // Fires when an option is changed
      onOptionChanged: optionChangedHandler,

      // Fires when the value is changed by the user
      onValueChanged: valueChangedHandler,

      // Fires when text is pasted into the input field
      onPaste: pasteHandler,
    })
    .dxDateBox("instance");

  // ==========================
  // ** Methods **
  // ==========================

  // Retrieve and log the dropdown content
  console.log(content(DateBoxInstance));

  // Retrieve and log the root element of the widget
  console.log(element(DateBoxInstance));

  // Retrieve and log the input field element inside the widget
  console.log(field(DateBoxInstance));

  // Modify the placeholder text dynamically
  option(DateBoxInstance, "placeholder", "Pick a date");

  // Open the dropdown programmatically
  open(DateBoxInstance);

  // Register a custom key handler for the "Enter" key
  registerKeyHandler(DateBoxInstance, "enter", () => {
    alert("Enter key pressed!");
  });
});
