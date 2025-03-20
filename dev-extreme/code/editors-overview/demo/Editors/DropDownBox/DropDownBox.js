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
  // ** Data Source Function **
  // ==========================

  // Creates a data source from a JSON file
  const makeAsyncDataSource = (jsonFile) => {
    return new DevExpress.data.CustomStore({
      loadMode: "raw", // Loads raw JSON data
      key: "ID", // Unique key for identifying records
      load: () => $.getJSON(jsonFile), // Fetches data from the specified JSON file
    });
  };

  // ==========================
  // ** DropDownBox Setup **
  // ==========================

  var DropDownInstance = $("#DropDownBox")
    .dxDropDownBox({
      // Data source for the dropdown
      dataSource: makeAsyncDataSource("data.json"),

      // Specifies the field storing the selected value
      valueExpr: "ID",

      // Specifies the field displayed in the dropdown list
      displayExpr: "CompanyName",

      // Disables manual text input, allowing only selection from the list
      acceptCustomValue: false,

      // Shortcut key (Alt + A) to focus on the DropDownBox
      accessKey: "A",

      // Disables active state styling when the component is clicked
      activeStateEnabled: false,

      // Placeholder text displayed in the input field
      placeholder: "Select a value...",

      // Custom formatting for the displayed selected value
      displayValueFormatter: (value) => `Selected Value: ${value}`,

      // Custom dropdown button template (replaces the default button with ▼)
      dropDownButtonTemplate: () => $("<span>").text("▼"),

      // Drop-down options configuration
      dropDownOptions: {
        closeOnOutsideClick: true, // Closes the dropdown when clicking outside
      },

      // Enables opening the dropdown when clicking on the input field
      openOnFieldClick: true,

      // Sets the initial state of the dropdown (true = open by default)
      opened: false,

      // Custom template for rendering the dropdown content
      contentTemplate: function (e) {
        const $list = $("<div>").dxList({
          // Enables search functionality inside the dropdown
          searchEnabled: true,

          // Search placeholder text
          searchEditorOptions: {
            placeholder: "Enter Company Name",
            showClearButton: true, // Enables clear button in search input
            inputAttr: { "aria-label": "Search" }, // Accessibility attribute
          },

          // Specifies the field to be searched
          searchExpr: ["CompanyName"],

          // Binds data to the List component inside the dropdown
          dataSource: e.component.getDataSource(),

          // Enables selection controls (checkboxes)
          showSelectionControls: true,

          // Allows only a single item to be selected
          selectionMode: "single",

          // Customizes how each item in the dropdown list is displayed
          itemTemplate: function (itemData) {
            return $("<div>").text(itemData.CompanyName);
          },

          // Handles item selection
          onItemClick: function (args) {
            const selectedItems = args.itemData;
            e.component.option("value", selectedItems.ID); // Sets the selected value
            e.component.close(); // Closes the dropdown after selection
          },
        });

        return $list;
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
    .dxDropDownBox("instance");

  // ==========================
  // ** Methods **
  // ==========================

  // ==========================
  // ** Methods **
  // ==========================

  // Retrieve and log the dropdown content
  console.log("Content:", content(DropDownInstance));

  // Retrieve and log the root element of the widget
  console.log("Element:", element(DropDownInstance));

  // Retrieve and log the input field element inside the widget
  console.log("Field:", field(DropDownInstance));

  // Modify the placeholder text dynamically
  option(DropDownInstance, "placeholder", "Select an item");

  // Open the dropdown programmatically
  open(DropDownInstance);

  // Register a custom key handler for the "Enter" key
  registerKeyHandler(DropDownInstance, "enter", () =>
    alert("Enter key pressed!")
  );

  // close(DropDownInstance);
});
