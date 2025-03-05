$(() => {
  console.log("Document is Ready!!");

  const store = new DevExpress.data.CustomStore({
    load() {
      return $.ajax({
        url: "https://dummyjson.com/recipes?limit=100",
        dataType: "json",
      })
        .then((result) => {
          if (Array.isArray(result.recipes)) {
            const filteredRecipes = result.recipes.filter(
              (recipe) =>
                !recipe.name.toString().toLowerCase().includes("chicken") &&
                !recipe.name.toString().toLowerCase().includes("beef")
            );
            console.log(filteredRecipes);
            return filteredRecipes;
          } else {
            console.error("Unexpected response structure:", result);
            return [];
          }
        })
        .catch((error) => {
          console.error("Data Loading Error:", error);
          return [];
        });
    },
  });

  // Initialize DevExtreme DataGrid
  $("#gridContainer").dxDataGrid({
    dataSource: store,
    columns: [
      {
        dataField: "image",
        caption: "Pizza Image",
        width: 200,
        cellTemplate: (container, options) => {
          // Create a container for the load indicator
          const $indicator = $("<div>").appendTo(container);

          // Initialize the dxLoadIndicator
          $indicator.dxLoadIndicator({
            height: 40,
            width: 40,
          });

          // Create an image element
          const $img = $("<img>").attr("src", options.value).css({
            maxWidth: "100%",
            height: "auto",
            display: "none", // Hidden initially
          });

          // Append the image to the container
          $img.appendTo(container);

          // When the image loads, hide the indicator and show the image
          $img.on("load", () => {
            $indicator.dxLoadIndicator("instance").option("visible", false);
            $img.css("display", "block");
          });
        },
      },
    ],
    showBorders: true,
    paging: {
      enabled: true,
    },
    pager: {},
  });
});
