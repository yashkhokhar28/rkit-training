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
  beginUpdate,
  blur,
  close,
  content,
  defaultOptions,
  dispose,
  element,
  endUpdate,
  field,
  focus,
  getButton,
  getDataSource,
  getInstance,
  instance,
  off,
  on,
  open,
  option,
  registerKeyHandler,
  repaint,
  reset,
  resetOption,
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

      onChange: changeHandler,
      onClosed: closedHandler,
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
      onOpened: openedHandler,
      onOptionChanged: optionChangedHandler,
      onValueChanged: valueChangedHandler,
      onPaste: pasteHandler,
    })
    .dxDropDownBox("instance");

  // ==========================
  // ** Methods **
  // ==========================

  beginUpdate(DropDownInstance);
  console.log("Content:", content(DropDownInstance));
  console.log("Element:", element(DropDownInstance));
  console.log("Field:", field(DropDownInstance));
  option(DropDownInstance, "placeholder", "Select an item");
  repaint(DropDownInstance);
  open(DropDownInstance);
  registerKeyHandler(DropDownInstance, "enter", () =>
    alert("Enter key pressed!")
  );
  // close(DropDownInstance);
  endUpdate(DropDownInstance);
});
