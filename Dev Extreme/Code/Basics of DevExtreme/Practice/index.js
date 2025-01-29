$(function () {
  // Initialize Button
  $("#buttonContainer").dxButton({
    text: "Submit",
    type: "success",
    icon: "check",
    disabled: false,
    stylingMode: "contained",
    width: 200,
    height: 50,
    hint: "Click to submit the form",
    elementAttr: { "data-test": "submit-btn" },
    useSubmitBehavior: true,
    onClick: function () {
      showPopUp(); // Call the popup function when button is clicked
    },
  });

  // Initialize DataGrid
  $("#gridContainer").dxDataGrid({
    dataSource: [
      { Name: "John", Age: 30 },
      { Name: "Jane", Age: 25 },
    ],
    columns: ["Name", "Age"],
  });

  // Initialize DateBox
  $("#dateBox").dxDateBox({
    type: "date",
    value: new Date(),
    min: new Date(2020, 1, 1),
    max: new Date(2025, 11, 31), // Month is zero-based (11 = December)
  });

  // Initialize Popup (hidden by default)
  $("#popup").dxPopup({
    visible: false, // Initially hidden
    title: "Welcome",
    contentTemplate: function () {
      return $("<div>").text("This is a popup!");
    },
  });

  // Get DataGrid Instance and Refresh
  var gridInstance = $("#gridContainer").dxDataGrid("instance");
  console.log(gridInstance);
  if (gridInstance) {
    gridInstance.refresh();
  }

  // Get and Set DateBox Value
  var currentValue = $("#dateBox").dxDateBox("option", "value");
  console.log("Current Date:", currentValue);

  $("#dateBox").dxDateBox("option", "value", new Date(2022, 11, 25));

  // Dispose the DataGrid
  // $("#gridContainer").dxDataGrid("dispose");
});

// Function to Show Popup
const showPopUp = () => {
  var popupInstance = $("#popup").dxPopup("instance");
  console.log(popupInstance);
  if (popupInstance) {
    popupInstance.show();
  }
};
