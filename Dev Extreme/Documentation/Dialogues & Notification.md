### **6 Dialogues & Notification**

#### **6.1 Overview**

- DevExtreme provides various UI components for **dialogs and notifications**, including **Load Indicator, Load Panel, Popup, Popover, and Toast**.
- Supports **custom styling, animation, positioning, and event handling**.

---

#### **6.2 Load Indicator**

- **Properties:**

  - `indicatorSrc` → Custom image for the loading indicator.
  - `visible` → Controls visibility (`true/false`).

- **Methods:**

  - `.show()` → Displays the load indicator.
  - `.hide()` → Hides the load indicator.

- **Example:**
  ```js
  $("#loadIndicator").dxLoadIndicator({
    visible: true,
  });
  ```

---

#### **6.3 Load Panel**

- **Properties:**

  - `message` → Displays a loading message.
  - `showPane` → Shows/hides the background pane.
  - `shading` → Enables background shading.

- **Methods:**

  - `.show()` → Displays the load panel.
  - `.hide()` → Hides the load panel.

- **Example:**
  ```js
  $("#loadPanel").dxLoadPanel({
    message: "Loading...",
    shading: true,
    showPane: true,
  });
  ```

---

#### **6.4 Popup**

- **Properties:**

  - `title` → Sets the popup title.
  - `visible` → Controls visibility (`true/false`).
  - `width` / `height` → Sets dimensions.

- **Methods:**

  - `.show()` → Opens the popup.
  - `.hide()` → Closes the popup.

- **Events:**

  - `onShown` → Fires when the popup is displayed.
  - `onHidden` → Fires when the popup is closed.

- **Example:**
  ```js
  $("#popup").dxPopup({
    title: "Information",
    width: 400,
    height: 300,
    visible: false,
  });
  ```

---

#### **6.5 Popover**

- **Properties:**

  - `target` → Specifies the element the popover is attached to.
  - `position` → Sets the display position (`"top"`, `"bottom"`, `"left"`, `"right"`).
  - `visible` → Controls visibility (`true/false`).

- **Methods:**

  - `.show()` → Displays the popover.
  - `.hide()` → Hides the popover.

- **Example:**
  ```js
  $("#popover").dxPopover({
    target: "#button",
    position: "top",
    contentTemplate: function () {
      return $("<div>").text("Popover Content");
    },
  });
  ```

---

#### **6.6 Toast**

- **Properties:**

  - `message` → Sets the text message.
  - `type` → Defines the notification type (`"info"`, `"success"`, `"error"`, `"warning"`).
  - `displayTime` → Duration before auto-hide (in milliseconds).

- **Methods:**

  - `.show()` → Displays the toast.
  - `.hide()` → Hides the toast.

- **Example:**
  ```js
  $("#toast")
    .dxToast({
      message: "Operation Successful",
      type: "success",
      displayTime: 3000,
    })
    .dxToast("show");
  ```
