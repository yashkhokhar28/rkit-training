$(document).ready(() => {
  // Create a CustomStore to fetch data from an API
  const store = new DevExpress.data.CustomStore({
    load() {
      return $.ajax({
        url: "https://dummyjson.com/recipes?limit=100", // Fetch 100 recipes
        dataType: "json",
      })
        .then((result) => {
          if (Array.isArray(result.recipes)) {
            // Filter out recipes containing "chicken" or "beef" in their name
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

  // Initialize DevExtreme DataGrid
  $("#gridContainer").dxDataGrid({
    dataSource: store,
    showBorders: true, // Display grid borders
    scrolling: {
      mode: "virtual", // Enable virtual scrolling for better performance
    },
    selection: {
      mode: "multiple", // Allow multiple row selection
      showCheckBoxesMode: "always", // Show checkboxes for selection
    },

    // Define columns for the DataGrid
    columns: [
      { dataField: "name", caption: "Title" },
      { dataField: "cuisine", caption: "Cuisine" },
      { dataField: "caloriesPerServing", caption: "Calories" },
      { dataField: "cookTimeMinutes", caption: "Cook Time (Min)" },
      { dataField: "prepTimeMinutes", caption: "Prep Time (Min)" },
      { dataField: "rating", caption: "Rating" },
      { dataField: "mealType", caption: "Meal Type" },
    ],

    // Enable data export options
    export: {
      enabled: true,
      allowExportSelectedData: true, // Allow exporting only selected rows
    },

    // Handle Excel export functionality
    onExporting(e) {
      const workbook = new ExcelJS.Workbook();
      const worksheet = workbook.addWorksheet("Recipes");

      // Define column widths in the Excel file
      worksheet.columns = [
        { width: 5 },
        { width: 30 },
        { width: 25 },
        { width: 15 },
        { width: 25 },
        { width: 40 },
        { width: 25 },
      ];

      // Export DataGrid to Excel
      DevExpress.excelExporter
        .exportDataGrid({
          component: e.component,
          worksheet,
          keepColumnWidths: false,
          topLeftCell: { row: 2, column: 2 }, // Start data from row 2, column 2
          customizeCell(options) {
            const { gridCell, excelCell } = options;

            if (gridCell.rowType === "data") {
              switch (gridCell.column.dataField) {
                case "name":
                  excelCell.font = { bold: true }; // Make title bold
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
                  excelCell.font = { italic: true }; // Italicize meal type
                  break;
                default:
                  break;
              }
            }

            // Style total footer row
            if (gridCell.rowType === "totalFooter" && excelCell.value) {
              excelCell.font.italic = true;
            }
          },
        })
        .then((cellRange) => {
          // Add a merged title row in the Excel file
          const headerRow = worksheet.getRow(1);
          headerRow.height = 30;
          worksheet.mergeCells(1, 1, 1, 7); // Merge across all columns
          headerRow.getCell(1).value =
            "Recipes List - Cuisine & Nutrition Details";
          headerRow.getCell(1).font = {
            name: "Segoe UI Light",
            size: 22,
            bold: true,
          };
          headerRow.getCell(1).alignment = { horizontal: "center" };

          // Add a footer message
          const footerRowIndex = cellRange.to.row + 2;
          const footerRow = worksheet.getRow(footerRowIndex);
          worksheet.mergeCells(footerRowIndex, 1, footerRowIndex, 7); // Merge across all columns

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
          // Save the generated Excel file
          saveAs(
            new Blob([buffer], { type: "application/octet-stream" }),
            "Recipes.xlsx"
          );
        });
    },
  });
});
