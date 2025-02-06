import {
  contentReadyHandler,
  disposeHandler,
  initializedHandler,
  optionChangedHandler,
} from "../Events.js";

import { element, option, registerKeyHandler, instance } from "../Methods.js";

$(document).ready(() => {
  console.log("Document is Ready!!");

  var normalButton = $("#normalButton")
    .dxButton({
      stylingMode: "contained",
      text: "filled",
      type: "normal",
      onClick: () => {
        DevExpress.ui.notify("this is normal button", "warning", 2000);
      },
      // Shortcut key for focusing on the widget (Alt+B)
      accessKey: "B",

      // Enables the active state when the widget is focused or clicked
      activeStateEnabled: true,

      elementAttr: {
        // Custom ID for the widget's root element
        id: "button-id",

        // Custom class for the widget's root element
        class: "button-class",
      },

      // Enables focus state when the widget is focused
      focusStateEnabled: true,

      // Enables hover effect when the widget is hovered
      hoverStateEnabled: true,

      // Tooltip hint text when hovering over the widget
      hint: "This is button",

      icon: "favorites",

      // Disables the right-to-left layout
      rtlEnabled: false,

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
    })
    .dxButton("instance");
  // ==========================
  // ** Methods **
  // ==========================

  // Get and log the "accessKey" option of the TextArea widget (not NumberBox)
  console.log(option(normalButton, "accessKey"));

  // Register a custom key handler for the "Enter" key
  registerKeyHandler(normalButton, "enter", () => {
    alert("Enter key pressed!");
  });

  var successButton = $("#successButton")
    .dxButton({
      stylingMode: "outlined",
      text: "Outlined",
      type: "success",
      onClick: () => {
        DevExpress.ui.notify("this is success button", "success", 2000);
      },
    })
    .dxButton("instance");

  var defaultButton = $("#defaultButton")
    .dxButton({
      stylingMode: "contained",
      text: "filled",
      type: "default",
      onClick: () => {
        DevExpress.ui.notify("this is default button", "info", 2000);
      },
    })
    .dxButton("instance");

  var dangerButton = $("#dangerButton")
    .dxButton({
      stylingMode: "outlined",
      text: "outlined",
      type: "danger",
      onClick: () => {
        DevExpress.ui.notify("this is danger button", "error", 2000);
      },
    })
    .dxButton("instance");
});
