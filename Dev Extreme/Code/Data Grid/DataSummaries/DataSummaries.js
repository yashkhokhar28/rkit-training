$(() => {
  let dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: orderData,
      showBorders: true,
      wordWrapEnabled: true,
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
      summary: {
        totalItems: [
          {
            column: "orderId",
            summaryType: "count",
            alignment: "center",
            customizeText(itemInfo) {
              return `Total Order : ${itemInfo.value}`;
            },
          },
          {
            column: "totalAmount",
            summaryType: "sum",
            valueFormat: {
              style: "currency",
              currency: "INR",
              useGrouping: true,
            },
          },
          {
            column: "deliveryDate",
            summaryType: "min",
            customizeText(itemInfo) {
              return `First: ${DevExpress.localization.formatDate(
                itemInfo.value,
                "MMM dd, yyyy"
              )}`;
            },
          },
        ],
      },
    })
    .dxDataGrid("instance");
});
