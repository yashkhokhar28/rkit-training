$(document).ready(() => {
  console.log("Document is Ready");

  var customStore = new DevExpress.data.CustomStore({
    loadMode: "raw",
    load: () => {
      return $.getJSON(
        "https://67a9f61d65ab088ea7e526d8.mockapi.io/loadoptions"
      )
        .then((data) => {
          console.log("MockAPI Response:", data);
          return data; // Ensure the data has the `productCategory` field
        })
        .fail((error) => {
          console.error("Error fetching data:", error);
          return [];
        });
    },
  });

  customStore
    .load({
      group: { selector: "productCategory", requireGroupCount: true },
    })
    .done((d) => {
      console.log(d);

      selectBox1.option("dataSource", d);
    });

  customStore
    .load({
      skip: 1,
      take: 10,
      // not working
      searchExpr: ["productName"],
      searchOperation: "endswith",
      searchValue: "product name",
      requireTotalCount: true,
    })
    .done((d) => {
      selectBox2.option("dataSource", d);
    });

  var selectBox1 = $("#SelectBox1")
    .dxSelectBox({
      displayExpr: "productName",
      valueExpr: "id",
      grouped: true,
      groupTemplate(data) {
        console.log("Group Data:", data);
        return $("<div>")
          .addClass("custom-group")
          .html(`<strong>${data.key}</strong>`);
      },
    })
    .dxSelectBox("instance");

  var selectBox2 = $("#SelectBox2")
    .dxSelectBox({
      acceptCustomValue: true,
      showClearButton: true,
      displayExpr: "productName",
      valueExpr: "id",
    })
    .dxSelectBox("instance");

  var dataGrid = $("#DataGrid").dxDataGrid({
    dataSource: customStore,
    showBorders: true,
    columns: [
      { dataField: "productName", caption: "Product Name" },
      {
        dataField: "productCategory",
        caption: "Product Category",
        groupIndex: 0, // Group by this column
      },
      { dataField: "productPrice", caption: "Product Price" },
    ],
    groupSummary: [
      {
        selector: "productCategory",
        summaryType: "count", // Count the number of items in each group
        alignByColumn: true,
      },
    ],
    summary: {
      groupItems: [
        {
          column: "productCategory",
          summaryType: "count",
          displayFormat: "{0} Category",
        },
      ],
      totalItems: [
        {
          column: "productName",
          summaryType: "count",
        },
      ],
    },
  });
});
