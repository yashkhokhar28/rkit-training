$(() => {
  var orientation = "horizontal";

  const dxMenu = $("#menu")
    .dxMenu({
      dataSource: peripheralData,
      activeStateEnabled: true,
      adaptivityEnabled: false,
      orientation: orientation,
      hideSubmenuOnMouseLeave: true,
      displayExpr: "name",
      showFirstSubmenuMode: "onHover",
      showSubmenuMode: "onClick",
      onItemClick(data) {
        const item = data.itemData;
        if (item.price) {
          DevExpress.ui.notify(
            `${item.name} is having price of ${item.price}`,
            "normal",
            3000
          );
        } else {
          DevExpress.ui.notify(`${item.name} selected`, "normal", 2000);
        }
      },

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
    })
    .dxMenu("instance");

  $("#orientationButton").dxButton({
    text: "Toggle Orientation",
    type: "success",
    onClick: () => {
      orientation = orientation === "horizontal" ? "vertical" : "horizontal";
      dxMenu.option("orientation", orientation);
    },
  });
});
