// Import event handler functions from Events.js
import {
  contentReadyHandler,
  disposeHandler,
  initializedHandler,
  optionChangedHandler,
} from "../Events.js";

// Import utility methods from Methods.js
import { element, option, registerKeyHandler, instance } from "../Methods.js";

// Document ready event handler using jQuery
$(document).ready(() => {
  // Log confirmation when document is fully loaded
  console.log("Document is Ready!!");

  // Initialize normal button widget with detailed configuration
  var normalButton = $("#normalButton")
    .dxButton({
      // Set button styling mode
      stylingMode: "contained",

      // Button display text
      text: "filled",

      // Button type/style
      type: "normal",

      // Click event handler showing a warning notification
      onClick: () => {
        DevExpress.ui.notify("this is normal button", "warning", 2000);
      },

      // Shortcut key for focusing (Alt+B)
      accessKey: "B",

      // Enable active state styling when focused/clicked
      activeStateEnabled: true,

      // Custom attributes for the button element
      elementAttr: {
        // Custom element ID
        id: "button-id",

        // Custom CSS class
        class: "button-class",
      },

      // Enable focus state styling
      focusStateEnabled: true,

      // Enable hover state styling
      hoverStateEnabled: true,

      // Tooltip text on hover
      hint: "This is button",

      // Button icon
      icon: "favorites",

      // Disable right-to-left layout
      rtlEnabled: false,

      // ==========================
      // Event Handlers Section
      // ==========================

      // Handle content ready event
      onContentReady: contentReadyHandler,

      // Handle widget disposal
      onDisposing: disposeHandler,

      // Handle widget initialization
      onInitialized: initializedHandler,

      // Handle option changes
      onOptionChanged: optionChangedHandler,
    })
    // Get the widget instance
    .dxButton("instance");

  // ==========================
  // Widget Methods Section
  // ==========================

  // Retrieve and log the accessKey option value
  console.log(option(normalButton, "accessKey"));

  // Register custom Enter key handler for the button
  registerKeyHandler(normalButton, "enter", () => {
    alert("Enter key pressed!");
  });

  // Initialize success button widget
  var successButton = $("#successButton")
    .dxButton({
      // Set outlined styling mode
      stylingMode: "outlined",

      // Button text
      text: "Outlined",

      // Success button type
      type: "success",

      // Click handler showing success notification
      onClick: () => {
        DevExpress.ui.notify("this is success button", "success", 2000);
      },
    })
    // Get the widget instance
    .dxButton("instance");

  // Initialize default button widget
  var defaultButton = $("#defaultButton")
    .dxButton({
      // Set contained styling mode
      stylingMode: "contained",

      // Button text
      text: "filled",

      // Default button type
      type: "default",

      // Click handler showing info notification
      onClick: () => {
        DevExpress.ui.notify("this is default button", "info", 2000);
      },
    })
    // Get the widget instance
    .dxButton("instance");

  // Initialize danger button widget
  var dangerButton = $("#dangerButton")
    .dxButton({
      // Set outlined styling mode
      stylingMode: "outlined",

      // Button text
      text: "outlined",

      // Danger button type
      type: "danger",

      // Click handler showing error notification
      onClick: () => {
        DevExpress.ui.notify("this is danger button", "error", 2000);
      },
    })
    // Get the widget instance
    .dxButton("instance");
});
