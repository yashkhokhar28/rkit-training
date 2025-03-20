window.jsPDF = window.jspdf.jsPDF;

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

    // PDF Export Handler
    async onExporting(e) {
      /*
        Page Orientation Options:
        - "p" → Portrait mode (default)
        - "l" → Landscape mode

        Unit of Measurement Options:
        - "mm" → Millimeters (default)
        - "pt" → Points
        - "cm" → Centimeters
        - "in" → Inches

        Paper Size Options:
        - "a4" → Standard A4 size (210mm × 297mm) (default)
        - "letter" → Letter size (8.5in × 11in)
        - "legal" → Legal size (8.5in × 14in)
        - "a3", "a5", etc.
      */
      const doc = new jsPDF({
        orientation: "p",
        unit: "mm",
        format: "a4",
        compress: false,
        putOnlyUsedFonts: true,
        precision: 16,
        userUnit: 1.0,
      });

      // Document Metadata
      doc.setProperties({
        title: "Recipe List",
        subject: "List of recipes with details",
        author: "Yash Khokhar",
        keywords: "recipes, food, cooking",
        creator: "MyWebApp",
      });

      /*
        Background Setup:
        - This sets the fill color for the upcoming shape (rectangle).
        RGB Color Values:
        - (255, 255, 255) → White
        - (0, 0, 0) → Black
        - (255, 0, 0) → Red
        - (0, 255, 0) → Green
        - (0, 0, 255) → Blue
      */
      doc.setFillColor(255, 255, 255); // White background

      /*
        This creates a filled rectangle covering the entire page.

        Function:
        - rect(x, y, width, height, style)

        Parameters:
        - 0, 0 → Top-left corner (X, Y)
        - doc.internal.pageSize.width → Width of the page
        - doc.internal.pageSize.height → Height of the page
        - "F" → Fills the rectangle with the set color (from setFillColor)
      */
      doc.rect(
        0,
        0,
        doc.internal.pageSize.width,
        doc.internal.pageSize.height,
        "F"
      );

      const startY = 20;
      doc.setFont("helvetica", "bold");
      doc.setFontSize(18);
      doc.text("Recipe List", 105, 15, null, null, "center");

      const dataGrid = e.component;

      let selectedRows = dataGrid.getSelectedRowsData();

      if (selectedRows.length === 0) {
        // No rows selected, export all data
        try {
          const allData = await dataGrid.getDataSource().store().load();
          selectedRows = allData; // Export all rows
        } catch (error) {
          console.error("Error loading all data for export:", error);
          alert("Failed to load all data for export.");
          return;
        }
      }

      // Prepare Table Data
      let rows = selectedRows.map((recipe) => [
        recipe.name,
        recipe.cuisine,
        recipe.caloriesPerServing,
        recipe.cookTimeMinutes,
        recipe.prepTimeMinutes,
        recipe.rating,
        recipe.mealType,
      ]);
      /*
            Positioning & Layout Options:

            - startY: Sets the starting Y position of the table.
            - margin: Defines margins for the table (e.g., {top: 10, right: 10, bottom: 10, left: 10}).
            - pageBreak: Determines how the table behaves when it reaches the end of a page ('auto', 'avoid', 'always').
            - showHead: Controls header display ('everyPage', 'firstPage', 'never').
            - showFoot: Controls footer display ('everyPage', 'lastPage', 'never').
            - tableWidth: Defines table width ('auto', 'wrap', or a fixed number).
          */

      /*
            Column & Cell Options:

            - head: Defines table headers (must be a nested array).
            - body: Defines table data (rows).
            - columnStyles: Sets styles per column (width, text alignment, etc.).
            - rowPageBreak: Defines row behavior when breaking across pages ('auto', 'avoid').
            - theme: Predefined themes: 'striped', 'grid', 'plain'.
            - columns: Allows defining columns with keys (alternative to head).
          */

      /*
            Styling Options:

            - styles: Defines global cell styles (font, alignment, text color, background, padding).
            - headStyles: Styles for header row only.
            - bodyStyles: Styles for body rows only.
            - alternateRowStyles: Alternates row styles for readability.
            - footStyles: Styles for footer row only.
          */

      /*
            Customizing Cells (didDrawCell, willDrawCell):

            - didDrawCell: Executes after a cell is drawn (useful for images, borders, etc.).
            - willDrawCell: Executes before a cell is drawn (modify content dynamically).

            Adding Custom Headers & Footers (didDrawPage):

            - didDrawPage: Executes when a new page is created (used for headers, footers, page numbers, etc.).

            Page Break Handling:

            - pageBreak: Defines how rows behave when breaking ('auto', 'avoid', 'always').
            - rowPageBreak: Controls row behavior across pages.

            Adding Borders to Specific Cells:

            - drawCell: Allows custom border styles for each cell.
          */

      // Draw Table
      doc.autoTable({
        startY: startY,
        margin: { top: 10, right: 10, bottom: 10, left: 10 },
        pageBreak: "auto",
        showHead: "everyPage",
        showFoot: "lastPage",
        tableWidth: "auto",
        head: [
          [
            "Title",
            "Cuisine",
            "Calories",
            "Cook Time",
            "Prep Time",
            "Rating",
            "Meal Type",
          ],
        ],
        body: rows,
        rowPageBreak: "avoid",
        theme: "striped",
        styles: {
          fontSize: 10,
          cellPadding: 2,
          valign: "middle",
          textColor: [0, 0, 0],
        },
        headStyles: {
          fillColor: [41, 128, 185], // Blue header
          textColor: [255, 255, 255],
          fontStyle: "bold",
        },
        bodyStyles: {
          fillColor: [245, 245, 245], // Light gray background
        },
        alternateRowStyles: {
          fillColor: [220, 220, 220], // Alternating row color
        },
        footStyles: {
          fillColor: [100, 100, 100],
          textColor: [255, 255, 255],
          fontStyle: "bold",
        },
        columnStyles: {
          0: { cellWidth: 40 },
          1: { cellWidth: 35 },
          2: { cellWidth: 20, halign: "center" },
          3: { cellWidth: 25, halign: "center" },
          4: { cellWidth: 25, halign: "center" },
          5: { cellWidth: 20, halign: "center" },
          6: { cellWidth: 30 },
        },
        willDrawCell(data) {
          if (data.column.index === 2 && data.cell.raw > 500) {
            data.cell.styles.textColor = [255, 0, 0]; // Red text for high-calorie items
          }
        },

        didDrawCell(data) {
          if (data.column.index === 5 && data.cell.raw >= 4.5) {
            doc.setTextColor(0, 128, 0);
          }
        },

        didDrawPage(data) {
          doc.setFontSize(8);
          doc.text(
            `Page ${doc.internal.getNumberOfPages()}`,
            data.settings.margin.left,
            doc.internal.pageSize.height - 10
          );
        },
      });
      doc.save("Recipes_Selected_Data.pdf");
      e.cancel = true; // Prevent default export behavior
    },
  });
});
