$(() => {
  console.log("Document is Ready");

  let popup;
  try {
    popup = $("#pop-up")
      .dxPopup({
        title: "Recipe Details",
        container: ".dx-viewport",
        visible: false,
        fullScreen: true,
        resizeEnabled: true,
        showTitle: true,
        showCloseButton: true,
        closeOnOutsideClick: true,
        onShowing: () => console.log("Popup showing"),
        onInitialized: () => console.log("Popup initialized successfully"),
        toolbarItems: [
          {
            locateInMenu: "always",
            widget: "dxButton",
            toolbar: "top",
            options: {
              text: "More info",
              onClick() {
                const message = `More info clicked`;
                DevExpress.ui.notify(
                  {
                    message,
                    position: {
                      my: "center top",
                      at: "center top",
                    },
                  },
                  "success",
                  3000
                );
              },
            },
          },
          {
            widget: "dxButton",
            toolbar: "bottom",
            location: "after",
            options: {
              text: "Close",
              stylingMode: "outlined",
              type: "normal",
              onClick() {
                popup.hide();
              },
            },
          },
        ],
      })
      .dxPopup("instance");
  } catch (error) {
    console.error("Failed to initialize popup:", error);
    return;
  }

  // Load Recipes
  const store = new DevExpress.data.CustomStore({
    load() {
      return $.ajax({
        url: "https://dummyjson.com/recipes?limit=100",
        dataType: "json",
        method: "GET",
        timeout: 10000,
      })
        .then((result) => {
          if (!result || !Array.isArray(result.recipes)) {
            console.error("Unexpected response structure:", result);
            return { data: [] };
          }
          const filteredRecipes = result.recipes.filter((recipe) => {
            const name = recipe.name || "";
            return (
              !name.toLowerCase().includes("chicken") &&
              !name.toLowerCase().includes("beef")
            );
          });
          console.log("Filtered recipes:", filteredRecipes);
          return { data: filteredRecipes };
        })
        .catch((error) => {
          console.error("Data Loading Error:", error);
          return { data: [] };
        });
    },
    error(err) {
      console.error("Store error:", err);
    },
  });

  // Initialize DataGrid
  $("#gridContainer").dxDataGrid({
    dataSource: store,
    columns: [
      { dataField: "name", caption: "Recipe Name" },
      { dataField: "cuisine", caption: "Cuisine" },
    ],
    showBorders: true,
    paging: {
      pageSize: 10,
    },
    onRowClick(e) {
      if (e && e.data) {
        showRecipeInfo(e.data);
      } else {
        console.error("Invalid row click event:", e);
      }
    },
    onInitialized: () => console.log("Grid initialized"),
    onDataErrorOccurred: (e) => console.error("Grid data error:", e.error),
  });

  const showRecipeInfo = (recipe) => {
    if (!recipe) {
      console.error("No recipe data provided");
      return;
    }

    if (!popup) {
      console.error("Popup not initialized");
      return;
    }

    // Update popup content and show
    try {
      popup.option("contentTemplate", () => {
        const $scrollView = $("<div/>");
        $scrollView.append(`
          <p><strong>Recipe Name:</strong> ${recipe.name || "N/A"}</p>
          <p><strong>Cuisine:</strong> ${recipe.cuisine || "N/A"}</p>
          <p><strong>Prep Time:</strong> ${
            recipe.prepTimeMinutes || "N/A"
          } min</p>
          <p><strong>Cook Time:</strong> ${
            recipe.cookTimeMinutes || "N/A"
          } min</p>
          <p><strong>Servings:</strong> ${recipe.servings || "N/A"}</p>
          <p><strong>Difficulty:</strong> ${recipe.difficulty || "N/A"}</p>
          <p><strong>Calories:</strong> ${
            recipe.caloriesPerServing || "N/A"
          } kcal</p>
          <p><strong>Ingredients:</strong> ${
            Array.isArray(recipe.ingredients)
              ? recipe.ingredients.join(", ")
              : "N/A"
          }</p>
          <p><strong>Instructions:</strong> <br> ${
            Array.isArray(recipe.instructions)
              ? recipe.instructions.join("<br>")
              : "N/A"
          }</p>
        `);

        if (recipe.image) {
          const $img = $(
            `<img src="${recipe.image}" alt="Recipe Image" style="width:100%; border-radius:8px;">`
          );
          $img.on("error", () => {
            $img.remove();
            console.warn("Failed to load image:", recipe.image);
          });
          $scrollView.append($img);
        }

        $scrollView.dxScrollView({
          width: "100%",
          height: "100%",
        });

        return $scrollView;
      });

      popup.show();
    } catch (error) {
      console.error("Error updating popup:", error);
      popup.option("contentTemplate", () =>
        $("<div>").text("Error loading recipe details")
      );
      popup.show();
    }
  };
});
