// Fires when the adaptive detail row is preparing
const onAdaptiveDetailRowPreparing = (e) =>
  console.log("Adaptive Detail Row Preparing", e);

// Fires when a cell is clicked
const onCellClick = (e) => console.log("Cell Clicked", e);

// Fires when a cell is double-clicked
const onCellDblClick = (e) => console.log("Cell Double Clicked", e);

// Fires when the cell hover state changes
const onCellHoverChanged = (e) => console.log("Cell Hover Changed", e);

// Fires when a cell is prepared
const onCellPrepared = (e) => console.log("Cell Prepared", e);

// Fires when the grid content is ready
const onContentReady = (e) => console.log("Content Ready", e);

// Fires when the context menu is prepared
const onContextMenuPreparing = (e) => console.log("Context Menu Preparing", e);

// Fires when a data error occurs
const onDataErrorOccurred = (e) => console.error("Data Error Occurred", e);

// Fires when the component is disposed
const onDisposing = (e) => console.log("Disposing", e);

// Fires when an edit operation is canceled
const onEditCanceled = (e) => console.log("Edit Canceled", e);

// Fires before an edit operation is canceled
const onEditCanceling = (e) => console.log("Edit Canceling", e);

// Fires when editing starts
const onEditingStart = (e) => console.log("Editing Started", e);

// Fires when an editor is prepared
const onEditorPrepared = (e) => console.log("Editor Prepared", e);

// Fires before an editor is prepared
const onEditorPreparing = (e) => console.log("Editor Preparing", e);

// Fires when exporting is completed
const onExported = (e) => console.log("Export Completed", e);

// Fires before exporting starts
const onExporting = (e) => console.log("Exporting", e);

// Fires before a file is saved
const onFileSaving = (e) => console.log("File Saving", e);

// Fires when the focused cell changes
const onFocusedCellChanged = (e) => console.log("Focused Cell Changed", e);

// Fires before the focused cell changes
const onFocusedCellChanging = (e) => console.log("Focused Cell Changing", e);

// Fires when the focused row changes
const onFocusedRowChanged = (e) => console.log("Focused Row Changed", e);

// Fires before the focused row changes
const onFocusedRowChanging = (e) => console.log("Focused Row Changing", e);

// Fires when the component is initialized
const onInitialized = (e) => console.log("Component Initialized", e);

// Fires when a new row is initialized
const onInitNewRow = (e) => console.log("New Row Initialized", e);

// Fires when a key is pressed down
const onKeyDown = (e) => console.log("Key Down", e);

// Fires when an option is changed
const onOptionChanged = (e) => console.log("Option Changed", e);

// Fires when a row is clicked
const onRowClick = (e) => console.log("Row Clicked", e);

// Fires when a row is collapsed
const onRowCollapsed = (e) => console.log("Row Collapsed", e);

// Fires before a row is collapsed
const onRowCollapsing = (e) => console.log("Row Collapsing", e);

// Fires when a row is double-clicked
const onRowDblClick = (e) => console.log("Row Double Clicked", e);

// Fires when a row is expanded
const onRowExpanded = (e) => console.log("Row Expanded", e);

// Fires before a row is expanded
const onRowExpanding = (e) => console.log("Row Expanding", e);

// Fires when a row is inserted
const onRowInserted = (e) => console.log("Row Inserted", e);

// Fires before a row is inserted
const onRowInserting = (e) => console.log("Row Inserting", e);

// Fires when a row is prepared
const onRowPrepared = (e) => console.log("Row Prepared", e);

// Fires when a row is removed
const onRowRemoved = (e) => console.log("Row Removed", e);

// Fires before a row is removed
const onRowRemoving = (e) => console.log("Row Removing", e);

// Fires when a row is updated
const onRowUpdated = (e) => console.log("Row Updated", e);

// Fires before a row is updated
const onRowUpdating = (e) => console.log("Row Updating", e);

// Fires when row validation occurs
const onRowValidating = (e) => console.log("Row Validating", e);

// Fires when data saving is completed
const onSaved = (e) => console.log("Data Saved", e);

// Fires before data saving starts
const onSaving = (e) => console.log("Saving Data", e);

// Fires when selection changes
const onSelectionChanged = (e) => console.log("Selection Changed", e);

// Fires before the toolbar is prepared
const onToolbarPreparing = (e) => console.log("Toolbar Preparing", e);

export {
  onAdaptiveDetailRowPreparing,
  onCellClick,
  onCellDblClick,
  onCellHoverChanged,
  onCellPrepared,
  onContentReady,
  onContextMenuPreparing,
  onDataErrorOccurred,
  onDisposing,
  onEditCanceled,
  onEditCanceling,
  onEditingStart,
  onEditorPrepared,
  onEditorPreparing,
  onExported,
  onExporting,
  onFileSaving,
  onFocusedCellChanged,
  onFocusedCellChanging,
  onFocusedRowChanged,
  onFocusedRowChanging,
  onInitialized,
  onInitNewRow,
  onKeyDown,
  onOptionChanged,
  onRowClick,
  onRowCollapsed,
  onRowCollapsing,
  onRowDblClick,
  onRowExpanded,
  onRowExpanding,
  onRowInserted,
  onRowInserting,
  onRowPrepared,
  onRowRemoved,
  onRowRemoving,
  onRowUpdated,
  onRowUpdating,
  onRowValidating,
  onSaved,
  onSaving,
  onSelectionChanged,
  onToolbarPreparing,
};
