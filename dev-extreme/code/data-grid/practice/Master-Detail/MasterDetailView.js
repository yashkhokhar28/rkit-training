$(() => {
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

  $("#gridContainer").dxDataGrid({
    dataSource: store,
    showBorders: true,
    paging: {
      pageSize: 10,
    },
    customizeColumns(columns) {
      columns[3].width = 120;
      columns[4].width = 120;
      columns[5].width = 120;
    },
    columns: [
      {
        dataField: "image",
        dataType: "string",
        caption: "Product Image",
        cellTemplate(container, options) {
          if (options.value) {
            $("<div>")
              .append(
                $("<img>", {
                  src: options.data.image,
                  alt: "Product Image",
                  style: "width:70px; height:70px; border-radius:5px;",
                })
              )
              .appendTo(container);
          } else {
            container.text("No Image");
          }
        },
      },
      { dataField: "name", dataType: "string", caption: "Title" },
      { dataField: "cuisine", dataType: "string", caption: "Cuisine" },
      {
        dataField: "caloriesPerServing",
        dataType: "number",
        caption: "Calories Per Serving",
      },
      {
        dataField: "cookTimeMinutes",
        dataType: "number",
        caption: "Cook Time In Minutes",
      },
      {
        dataField: "prepTimeMinutes",
        dataType: "number",
        caption: "Prep Time In Minutes",
      },
      {
        dataField: "rating",
        dataType: "number",
        caption: "Rating",
      },
      {
        dataField: "mealType",
        dataType: "string",
        caption: "Meal Type",
      },
    ],
    masterDetail: {
      enabled: true,
      template: (container, options) => {
        const currentRecipeData = options.data;

        const ingredientsList = $("<ul>").css({
          "padding-left": "20px",
          "list-style-type": "circle",
        });

        currentRecipeData.ingredients.forEach((ingredient) => {
          ingredientsList.append($("<li>").text(ingredient));
        });

        const instructionsList = $("<ol>").css({
          "padding-left": "20px",
        });

        currentRecipeData.instructions.forEach((step) => {
          instructionsList.append($("<li>").text(step));
        });

        $("<div>")
          .addClass("master-detail-caption")
          .text("📝 Recipe Details")
          .css({
            "font-weight": "bold",
            "font-size": "16px",
            "margin-bottom": "10px",
          })
          .appendTo(container);

        $("<div>")
          .append($("<h4>").text("🍽 Ingredients:"))
          .append(ingredientsList)
          .appendTo(container);

        $("<div>")
          .css({ "margin-top": "15px" })
          .append($("<h4>").text("📜 Instructions:"))
          .append(instructionsList)
          .appendTo(container);
      },
    },
  });
});
