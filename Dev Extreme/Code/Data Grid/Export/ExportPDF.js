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
            return result.recipes.filter(
              (recipe) =>
                !recipe.name.toLowerCase().includes("chicken") &&
                !recipe.name.toLowerCase().includes("beef")
            );
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
    scrolling: {
      mode: "virtual",
    },
    selection: {
      mode: "multiple",
      showCheckBoxesMode: "always",
    },
    columns: [
      {
        dataField: "image",
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
      { dataField: "name", caption: "Title" },
      { dataField: "cuisine", caption: "Cuisine" },
      { dataField: "caloriesPerServing", caption: "Calories" },
      { dataField: "cookTimeMinutes", caption: "Cook Time (Min)" },
      { dataField: "prepTimeMinutes", caption: "Prep Time (Min)" },
      { dataField: "rating", caption: "Rating" },
      { dataField: "mealType", caption: "Meal Type" },
    ],
    export: {
      enabled: true,
      allowExportSelectedData: true,
    },
    onExporting(e) {
      // "p" → Page Orientation

      // "p" → Portrait mode (default)
      // "l" → Landscape mode
      // "mm" → Unit of Measurement

      // "mm" → Millimeters

      // Other options:
      // "pt" → Points
      // "cm" → Centimeters
      // "in" → Inches
      // "a4" → Paper Size

      // "a4" → Standard A4 size (210mm × 297mm)

      // Other options:
      // "letter" (8.5in × 11in)
      // "legal" (8.5in × 14in)
      // "a3", "a5", etc.

      const doc = new jsPDF({
        orientation: "p", // "p" = Portrait, "l" = Landscape
        unit: "mm", // "mm" = Millimeters, can be "pt", "cm", "in"
        format: "a4", // Paper size: "a4", "letter", "legal", "a3", "a5", custom sizes
        compress: false, // Enable compression (reduces PDF size)
        putOnlyUsedFonts: true, // Only include used fonts (reduces size)
        precision: 16, // Precision for measurements
        userUnit: 1.0, // Scale factor for measurements
      });

      doc.setProperties({
        title: "Recipe List",
        subject: "List of recipes with details",
        author: "Yash Khokhar",
        keywords: "recipes, food, cooking",
        creator: "MyWebApp",
      });

      doc.setFillColor(255, 255, 255); // white
      doc.rect(
        0,
        0,
        doc.internal.pageSize.width,
        doc.internal.pageSize.height,
        "F"
      );

      const marginLeft = 10;
      const startY = 20;

      // Title Styling
      doc.setFont("helvetica", "bold");
      doc.setFontSize(18);
      doc.text("Recipe List", 105, 15, null, null, "center");

      // Define Column Headers
      const columns = [
        "Image",
        "Title",
        "Cuisine",
        "Calories",
        "Cook Time",
        "Prep Time",
        "Rating",
        "Meal Type",
      ];
      const columnWidths = [30, 40, 35, 20, 25, 25, 20, 30];

      let rows = [];

      e.component
        .getDataSource()
        .load()
        .done((data) => {
          data.forEach((recipe, index) => {
            let imgData = recipe.image;
            let imgSize = 10;
            let imgCell = { content: imgData, image: true, width: imgSize };

            rows.push([
              imgCell,
              recipe.name,
              recipe.cuisine,
              recipe.caloriesPerServing,
              recipe.cookTimeMinutes,
              recipe.prepTimeMinutes,
              recipe.rating,
              recipe.mealType,
            ]);
          });

          // Draw Table
          doc.autoTable({
            startY: startY + 10,
            head: [columns],
            body: rows,
            styles: {
              fontSize: 10,
              halign: "center",
              valign: "middle",
            },
            columnStyles: {
              0: { cellWidth: columnWidths[0] },
              1: { cellWidth: columnWidths[1] },
              2: { cellWidth: columnWidths[2] },
              3: { cellWidth: columnWidths[3] },
              4: { cellWidth: columnWidths[4] },
              5: { cellWidth: columnWidths[5] },
              6: { cellWidth: columnWidths[6] },
              7: { cellWidth: columnWidths[7] },
            },
            didDrawCell: function (data) {
              if (data.section === "body" && data.column.index === 0) {
                let img = data.row.raw[0].content;
                if (img) {
                  doc.addImage(
                    img,
                    "JPEG",
                    data.cell.x + 2,
                    data.cell.y + 2,
                    15,
                    15
                  );
                }
              }
            },
          });

          doc.save("Recipe.pdf");
        });
    },
  });
});
