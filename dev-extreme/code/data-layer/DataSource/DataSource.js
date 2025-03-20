$(document).ready(() => {
  console.log("Document is Ready!!");

  // Initialize DevExtreme DataSource
  var dataSource = new DevExpress.data.DataSource({
    // Data store - Uses ArrayStore to manage local data
    store: new DevExpress.data.ArrayStore({
      data: marvelCharacters,
      key: "ID", // Unique key identifier
    }),

    // Filter - Exclude characters with ID = 1
    filter: ["ID", ">", 1],

    // Sort - Arrange characters in descending order by Name
    sort: { selector: "Name", desc: true },

    // Enable pagination
    paginate: true,

    // Number of records per page
    pageSize: 3,

    // Require total count to fetch full data count
    requireTotalCount: true,

    // Automatically reshapes data when a new record is added
    reshapeOnPush: true,

    // Search configuration
    searchExpr: ["Name"], // Search will be performed on the Name field
    searchOperation: "startswith", // Matches records where Name starts with the given input

    // Event triggered when the data source changes
    onChanged: (e) => {
      console.log("Data source changed:", e);
    },

    // Event triggered when there is an error loading data
    onLoadError: (error) => {
      console.log("Data load error:", error.message);
    },

    // Event triggered when loading status changes
    onLoadingChanged: (isLoading) => {
      console.log("Loading status changed:", isLoading);
    },
  });

  console.log("Initialized DataSource:", dataSource);

  // Initialize DevExtreme SelectBox
  $("#SelectBox").dxSelectBox({
    // Allows users to enter custom values
    acceptCustomValue: true,

    // Enables search functionality in dropdown
    searchEnabled: true,

    // Displays a clear button to reset selection
    showClearButton: true,

    // Assign the data source
    dataSource: dataSource,

    // Field to display in the dropdown
    displayExpr: "Name",

    // Custom button added inside SelectBox
    buttons: [
      {
        name: "filter",
        location: "after", // Position of the button
        options: {
          icon: "dragvertical", // Button icon
          onClick: (e) => {
            var count = dataSource.totalCount(); // Get total record count
            alert("Total Records: " + count);
          },
        },
      },
      "dropDown", // Default dropdown button
    ],
  });

  // Retrieve and log the current filter expression
  var filterExpr = dataSource.filter();
  console.log("Current Filter Expression:", filterExpr);

  // Set a new filter expression
  dataSource.filter(["ID", ">", 1]);
  dataSource.load(); // Reload the data

  // Retrieve and log the current group expression
  var groupExpr = dataSource.group();
  console.log("Current Group Expression:", groupExpr);

  // Check if the last page is reached
  console.log("Is Last Page:", dataSource.isLastPage());

  // Check if the data is loaded
  console.log("Is Data Loaded:", dataSource.isLoaded());

  // Check if data is currently loading
  console.log("Is Loading:", dataSource.isLoading());

  // Retrieve all items from data source
  var dataItems = dataSource.items();
  console.log("Data Items:", dataItems);

  // Get the key property of the data store
  var keyProps = dataSource.key();
  console.log("Key Property:", keyProps);

  // Get and set the page index
  console.log("Current Page Index:", dataSource.pageIndex());
  dataSource.pageIndex(2);
  dataSource.load();

  // Get and set page size
  console.log("Current Page Size:", dataSource.pageSize());
  dataSource.pageSize(3);
  dataSource.load();

  // Enable or disable pagination
  console.log("Is Pagination Enabled:", dataSource.paginate());
  dataSource.paginate(true);
  dataSource.load();

  // Enable or disable total count requirement
  console.log("Require Total Count:", dataSource.requireTotalCount());
  dataSource.requireTotalCount(true);
  dataSource.load();

  // Get and set search expressions
  console.log("Search Expression:", dataSource.searchExpr());
  dataSource.searchExpr("Real_Name");

  // Get and set search operation
  console.log("Search Operation:", dataSource.searchOperation());
  dataSource.searchOperation("contains");

  // Retrieve data store
  var store = dataSource.store();
  console.log("Data Store:", store);

  // Get total number of records
  var itemCount = dataSource.totalCount();
  console.log("Total Item Count:", itemCount);
});
