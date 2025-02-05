### **Editors – Overview**

---

## **Check Box (`dxCheckBox`)**

### **Properties**

- `value` → Boolean (`true`, `false`, `null` for indeterminate).
- `text` → String (Label text for the checkbox).
- `disabled` → Boolean (Enables/disables the checkbox).
- `readOnly` → Boolean (Prevents user interaction).
- `elementAttr` → Object (Custom attributes for the root element).

### **Methods**

- `.option("value", true/false)` → Sets the checkbox value.
- `.toggle()` → Toggles between checked and unchecked states.
- `.focus()` → Sets focus on the checkbox.
- `.dispose()` → Destroys the widget.

### **Events**

- `onValueChanged` → Fires when the checkbox state changes.
- `onFocusIn` → Fires when the checkbox gains focus.
- `onFocusOut` → Fires when the checkbox loses focus.

#### **Example**

```js
$("#checkBox").dxCheckBox({
  text: "Accept Terms",
  value: false,
  onValueChanged: function (e) {
    console.log("Checkbox state: " + e.value);
  },
});
```

---

## **Date Box (`dxDateBox`)**

### **Properties**

- `value` → Date/String (Selected date).
- `type` → String (`"date"`, `"datetime"`, `"time"`, `"month"`, `"year"`).
- `min` / `max` → Date/String (Defines valid date range).
- `displayFormat` → String (Formats date display, e.g., `"MM/dd/yyyy"`).
- `calendarOptions` → Object (Customizes the popup calendar).

### **Methods**

- `.option("value", new Date())` → Sets a new date value.
- `.open()` / `.close()` → Opens/closes the date picker manually.
- `.reset()` → Resets the value to default.

### **Events**

- `onValueChanged` → Fires when the selected date changes.
- `onOpened` / `onClosed` → Fires when the date picker opens/closes.
- `onKeyDown` → Captures key events inside the widget.

#### **Example**

```js
$("#dateBox").dxDateBox({
  type: "date",
  displayFormat: "MM/dd/yyyy",
  min: "2023-01-01",
  max: "2025-12-31",
  onValueChanged: function (e) {
    console.log("Selected Date: " + e.value);
  },
});
```

---

## **Drop Down Box (`dxDropDownBox`)**

### **Properties**

- `value` → Any (Selected item value).
- `items` → Array (Dropdown item list).
- `placeholder` → String (Placeholder text).
- `displayExpr` / `valueExpr` → String (Defines how values are displayed and stored).
- `searchEnabled` → Boolean (Enables search inside the dropdown).
- `opened` → Boolean (Defines whether the dropdown is open).

### **Methods**

- `.open()` / `.close()` → Opens or closes the dropdown manually.
- `.option("value", newValue)` → Changes the selected item.
- `.reset()` → Resets the dropdown selection.

### **Events**

- `onValueChanged` → Fires when the selected item changes.
- `onOpened` / `onClosed` → Fires when the dropdown opens/closes.
- `onSelectionChanged` → Fires when a different item is selected.

#### **Example**

```js
$("#dropDownBox").dxDropDownBox({
  items: ["Apple", "Banana", "Cherry"],
  placeholder: "Select a fruit",
  searchEnabled: true,
  onValueChanged: function (e) {
    console.log("Selected Item: " + e.value);
  },
});
```
