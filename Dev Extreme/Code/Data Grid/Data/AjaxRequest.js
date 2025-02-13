$(() => {
  const store = new DevExpress.data.CustomStore({
    key: "id",
    load() {
      return $.ajax({
        url: "https://dummyjson.com/products",
        dataType: "json",
      })
        .then((result) => {
          return {
            data: result.products,
            totalCount: result.total,
          };
        })
        .fail(() => {
          throw new Error("Data Loading Error");
        });
    },
  });

  $("#gridContainer")
    .dxDataGrid({
      dataSource: store,
      showBorders: true,
      columns: [
        { dataField: "id", dataType: "number", caption: "ID" },
        { dataField: "title", dataType: "string", caption: "Title" },
        {
          dataField: "description",
          dataType: "string",
          caption: "Description",
        },
        {
          dataField: "category",
          dataType: "string",
          caption: "Category",
        },
        {
          dataField: "price",
          dataType: "number",
          format: "currency",
          caption: "Price",
        },
        {
          dataField: "discountPercentage",
          dataType: "number",
          format: "percent",
          caption: "Discount %",
        },
        { dataField: "rating", dataType: "number", caption: "Rating" },
        { dataField: "stock", dataType: "number", caption: "Stock" },
        { dataField: "brand", dataType: "string", caption: "Brand" },
        { dataField: "sku", dataType: "string", caption: "SKU" },
        { dataField: "weight", dataType: "number", caption: "Weight" },
        {
          dataField: "availabilityStatus",
          dataType: "string",
          caption: "Availability",
        },
        {
          dataField: "returnPolicy",
          dataType: "string",
          caption: "Return Policy",
        },
        {
          dataField: "minimumOrderQuantity",
          dataType: "number",
          caption: "Min Order Qty",
        },
      ],
    })
    .dxDataGrid("instance");
});
