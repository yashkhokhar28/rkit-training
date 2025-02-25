window.jsPDF = window.jspdf.jsPDF;

$(document).ready(() => {
  // Custom Store with image caching
  const store = new DevExpress.data.CustomStore({
    async load() {
      try {
        const response = await $.ajax({
          url: "https://dummyjson.com/recipes?limit=100",
          dataType: "json",
        });

        if (!Array.isArray(response.recipes)) {
          console.error("Unexpected response structure:", response);
          return [];
        }

        const recipes = response.recipes.filter(
          (recipe) =>
            !recipe.name.toLowerCase().includes("chicken") &&
            !recipe.name.toLowerCase().includes("beef")
        );

        // Pre-cache images (limited to first 50 recipes)
        for (const recipe of recipes.slice(0, 50)) {
          if (recipe.image) {
            try {
              recipe.imageBase64 = await getBase64Image(recipe.image);
            } catch (error) {
              console.warn(`Failed to load image for ${recipe.name}:`, error);
              recipe.imageBase64 = null;
            }
          }
        }

        return recipes.slice(0, 50);
      } catch (error) {
        console.error("Data Loading Error:", error);
        return [];
      }
    },
  });

  // Utility Function to Convert Image URL to Base64
  async function getBase64Image(url) {
    return new Promise((resolve, reject) => {
      const img = new Image();
      img.crossOrigin = "Anonymous";
      img.src = url;

      img.onload = function () {
        const canvas = document.createElement("canvas");
        canvas.width = 50;
        canvas.height = 50;
        const ctx = canvas.getContext("2d");
        ctx.drawImage(this, 0, 0, 50, 50);
        resolve(canvas.toDataURL("image/jpeg"));
      };

      img.onerror = () => reject("Image load error: " + url);
    });
  }

  // Data Grid Configuration
  const grid = $("#gridContainer")
    .dxDataGrid({
      dataSource: store,
      showBorders: true,
      scrolling: { mode: "virtual" },
      selection: {
        mode: "multiple",
        showCheckBoxesMode: "always",
      },
      columns: [
        {
          dataField: "image",
          caption: "Product Image",
          width: 80,
          cellTemplate(container, options) {
            if (options.data.image) {
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
        { dataField: "name", caption: "Title", width: 150 },
        { dataField: "cuisine", caption: "Cuisine", width: 100 },
        { dataField: "caloriesPerServing", caption: "Calories", width: 80 },
        {
          dataField: "cookTimeMinutes",
          caption: "Cook Time (Min)",
          width: 100,
        },
        {
          dataField: "prepTimeMinutes",
          caption: "Prep Time (Min)",
          width: 100,
        },
        { dataField: "rating", caption: "Rating", width: 80 },
        { dataField: "mealType", caption: "Meal Type", width: 120 },
      ],
      export: {
        enabled: true,
        allowExportSelectedData: true,
      },
      onExporting: async function (e) {
        const loading = $("#gridContainer")
          .dxLoadPanel({
            message: "Generating PDF...",
            visible: true,
          })
          .dxLoadPanel("instance");

        const doc = new jsPDF({
          orientation: "p",
          unit: "mm",
          format: "a4",
          compress: true,
          putOnlyUsedFonts: true,
        });

        // Document Metadata
        doc.setProperties({
          title: "Recipe List",
          subject: "List of recipes with details",
          author: "Yash Khokhar",
          keywords: "recipes, food, cooking",
          creator: "MyWebApp",
        });

        // Layout Constants
        const pageWidth = doc.internal.pageSize.width;
        const pageHeight = doc.internal.pageSize.height;
        const margin = 10;
        const startY = 20;
        const maxRowsPerPage = 10;

        // Background
        doc.setFillColor(255, 255, 255);
        doc.rect(0, 0, pageWidth, pageHeight, "F");

        // Title
        doc.setFont("helvetica", "bold");
        doc.setFontSize(16);
        doc.text("Recipe List", pageWidth / 2, 15, { align: "center" });

        // Table Configuration
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
        const columnWidths = [20, 40, 30, 20, 20, 20, 15, 30];

        // Get and process data
        const data = await e.component.getDataSource().load();
        const selectedRows = e.component.getSelectedRowsData();
        const exportData =
          e.component.option("export.allowExportSelectedData") &&
          selectedRows.length > 0
            ? selectedRows
            : data;

        // Prepare table rows - Empty string for image column text
        const rows = exportData.map((recipe) => [
          "", // Empty string to prevent Base64 text
          recipe.name || "",
          recipe.cuisine || "",
          recipe.caloriesPerServing || "",
          recipe.cookTimeMinutes || "",
          recipe.prepTimeMinutes || "",
          recipe.rating || "",
          recipe.mealType || "",
        ]);

        // Split rows into pages
        const pageRows = [];
        for (let i = 0; i < rows.length; i += maxRowsPerPage) {
          pageRows.push(rows.slice(i, i + maxRowsPerPage));
        }

        // Generate table for each page
        pageRows.forEach((pageData, pageIndex) => {
          if (pageIndex > 0) {
            doc.addPage();
            doc.setFillColor(255, 255, 255);
            doc.rect(0, 0, pageWidth, pageHeight, "F");
            doc.setFont("helvetica", "bold");
            doc.setFontSize(16);
            doc.text("Recipe List (Continued)", pageWidth / 2, 15, {
              align: "center",
            });
          }

          doc.autoTable({
            startY: startY,
            head: [columns],
            body: pageData,
            theme: "grid",
            margin: { left: margin, right: margin },
            styles: {
              fontSize: 8,
              cellPadding: 2,
              halign: "center",
              valign: "middle",
              overflow: "linebreak",
            },
            headStyles: {
              fillColor: [200, 200, 200],
              textColor: [0, 0, 0],
              fontSize: 9,
            },
            columnStyles: {
              0: { cellWidth: columnWidths[0], halign: "center" },
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
                const rowIndex = data.row.index;
                const recipe =
                  exportData[rowIndex + pageIndex * maxRowsPerPage];
                if (recipe && recipe.imageBase64) {
                  doc.addImage(
                    recipe.imageBase64,
                    "JPEG",
                    data.cell.x + 1,
                    data.cell.y + 1,
                    18,
                    18
                  );
                }
              }
            },
            didDrawPage: function (data) {
              doc.setFontSize(8);
              doc.text(
                `Page ${pageIndex + 1} of ${pageRows.length}`,
                pageWidth - margin,
                pageHeight - 5,
                { align: "right" }
              );
            },
          });
        });

        // Save and cleanup
        doc.save("Recipe.pdf");
        loading.hide();
      },
    })
    .dxDataGrid("instance");
});
