$(() => {
  const store = new DevExpress.data.CustomStore({
    load() {
      return $.ajax({
        url: "https://dummyjson.com/posts",
        dataType: "json",
      })
        .then((result) => {
          return result.posts;
        })
        .catch((error) => {
          console.error("Data Loading Error:", error);
          return [];
        });
    },
  });

  const dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: store,
      showBorders: true,
      keyExpr: "id",
      dataRowTemplate: (container, items) => {
        console.log("Row Template Called:", items);
        console.log(items);
      },
      columns: [
        {
          dataField: "title",
          dataType: "string",
          caption: "Title",
        },
        { dataField: "body", dataType: "string", caption: "Body" },
        {
          dataField: "tags",
          dataType: "string",
          caption: "Tags",
        },
        {
          dataField: "reactions.likes",
          dataType: "number",
          caption: "Likes",
        },
        {
          dataField: "reactions.dislikes",
          dataType: "number",
          caption: "DisLikes",
        },
        {
          dataField: "views",
          dataType: "number",
          caption: "Views",
        },
      ],
    })
    .dxDataGrid("instance");
});
