// Starts a batch update operation, preventing UI updates until `endUpdate()` is called
const beginUpdate = (widgetInstance) => widgetInstance.beginUpdate();

// Removes focus from the widget
const blur = (widgetInstance) => widgetInstance.blur();

// Closes the widget's dropdown (if applicable)
const close = (widgetInstance) => widgetInstance.close();

// Returns the widget's drop-down content
const content = (widgetInstance) => widgetInstance.content();

// Sets default options for a widget globally
const defaultOptions = (rule) => DevExpress.ui.dxWidget.defaultOptions(rule);

// Disposes of the widget, freeing up memory
const dispose = (widgetInstance) => widgetInstance.dispose();

// Returns the root HTML element of the widget
const element = (widgetInstance) => widgetInstance.element();

// Ends a batch update operation and applies UI updates
const endUpdate = (widgetInstance) => widgetInstance.endUpdate();

// Returns the input field element inside the widget
const field = (widgetInstance) => widgetInstance.field();

// Sets focus to the widget
const focus = (widgetInstance) => widgetInstance.focus();

// Retrieves a button instance inside the widget (e.g., clear, dropdown)
const getButton = (widgetInstance, name) => widgetInstance.getButton(name);

// Returns the data source object of the widget
const getDataSource = (widgetInstance) => widgetInstance.getDataSource();

// Returns the instance of the widget itself
const instance = (widgetInstance) => widgetInstance.instance();

// Unsubscribes an event from the widget
const off = (widgetInstance, eventName, eventHandler = null) =>
  eventHandler
    ? widgetInstance.off(eventName, eventHandler) // Removes a specific event handler
    : widgetInstance.off(eventName); // Removes all handlers for the event

// Subscribes an event to the widget
const on = (widgetInstance, eventNameOrEvents, eventHandler = null) =>
  eventHandler
    ? widgetInstance.on(eventNameOrEvents, eventHandler) // Attaches a specific event handler
    : widgetInstance.on(eventNameOrEvents); // Attaches multiple event handlers

// Opens the widget's dropdown (if applicable)
const open = (widgetInstance) => widgetInstance.open();

// Gets or sets an option for the widget
const option = (widgetInstance, optionNameOrObject, optionValue = null) =>
  optionValue !== null
    ? widgetInstance.option(optionNameOrObject, optionValue) // Sets an option
    : widgetInstance.option(optionNameOrObject); // Gets an option

// Registers a custom key handler for specific key events
const registerKeyHandler = (widgetInstance, key, handler) =>
  widgetInstance.registerKeyHandler(key, handler);

// Forces the widget to repaint (useful for UI updates)
const repaint = (widgetInstance) => widgetInstance.repaint();

// Resets the widget to its default state
const reset = (widgetInstance) => widgetInstance.reset();

// Resets a specific option to its default value
const resetOption = (widgetInstance, optionName) =>
  widgetInstance.resetOption(optionName);

export {
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
  instance,
  off,
  on,
  open,
  option,
  registerKeyHandler,
  repaint,
  reset,
  resetOption,
};
