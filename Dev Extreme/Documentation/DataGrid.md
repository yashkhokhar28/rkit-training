# **DataGrid**

### **4.1 Data Source**

- **4.1.1 Simple Array** – Uses a local JavaScript array as the data source.

  - **Property**: `dataSource` – Binds the grid to an array.
  - **Method**: `refresh()` – Reloads the data.
  - **Event**: `onContentReady` – Triggered when the grid content is ready.

- **4.1.2 Ajax Request** – Fetches data from an API or server using AJAX.
  - **Property**: `remoteOperations` – Enables server-side data handling.
  - **Method**: `reload()` – Reloads data from the server.
  - **Event**: `onDataErrorOccurred` – Triggered if an error occurs while loading data.

---

### **4.2 Paging and Scrolling**

- **4.2.1 Record Paging** – Divides records into pages with a set page size.

  - **Property**: `paging.enabled` – Enables or disables paging.
  - **Method**: `pageIndex()` – Gets or sets the current page index.

- **4.2.2 Virtual Scrolling** – Loads only visible records for performance.

  - **Property**: `scrolling.mode` – Sets scrolling mode (`"virtual"` for virtual scrolling).
  - **Method**: `refresh()` – Reloads the data and refreshes the UI.

- **4.2.3 Infinite Scrolling** – Dynamically loads more records as you scroll.
  - **Property**: `scrolling.mode` – Set to `"infinite"` for infinite scrolling.
  - **Method**: `load()` – Manually loads the next set of records.

---

### **4.3 Editing**

- **4.3.1 Row Editing & Events** – Edits entire rows with save/cancel actions.

  - **Property**: `editing.mode` – Defines editing mode (`"row"` for row editing).
  - **Method**: `editRow(rowIndex)` – Puts a specific row into edit mode.
  - **Event**: `onRowUpdating` – Triggered before a row update.

- **4.3.2 Batch Editing** – Allows multiple edits before saving changes.

  - **Property**: `editing.mode` – Set to `"batch"` for batch editing.
  - **Method**: `saveEditData()` – Saves all changes.
  - **Event**: `onSaving` – Triggered before changes are saved.

- **4.3.3 Cell Editing** – Edits individual cells without affecting the row.

  - **Property**: `editing.mode` – Set to `"cell"` for cell editing.
  - **Method**: `editCell(rowIndex, dataField)` – Puts a specific cell into edit mode.
  - **Event**: `onCellPrepared` – Triggered after a cell is prepared.

- **4.3.4 Form Editing** – Opens a form to edit row data.

  - **Property**: `editing.mode` – Set to `"form"` for form-based editing.
  - **Method**: `editRow(rowIndex)` – Opens the form for editing.

- **4.3.5 Popup Editing** – Edits rows in a modal popup.

  - **Property**: `editing.mode` – Set to `"popup"` for popup editing.
  - **Method**: `editRow(rowIndex)` – Opens a popup for editing.
  - **Event**: `onEditorPreparing` – Triggered before the popup editor is created.

- **4.3.6 Data Validation** – Ensures input follows rules (e.g., required, max length).

  - **Property**: `columns.validationRules` – Defines validation rules for a column.
  - **Method**: `validate()` – Validates all fields.
  - **Event**: `onRowValidating` – Triggered before row validation.

- **4.3.7 Cascading Lookups** – Dynamically filters lookup fields based on selection.
  - **Property**: `lookup.dataSource` – Defines the lookup data source.
  - **Event**: `onEditorPreparing` – Adjusts lookup options dynamically.

---

### **4.4 Grouping**

- **4.4.1 Record Grouping** – Groups records based on a field.
  - **Property**: `grouping.autoExpandAll` – Expands or collapses all groups.
  - **Method**: `expandAll()` – Expands all groups.
  - **Event**: `onRowPrepared` – Triggered when a group row is prepared.

---

### **4.5 Filtering**

- **4.5.1 Filtering** – Allows users to filter records dynamically.

  - **Property**: `filterRow.visible` – Enables or disables filtering.
  - **Method**: `clearFilter()` – Clears the applied filter.
  - **Event**: `onEditorPreparing` – Customizes filter options.

- **4.5.2 Filter Panel** – Displays applied filters in a panel.
  - **Property**: `filterPanel.visible` – Shows or hides the filter panel.
  - **Method**: `applyFilter()` – Applies the current filter settings.
  - **Event**: `onOptionChanged` – Triggered when filter settings change.

---

### **4.6 Sorting**

- **4.6.1 Multiple Sorting** – Sorts records by multiple columns.
  - **Property**: `sorting.mode` – Set to `"multiple"` to allow multi-column sorting.
  - **Method**: `columnOption(column, "sortOrder", "asc")` – Sets sorting for a column.
  - **Event**: `onOptionChanged` – Triggered when sorting options change.

---

### **4.7 Selection**

- **4.7.1 Row Selection** – Allows selection of single rows.

  - **Property**: `selection.mode` – Set to `"single"` for single row selection.
  - **Method**: `selectRows(keys, true)` – Selects specific rows by key.
  - **Event**: `onSelectionChanged` – Triggered when selection changes.

- **4.7.2 Multiple Record Selection Modes** – Enables selecting multiple rows.
  - **Property**: `selection.mode` – Set to `"multiple"` for multiple selection.
  - **Method**: `clearSelection()` – Clears all selected rows.
  - **Event**: `onSelectionChanged` – Triggered when multiple selections change.

---
