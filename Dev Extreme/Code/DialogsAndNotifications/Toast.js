$(() => {
  console.log("Document is Ready");

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
                !recipe.name.toLowerCase().includes("chicken") &&
                !recipe.name.toLowerCase().includes("beef")
            );
            console.log(filteredRecipes);
            return filteredRecipes; // DataGrid expects an array
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

  $("#gridContainer").dxDataGrid({
    dataSource: store,
    keyExpr: "id",
    columns: [
      { dataField: "name", caption: "Recipe Name" },
      { dataField: "cuisine", caption: "Cuisine" },
      { dataField: "prepTimeMinutes", caption: "Prep Time (min)" },
      { dataField: "cookTimeMinutes", caption: "Cook Time (min)" },
      { dataField: "servings", caption: "Servings" },
      { dataField: "difficulty", caption: "Difficulty" },
      { dataField: "caloriesPerServing", caption: "Calories" },
    ],
    showBorders: true,
    paging: {
      pageSize: 10,
    },
    onRowClick(e) {
      const message = `Name: ${e.data.name}`;
      let toast = $("#toastContainer").dxToast("instance");
      toast.option("message", message);
      toast.show();
    },
  });

  // Initialize toast
  $("#toastContainer").dxToast({
    closeOnSwipe: true,
    displayTime: 5000,
    type: "info",
    message: "",
  });
});
