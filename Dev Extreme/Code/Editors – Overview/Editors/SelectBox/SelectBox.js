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
  console.log("Document is Ready!!");

  // Initialize dxSelectBox widget with various configuration options
  var SelectBoxInstance = $("#SelectBox")
    .dxSelectBox({
      // Allows only predefined values to be selected
      acceptCustomValue: false,

      // Shortcut key for focusing on the widget (Alt+C)
      accessKey: "C",

      // Enables the active state when the widget is focused or clicked
      activeStateEnabled: true,

      // Displays a dropdown button to expand the list
      showDropDownButton: true,

      // Displays a clear button to reset the selection
      showClearButton: true,

      dropDownOptions: {
        // Closes the dropdown when clicking outside
        closeOnOutsideClick: true,
      },

      // DataSource: A list of selectable items
      dataSource: new DevExpress.data.DataSource({
        store: new DevExpress.data.ArrayStore({
          data: [
            { id: 1, name: "Yash", group: "Team A" },
            { id: 2, name: "Maulik", group: "Team A" },
            { id: 3, name: "Keyur", group: "Team B" },
            { id: 4, name: "Arjun", group: "Team B" },
            { id: 5, name: "Nirav", group: "Team C" },
          ],
          key: "id", // Define key field
        }),
        group: "group", // Specify group field
      }),

      // Prevents delayed rendering of the dropdown
      deferRendering: false,

      // The widget is not disabled
      disabled: false,

      // Specifies which field to display as text in the dropdown
      displayExpr: "name",

      // Specifies which field will be used as the actual value
      valueExpr: "id",

      // Default selected value (corresponding to ID 1)
      value: 1,

      elementAttr: {
        // Custom ID for the widget's root element
        id: "select-box-id",

        // Custom class for the widget's root element
        class: "select-box-class",
      },

      // Enables focus state when the widget is focused
      focusStateEnabled: true,

      // Enables hover effect when the widget is hovered
      hoverStateEnabled: true,

      // Disables grouping of items
      grouped: true,

      // Define how group headers should be displayed
      groupTemplate(data) {
        return $("<div>")
          .addClass("custom-group")
          .html(`<strong>${data.key}</strong>`); // Display the group name (Team A, Team B, etc.)
      },

      // Tooltip hint text when hovering over the widget
      hint: "This is a select box",

      // Sets the initial validation state to valid
      isValid: true,

      // ==========================
      // ** Custom Templates **
      // ==========================

      // Customizing the display of the selected value in the input field
      fieldTemplate: (selectedItem) => {
        if (!selectedItem) {
          return $("<div>").dxTextBox({
            value: "Select an item", // Default placeholder text
            readOnly: true, // Prevents user input
          });
        }
        return $("<div>").dxTextBox({
          value: "Selected: " + selectedItem.name, // Display selected item
          readOnly: true,
        });
      },

      // Customizing the display of items inside the dropdown
      itemTemplate: (data) => {
        return $("<div>").html(
          "<i class='dx-icon dx-icon-user'></i> " + data.name
        ); // Add a user icon before each name
      },

      // ==========================
      // ** Event Handlers **
      // ==========================

      // Fires when the value changes
      onChange: changeHandler,

      // Fires when the dropdown closes
      onClosed: closedHandler,

      // Fires when text is copied from the input field
      onCopy: copyHandler,

      // Fires when text is cut from the input field
      onCut: cutHandler,

      // Fires when the dropdown content is ready
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

      // Fires when the dropdown opens
      onOpened: openedHandler,

      // Fires when an option is changed
      onOptionChanged: optionChangedHandler,

      // Fires when the selected value changes
      onValueChanged: valueChangedHandler,

      // Fires when text is pasted into the input field
      onPaste: pasteHandler,
    })
    .dxSelectBox("instance");

  // ==========================
  // ** Methods **
  // ==========================

  // Retrieve and log the dropdown content
  console.log("Content:", content(SelectBoxInstance));

  // Retrieve and log the root element of the widget
  console.log("Element:", element(SelectBoxInstance));

  // Retrieve and log the input field element inside the widget
  console.log("Field:", field(SelectBoxInstance));

  // Modify the placeholder text dynamically
  option(SelectBoxInstance, "placeholder", "Select an item");

  // Open the dropdown programmatically
  open(SelectBoxInstance);

  // Register a custom key handler for the "Enter" key
  registerKeyHandler(SelectBoxInstance, "enter", () =>
    alert("Enter key pressed!")
  );

  // close(SelectBoxInstance);
});
