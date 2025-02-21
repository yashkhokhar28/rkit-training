$(() => {
  let dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: orderData,
      showBorders: true,
      wordWrapEnabled: true,
      selection: {
        mode: "multiple",
        showCheckBoxesMode: "always",
        allowSelectAll: false,
        deferred: true,
      },
      paging: {
        pageSize: 10,
      },
      columns: [
        {
          dataField: "orderId",
          caption: "Order ID",
          alignment: "center",
        },
        {
          dataField: "customer",
          caption: "Customer Name",
          alignment: "center",
        },
        {
          dataField: "orderStatus",
          caption: "Status",
          alignment: "center",
        },
        {
          dataField: "totalAmount",
          caption: "Total Amount",
          alignment: "center",
        },
        {
          dataField: "discount",
          caption: "Discount",
          alignment: "center",
        },
        {
          dataField: "deliveryDate",
          caption: "Delivery Date",
          dataType: "date",
          alignment: "center",
        },
      ],
      onSelectionChanged(e) {
        e.component.refresh(true);
      },
      summary: {
        // recalculateWhileEditing: true,
        totalItems: [
          {
            name: "SelectedRowsSummary",
            summaryType: "custom",
            alignment: "center",
            showInColumn: "totalAmount",
            displayFormat: "Sum: {0}",
          },
        ],
        calculateCustomSummary(options) {
          if (options.name === "SelectedRowsSummary") {
            if (options.summaryProcess === "start") {
              options.totalValue = 0;
            }
            if (options.summaryProcess === "calculate") {
              if (options.component.isRowSelected(options.value)) {
                options.totalValue += options.value.totalAmount;
              }
            }
          }
        },
      },
    })
    .dxDataGrid("instance");
});
