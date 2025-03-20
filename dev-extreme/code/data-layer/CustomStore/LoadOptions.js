// Document ready event handler using jQuery
$(document).ready(() => {
  // Log confirmation when document is fully loaded
  console.log("Document is Ready");

  // Create CustomStore for data management
  var customStore = new DevExpress.data.CustomStore({
    // Set data loading mode to raw
    loadMode: "raw",

    // Define data loading function
    load: () => {
      // Fetch data from MockAPI endpoint
      return $.getJSON(
        "https://67a9f61d65ab088ea7e526d8.mockapi.io/loadoptions"
      )
        .then((data) => {
          // Log successful API response
          console.log("MockAPI Response:", data);
          return data; // Return data ensuring it has productCategory field
        })
        .fail((error) => {
          // Log error and return empty array on failure
          console.error("Error fetching data:", error);
          return [];
        });
    },
  });

  // Load grouped data for SelectBox1
  customStore
    .load({
      // Group configuration
      group: {
        selector: "productCategory",
        requireGroupCount: true,
      },
    })
    .done((d) => {
      // Log loaded data and set as SelectBox1 datasource
      console.log(d);
      selectBox1.option("dataSource", d);
    });

  // Load filtered data for SelectBox2
  customStore
    .load({
      // Pagination parameters
      skip: 1, // Skip first record
      take: 10, // Take 10 records

      // Search parameters (noted as not working)
      searchExpr: ["productName"], // Search in productName field
      searchOperation: "endswith", // Search for strings ending with value
      searchValue: "product name", // Search term
      requireTotalCount: true, // Request total count of records
    })
    .done((d) => {
      // Set loaded data as SelectBox2 datasource
      selectBox2.option("dataSource", d);
    });

  // Initialize first SelectBox widget
  var selectBox1 = $("#SelectBox1")
    .dxSelectBox({
      // Field to display in dropdown
      displayExpr: "productName",

      // Field to use as value
      valueExpr: "id",

      // Enable grouping
      grouped: true,

      // Custom group header template
      groupTemplate(data) {
        console.log("Group Data:", data);
        return $("<div>")
          .addClass("custom-group")
          .html(`<strong>${data.key}</strong>`);
      },
    })
    // Get widget instance
    .dxSelectBox("instance");

  // Initialize second SelectBox widget
  var selectBox2 = $("#SelectBox2")
    .dxSelectBox({
      // Allow custom user input
      acceptCustomValue: true,

      // Show button to clear selection
      showClearButton: true,

      // Field to display in dropdown
      displayExpr: "productName",

      // Field to use as value
      valueExpr: "id",
    })
    // Get widget instance
    .dxSelectBox("instance");

  // Initialize DataGrid widget
  var dataGrid = $("#DataGrid").dxDataGrid({
    // Set data source to custom store
    dataSource: customStore,

    // Show borders around cells
    showBorders: true,

    // Column definitions
    columns: [
      {
        dataField: "productName",
        caption: "Product Name",
      },
      {
        dataField: "productCategory",
        caption: "Product Category",
        groupIndex: 0, // Enable grouping by this column
      },
      {
        dataField: "productPrice",
        caption: "Product Price",
      },
    ],

    // Group summary configuration
    groupSummary: [
      {
        selector: "productCategory",
        summaryType: "count", // Count items in each group
        alignByColumn: true, // Align summary with column
      },
    ],

    // Summary configuration
    summary: {
      // Group-level summaries
      groupItems: [
        {
          column: "productCategory",
          summaryType: "count",
          displayFormat: "{0} Category", // Format for group count
        },
      ],
      // Total summaries
      totalItems: [
        {
          column: "productName",
          summaryType: "count", // Total count of products
        },
      ],
    },
  });
});
