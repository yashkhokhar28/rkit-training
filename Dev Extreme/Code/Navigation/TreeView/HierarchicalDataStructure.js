$(() => {
  $("#tree").dxTreeView({
    // Assign an access key for keyboard shortcuts
    accessKey: "T",

    // Enable active state for better UI feedback
    activeStateEnabled: true,

    // Enable animation for expanding/collapsing nodes
    animationEnabled: true,

    // Function to dynamically load child nodes
    createChildren: (parent) => parent.items,

    // Data source for the tree view
    dataSource: peripheralData,

    // Defines how the data is structured: "tree" or "plain"
    dataStructure: "tree",

    // Disable the tree view if set to true
    disabled: false,

    // Expression to determine which items are disabled
    disabledExpr: "disabled",

    // Property that represents display text for nodes
    displayExpr: "name",

    // Custom attributes for the tree container
    elementAttr: { class: "custom-tree" },

    // Enables "Expand All" button
    expandAllEnabled: true,

    // Defines which items should be expanded by default
    expandedExpr: "expanded",

    // Event that triggers expansion (e.g., "click" or "dblclick")
    expandEvent: "dblclick",

    // Determines if nodes expand recursively
    expandNodesRecursive: false,

    // Enables keyboard navigation and focus state
    focusStateEnabled: true,

    // Expression to check if an item has child nodes
    hasItemsExpr: "hasItems",

    // Sets the height of the tree
    // height: 400,

    // Tooltip text for the tree view
    hint: "Select a peripheral",

    // Enables hover effect on nodes
    hoverStateEnabled: true,

    // Time in milliseconds before an itemHold event triggers
    itemHoldTimeout: 750,

    // The actual data items to be rendered
    items: peripheralData,

    // Expression defining the child items property
    itemsExpr: "items",

    // Custom template for rendering each item
    itemTemplate: (itemData) =>
      `<i class='${itemData.icon}'></i> ${itemData.name}`,

    // Unique identifier for each node
    keyExpr: "id",

    // Text to display when no data is available
    noDataText: "No peripherals available",

    // Event triggered when the tree content is ready
    onContentReady: () => console.log("Tree Ready"),

    // Event triggered when the tree is being disposed
    onDisposing: () => console.log("Tree Disposed"),

    // Event triggered when the tree is initialized
    onInitialized: () => console.log("Tree Initialized"),

    // Event triggered when an item is clicked
    onItemClick: (e) =>
      DevExpress.ui.notify(`${e.itemData.name} clicked!`, "info", 2000),

    // Event triggered when an item collapses
    onItemCollapsed: (e) => console.log(`Collapsed: ${e.itemData.name}`),

    // Event triggered when right-clicking on an item
    onItemContextMenu: (e) => console.log(`Context menu: ${e.itemData.name}`),

    // Event triggered when an item expands
    onItemExpanded: (e) => console.log(`Expanded: ${e.itemData.name}`),

    // Event triggered when an item is held (long press)
    onItemHold: (e) => console.log(`Hold on: ${e.itemData.name}`),

    // Event triggered when an item is rendered
    onItemRendered: (e) => console.log(`Rendered: ${e.itemData.name}`),

    // Event triggered when an item's selection state changes
    onItemSelectionChanged: (e) =>
      console.log(`Selection changed: ${e.itemData.name}`),

    // Event triggered when the "Select All" checkbox is changed
    onSelectAllValueChanged: (e) =>
      console.log(`Select All changed: ${e.value}`),

    // Event triggered when the selection state of items changes
    onSelectionChanged: (e) => console.log("Selection Changed"),

    // Expression defining the parent ID field (for hierarchical structures)
    parentIdExpr: "parentId",

    // Root value for hierarchical data
    rootValue: "root",

    // Enables right-to-left text direction
    rtlEnabled: false,

    // Defines scrolling behavior: "horizontal" or "vertical"
    scrollDirection: "vertical",

    // Custom settings for the search input
    searchEditorOptions: { placeholder: "Search..." },

    // Enables search functionality in the tree view
    searchEnabled: true,

    // Specifies which field is used for searching
    searchExpr: "name",

    // Defines search behavior: "contains", "startswith", "equals"
    searchMode: "contains",

    // Delay before the search is executed (in milliseconds)
    searchTimeout: 500,

    // Initial search value (empty by default)
    searchValue: "",

    // Text displayed for the "Select All" option
    selectAllText: "Select All",

    // Allows selection of items by clicking on them
    selectByClick: true,

    // Expression defining selected items
    selectedExpr: "selected",

    // Defines how selection works: "single", "multiple", "none"
    selectionMode: "multiple",

    // Determines if child nodes should be selected when a parent is selected
    selectNodesRecursive: true,

    // Controls visibility of checkboxes: "none", "normal", "selectAll"
    showCheckBoxesMode: "normal",

    // Tab index for keyboard navigation
    tabIndex: 0,

    // Enables native scrolling behavior
    useNativeScrolling: false,

    // Enables virtual scrolling for large datasets
    virtualModeEnabled: false,

    // Controls the visibility of the tree view
    visible: true,

    // Sets the width of the tree view
    // width: 300,
  });
});
