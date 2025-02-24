window.jsPDF = window.jspdf.jsPDF;
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
    selection: {
      mode: "multiple",
      showCheckBoxesMode: "always",
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
    export: {
      enabled: true,
      allowExportSelectedData: true,
    },
    onExporting(e) {
      console.log(e);

      const doc = new jsPDF();

      DevExpress.pdfExporter
        .exportDataGrid({
          columnWidths: [50, 40, 40, 40, 40, 40, 40, 40],
          jsPDFDocument: doc,
          keepColumnWidths: true,
          component: e.component,
          indent: 5,
          customizeCell: function (options) {
            const { gridCell, pdfCell } = options;
            if (gridCell.rowType === "data") {
              pdfCell.styles = {
                font: "times",
                fontSize: 14,
              };
            }
          },
          loadPanel: {
            enabled: true,
            height: 70,
            indicatorSrc: "op",
            shading: true,
            shadingColor: "",
            showIndicator: true,
            showPane: true,
            text: "boooom",
            width: 400,
          },
        })
        .then(() => {
          doc.save("Recipe.pdf");
        });
    },
  });
});
