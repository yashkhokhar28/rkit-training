$(() => {
  let popup;

  const popover = $("#popover")
    .dxPopover({
      target: "#menu",
      closeOnOutsideClick: true,
      showEvent: "mouseenter",
      hideEvent: "mouseleave",
      position: "top",
      width: 300,
      animation: {
        show: {
          type: "pop",
          from: { scale: 0 },
          to: { scale: 1 },
        },
        hide: {
          type: "fade",
          from: 1,
          to: 0,
        },
      },
      contentTemplate: function (contentElement) {
        contentElement.append("<p>Click to open the form</p>");
      },
    })
    .dxPopover("instance");

  // Initialize the Menu
  let menu = $("#menu")
    .dxMenu({
      dataSource: [{ id: 1, text: "Open Form" }],
      adaptivityEnabled: true,
      onItemClick: function () {
        popup.option("visible", true);
      },
      onItemRendered: function (e) {
        popover.option("target", e.itemElement);
      },
    })
    .dxMenu("instance");

  const treeView = $("#tree")
    .dxTreeView({
      activeStateEnabled: true,
      animationEnabled: true,
      dataSource: new DevExpress.data.CustomStore({
        load: function () {
          return $.getJSON("https://67a9f61d65ab088ea7e526d8.mockapi.io/users")
            .then((data) => {
              return data;
            })
            .fail((error) => {
              console.error("API Load Error:", error);
              return [];
            });
        },
      }),
      dataStructure: "tree",
      keyExpr: "id",
      parentIdExpr: "parentId",
      rootValue: null,
      expandAllEnabled: true,
      focusStateEnabled: true,
      searchEnabled: true,
      searchExpr: "text",
      onItemClick: (e) => {
        console.log("Clicked:", e.itemData);
        DevExpress.ui.notify(`Selected: ${e.itemData.text}`, "info", 2000);
      },
    })
    .dxTreeView("instance");

  popup = $("#popup")
    .dxPopup({
      title: "User Form",
      visible: false,
      fullScreen: true,
      width: 500,
      height: 500,
      showTitle: true,
      showCloseButton: true,
      closeOnOutsideClick: true,
      contentTemplate: function (contentElement) {
        const $scrollView = $("<div id='scrollView'></div>").appendTo(
          contentElement
        );
        $scrollView.append("<div id='form'></div>");

        $("#form").dxForm({
          formData: {},
          validationGroup: "userFormValidation",
          alignItemLabels: true,
          colCount: 1,
          labelLocation: "left",
          scrollingEnabled: true,
          showRequiredMark: true,
          items: [
            {
              itemType: "group",
              caption: "Name",
              items: [
                {
                  dataField: "Name.FirstName",
                  label: { text: "First Name" },
                  validationRules: [
                    { type: "required", message: "First Name is required" },
                  ],
                },
                {
                  dataField: "Name.MiddleName",
                  label: { text: "Middle Name" },
                },
                {
                  dataField: "Name.LastName",
                  label: { text: "Last Name" },
                  validationRules: [
                    { type: "required", message: "Last Name is required" },
                  ],
                },
              ],
            },
            {
              itemType: "group",
              caption: "Contact",
              items: [
                {
                  dataField: "Contact.Email",
                  label: { text: "Email" },
                  validationRules: [
                    { type: "email", message: "Invalid email format" },
                    { type: "required", message: "Email is required" },
                  ],
                },
                {
                  dataField: "Contact.Number.Mobile",
                  label: { text: "Mobile Number" },
                  validationRules: [
                    {
                      type: "pattern",
                      pattern: /^\d{10}$/,
                      message: "Mobile must be a 10-digit number",
                    },
                  ],
                },
                {
                  dataField: "Contact.Number.Home",
                  label: { text: "Home Number" },
                },
                {
                  dataField: "Contact.Number.Work",
                  label: { text: "Work Number" },
                },
              ],
            },
            {
              itemType: "group",
              caption: "Address",
              items: [
                { dataField: "Address.Street", label: { text: "Street" } },
                { dataField: "Address.City", label: { text: "City" } },
                { dataField: "Address.State", label: { text: "State" } },
                { dataField: "Address.ZipCode", label: { text: "Zip Code" } },
                { dataField: "Address.Country", label: { text: "Country" } },
              ],
            },
          ],
        });

        $scrollView.append(
          "<div id='submitBtn' style='margin-top: 20px;'></div>"
        );
        $("#submitBtn").dxButton({
          text: "Save",
          type: "success",
          validationGroup: "userFormValidation",
          onClick: function () {
            saveData();
          },
        });

        $scrollView.dxScrollView({ width: "100%", height: "100%" });
      },
    })
    .dxPopup("instance");

  const saveData = () => {
    var validationGroup =
      DevExpress.validationEngine.getGroupConfig("userFormValidation");
    if (!validationGroup || !validationGroup.validate().isValid) {
      DevExpress.ui.notify(
        "Please fill all required fields correctly!",
        "error",
        2000
      );
      return;
    }

    var form = $("#form").dxForm("instance");
    var data = form.option("formData");
    var userId = Date.now().toString();

    // Construct the hierarchical data
    const userData = {
      id: userId,
      parentId: null,
      text: `${data.Name.FirstName}${
        data.Name.MiddleName ? " " + data.Name.MiddleName : ""
      } ${data.Name.LastName}`,
      hasItems: true,
      items: [
        {
          id: `${userId}-1`,
          parentId: userId,
          text: `First Name: ${data.Name.FirstName}`,
          hasItems: false,
        },
        ...(data.Name.MiddleName
          ? [
              {
                id: `${userId}-2`,
                parentId: userId,
                text: `Middle Name: ${data.Name.MiddleName}`,
                hasItems: false,
              },
            ]
          : []),
        {
          id: `${userId}-3`,
          parentId: userId,
          text: `Last Name: ${data.Name.LastName}`,
          hasItems: false,
        },
        {
          id: `${userId}-4`,
          parentId: userId,
          text: `Email: ${data.Contact.Email}`,
          hasItems: false,
        },
        {
          id: `${userId}-5`,
          parentId: userId,
          text: `Mobile: ${data.Contact.Number.Mobile}`,
          hasItems: false,
        },
        ...(data.Contact.Number.Home
          ? [
              {
                id: `${userId}-6`,
                parentId: userId,
                text: `Home: ${data.Contact.Number.Home}`,
                hasItems: false,
              },
            ]
          : []),
        ...(data.Contact.Number.Work
          ? [
              {
                id: `${userId}-7`,
                parentId: userId,
                text: `Work: ${data.Contact.Number.Work}`,
                hasItems: false,
              },
            ]
          : []),
        {
          id: `${userId}-8`,
          parentId: userId,
          text: `Street: ${data.Address.Street}`,
          hasItems: false,
        },
        {
          id: `${userId}-9`,
          parentId: userId,
          text: `City: ${data.Address.City}`,
          hasItems: false,
        },
        {
          id: `${userId}-10`,
          parentId: userId,
          text: `State: ${data.Address.State}`,
          hasItems: false,
        },
        {
          id: `${userId}-11`,
          parentId: userId,
          text: `Zip Code: ${data.Address.ZipCode}`,
          hasItems: false,
        },
        {
          id: `${userId}-12`,
          parentId: userId,
          text: `Country: ${data.Address.Country}`,
          hasItems: false,
        },
      ],
    };

    const jsonData = JSON.stringify(userData);

    var loadPanel = $("#loader")
      .dxLoadPanel({
        shadingColor: "rgba(0,0,0,0.4)",
        message: "Saving...",
        visible: true,
        showIndicator: true,
      })
      .dxLoadPanel("instance");

    $.ajax({
      url: "https://67a9f61d65ab088ea7e526d8.mockapi.io/users",
      method: "POST",
      contentType: "application/json",
      data: jsonData,
      success: () => {
        DevExpress.ui.notify("User saved successfully!", "success", 2000);
        popup.option("visible", false);
        form.option("formData", {});
        treeView.getDataSource().reload();
      },
      error: (xhr, status, error) => {
        DevExpress.ui.notify(
          `Error saving user: ${xhr.status} - ${error}`,
          "error",
          2000
        );
      },
      complete: () => {
        loadPanel.option("visible", false);
      },
    });
  };
});
