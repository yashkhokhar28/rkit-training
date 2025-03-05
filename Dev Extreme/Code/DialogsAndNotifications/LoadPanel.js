$(() => {
  console.log("Document is Ready");

  // Ensure loadPanel is properly initialized
  const loadPanel = $(".loadpanel")
    .dxLoadPanel({
      shadingColor: "rgba(0,0,0,0.4)",
      deferRendering: true,
      message: "Loading.......",
      position: { of: ".details-container" },
      visible: false,
      showIndicator: true,
      showPane: true,
      shading: true,
      hideOnOutsideClick: false,
    })
    .dxLoadPanel("instance");

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
            return { data: filteredRecipes };
          } else {
            console.error("Unexpected response structure:", result);
            return { data: [] };
          }
        })
        .catch((error) => {
          console.error("Data Loading Error:", error);
          return { data: [] };
        });
    },
  });

  $("#gridContainer").dxDataGrid({
    dataSource: store,
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
      if (!loadPanel) {
        console.error("Load panel is not initialized.");
        return;
      }

      console.log("Load panel is shown");
      loadPanel.show();

      // Simulate a loading delay
      setTimeout(() => {
        loadPanel.hide();
        showRecipeInfo(e.data);
      }, 2000);
    },
  });

  function showRecipeInfo(recipe) {
    $(".header").text(recipe.name || "");
    $(".cuisine").text(recipe.cuisine || "");
    $(".prep-time").text(recipe.prepTimeMinutes || "");
    $(".cook-time").text(recipe.cookTimeMinutes || "");
    $(".servings").text(recipe.servings || "");
    $(".difficulty").text(recipe.difficulty || "");
    $(".calories").text(recipe.caloriesPerServing || "");
    $(".rating").text(recipe.rating || "");
    $(".review-count").text(recipe.reviewCount || "");

    $(".ingredient-list").html(
      recipe.ingredients ? recipe.ingredients.join(", ") : ""
    );
    $(".instruction-list").html(
      recipe.instructions ? recipe.instructions.join("<br>") : ""
    );

    $(".meal-type").text(recipe.mealType || "");
    $(".tags").text(recipe.tags ? recipe.tags.join(", ") : "");

    if (recipe.image) {
      $(".recipe-image").attr("src", recipe.image);
    }
  }
});
