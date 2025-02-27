$(document).ready(() => {
  const store = new DevExpress.data.CustomStore({
    load() {
      return $.ajax({
        url: "https://dummyjson.com/recipes?limit=100",
        dataType: "json",
      })
        .then((result) => {
          if (Array.isArray(result.recipes)) {
            return result.recipes.filter(
              (recipe) =>
                !recipe.name.toLowerCase().includes("chicken") &&
                !recipe.name.toLowerCase().includes("beef")
            );
          }
          console.error("Unexpected response structure:", result);
          return [];
        })
        .catch((error) => {
          console.error("Data Loading Error:", error);
          return [];
        });
    },
  });

  // Data Grid Configuration
  $("#gridContainer").dxDataGrid({
    dataSource: store,
    showBorders: true,
    scrolling: {
      mode: "virtual",
    },
    selection: {
      mode: "multiple",
      showCheckBoxesMode: "always",
    },

    columns: [
      // {
      //   dataField: "image",
      //   caption: "Product Image",
      //   cellTemplate(container, options) {
      //     if (options.value) {
      //       $("<div>")
      //         .append(
      //           $("<img>", {
      //             src: options.data.image,
      //             alt: "Product Image",
      //             style: "width:70px; height:70px; border-radius:5px;",
      //           })
      //         )
      //         .appendTo(container);
      //     } else {
      //       container.text("No Image");
      //     }
      //   },
      // },
      { dataField: "name", caption: "Title" },
      { dataField: "cuisine", caption: "Cuisine" },
      { dataField: "caloriesPerServing", caption: "Calories" },
      { dataField: "cookTimeMinutes", caption: "Cook Time (Min)" },
      { dataField: "prepTimeMinutes", caption: "Prep Time (Min)" },
      { dataField: "rating", caption: "Rating" },
      { dataField: "mealType", caption: "Meal Type" },
    ],

    // Export Configuration
    export: {
      enabled: true,
      allowExportSelectedData: true,
    },

    // Excel Export Handler
    onExporting(e) {
      const workbook = new ExcelJS.Workbook();
      const worksheet = workbook.addWorksheet("Recipes");

      worksheet.columns = [
        { width: 5 },
        { width: 30 },
        { width: 25 },
        { width: 15 },
        { width: 25 },
        { width: 40 },
        { width: 25 },
      ];

      DevExpress.excelExporter
        .exportDataGrid({
          component: e.component,
          worksheet,
          keepColumnWidths: false,
          topLeftCell: { row: 2, column: 2 },
          customizeCell(options) {
            const { gridCell, excelCell } = options;

            if (gridCell.rowType === "data") {
              switch (gridCell.column.dataField) {
                case "name":
                  excelCell.font = { bold: true };
                  break;
                case "caloriesPerServing":
                  excelCell.numFmt = "0"; // Format as a number
                  break;
                case "cookTimeMinutes":
                case "prepTimeMinutes":
                  excelCell.numFmt = "0"; // Ensure these are numbers
                  excelCell.alignment = { horizontal: "right" };
                  break;
                case "rating":
                  excelCell.numFmt = "0.0"; // Format rating with one decimal place
                  excelCell.alignment = { horizontal: "center" };
                  break;
                case "mealType":
                  excelCell.font = { italic: true };
                  break;
                default:
                  break;
              }
            }

            if (gridCell.rowType === "totalFooter" && excelCell.value) {
              excelCell.font.italic = true;
            }
          },
        })
        .then((cellRange) => {
          // HEADER
          const headerRow = worksheet.getRow(2);
          headerRow.height = 30;
          worksheet.mergeCells(2, 1, 2, 7); // Merging across all columns

          headerRow.getCell(1).value =
            "Recipes List - Cuisine & Nutrition Details";
          headerRow.getCell(1).font = {
            name: "Segoe UI Light",
            size: 22,
            bold: true,
          };
          headerRow.getCell(1).alignment = { horizontal: "center" };

          // FOOTER
          const footerRowIndex = cellRange.to.row + 2;
          const footerRow = worksheet.getRow(footerRowIndex);
          worksheet.mergeCells(footerRowIndex, 1, footerRowIndex, 7); // Merging across all columns

          footerRow.getCell(1).value = "Generated from DevExtreme DataGrid";
          footerRow.getCell(1).font = {
            color: { argb: "BFBFBF" },
            italic: true,
          };
          footerRow.getCell(1).alignment = { horizontal: "right" };
        })
        .then(() => {
          return workbook.xlsx.writeBuffer();
        })
        .then((buffer) => {
          saveAs(
            new Blob([buffer], { type: "application/octet-stream" }),
            "Recipes.xlsx"
          );
        });
    },
  });
});
