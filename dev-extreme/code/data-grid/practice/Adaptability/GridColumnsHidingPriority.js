$(() => {
  $("#gridContainer").dxDataGrid({
    dataSource: orderData,
    keyExpr: "orderId",
    columnHidingEnabled: true,
    showRowLines: true,
    showBorders: true,
    showColumnLines: false,
    onCellPrepared(options) {
      if (options.column.dataField === "orderStatus") {
        let statusColors = {
          Shipped: "green",
          Pending: "orange",
          Cancelled: "red",
          Delivered: "white",
        };
        options.cellElement
          .css("color", statusColors[options.value])
          .css("font-weight", "bold");
      }

      if (options.column.dataField === "discount" && options.value > 0) {
        options.cellElement.html(
          `<span class="discount-badge">-${options.value}%</span>`
        );
        options.cellElement.css("color", "red").css("font-weight", "bold");
      } else {
        if (options.column.dataField === "discount" && options.value == 0) {
          options.cellElement.html(
            `<span class="discount-badge">${options.value}%</span>`
          );
          options.cellElement.css("color", "white").css("font-weight", "bold");
        }
      }

      if (options.column.dataField === "totalAmount") {
        options.cellElement.html(
          DevExpress.localization.formatNumber(options.value, {
            type: "currency",
            currency: "USD",
            precision: 2,
          })
        );
        options.cellElement.css("font-weight", "bold");
      }

      if (options.column.dataField === "deliveryDate") {
        let today = new Date();
        let deliveryDate = new Date(options.value);
        if (deliveryDate < today) {
          options.cellElement.css("color", "red").css("font-weight", "bold");
        }
      }
    },
    columns: [
      {
        dataField: "orderId",
        caption: "Order ID",
        hidingPriority: 0,
      },
      {
        dataField: "customer",
        caption: "Customer Name",
        hidingPriority: 2,
      },
      {
        dataField: "orderStatus",
        caption: "Status",
        hidingPriority: 4,
      },
      {
        dataField: "totalAmount",
        caption: "Total Amount",
        hidingPriority: 1,
      },
      {
        dataField: "discount",
        caption: "Discount",
        hidingPriority: 3,
      },
      {
        dataField: "deliveryDate",
        caption: "Delivery Date",
        dataType: "date",
        hidingPriority: 5,
      },
    ],
  });
});
