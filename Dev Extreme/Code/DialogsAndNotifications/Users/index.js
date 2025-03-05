$(() => {
  // Initialize the Menu
  $("#menu").dxMenu({
    dataSource: [{ id: 1, text: "Open Form" }],
    adaptivityEnabled: true,
    onSubmenuHidden: () => {
      console.log("Sub Menu Is Hidden");
    },
    onSubmenuHiding: () => {
      console.log("Sub Menu Is Hiding");
    },
    onSubmenuShowing: () => {
      console.log("Sub Menu Is Showing");
    },
    onSubmenuShown: () => {
      console.log("Sub Menu Is Shown");
    },
    onItemClick: function () {
      popup.option("visible", true);
    },
  });

  // Initialize the Popup with a form
  var popup = $("#popup")
    .dxPopup({
      title: "User Form",
      visible: false,
      fullScreen: true,
      width: 400,
      showTitle: true,
      height: 300,
      showCloseButton: true,
      closeOnOutsideClick: true,
      onShowing: () => console.log("Popup showing"),
      onInitialized: () => console.log("Popup initialized successfully"),
      contentTemplate: function (contentElement) {
        contentElement.append("<div id='form'></div>");
        $("#form").dxForm({
          formData: {},
          items: [
            {
              dataField: "FullName",
              label: { text: "Full Name" },
              validationRules: [
                { type: "required", message: "Username is required" },
              ],
            },
            {
              dataField: "Email",
              label: { text: "Email" },
              validationRules: [
                { type: "required", message: "Username is required" },
              ],
            },
            {
              dataField: "PhoneNumber",
              label: { text: "Phone" },
              validationRules: [
                { type: "required", message: "Username is required" },
              ],
            },
            {
              dataField: "Department",
              label: { text: "Department" },
              validationRules: [
                { type: "required", message: "Username is required" },
              ],
            },
          ],
        });
        contentElement.append("<div id='submitBtn'></div>");
        $("#submitBtn").dxButton({
          text: "Save",
          type: "success",
          onClick: function () {
            saveData();
          },
        });
      },
    })
    .dxPopup("instance");

  $("#grid").dxDataGrid({
    dataSource: "https://67a9f61d65ab088ea7e526d8.mockapi.io/users",
  });

  function saveData() {
    var form = $("#form").dxForm("instance");
    const validationResult = form.validate();
    if (!validationResult.isValid) {
      return;
    }
    var data = form.option("formData");

    // Show loading indicator
    var loadPanel = $("#loader")
      .dxLoadPanel({
        shadingColor: "rgba(0,0,0,0.4)",
        deferRendering: true,
        message: "Loading.......",
        position: { of: ".details-container" },
        visible: true,
        showIndicator: true,
        showPane: true,
        shading: true,
        hideOnOutsideClick: false,
      })
      .dxLoadPanel("instance");

    $.ajax({
      url: "https://67a9f61d65ab088ea7e526d8.mockapi.io/users",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify(data),
      success: () => {
        DevExpress.ui.notify("User saved successfully!", "success", 2000);
        popup.option("visible", false);
      },
      error: () => {
        DevExpress.ui.notify("Error saving user!", "error", 2000);
      },
      complete: () => {
        loadPanel.option("visible", false);
      },
    });
  }
});
