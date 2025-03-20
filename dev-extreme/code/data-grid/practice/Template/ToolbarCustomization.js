$(() => {
  var store = new DevExpress.data.CustomStore({
    load() {
      return $.ajax({
        url: "https://dummyjson.com/posts?limit=291",
        dataType: "json",
      })
        .then((result) => {
          return { data: result.posts, totalCount: result.total };
        })
        .catch((error) => {
          console.error("Data Loading Error:", error);
          return { data: [], totalCount: 0 };
        });
    },
  });

  let dataGrid = $("#gridContainer")
    .dxDataGrid({
      dataSource: store,
      showBorders: true,
      wordWrapEnabled: true,
      onToolbarPreparing: function (e) {
        e.toolbarOptions.items.push({
          location: "before",
          template: () =>
            $("<div>")
              .attr("id", "totalCountDisplay")
              .text("Total Posts: Loading..."),
        });
      },
      onContentReady: function (e) {
        let dataGridInstance = e.component;
        let dataSource = dataGridInstance.getDataSource();
        if (dataSource) {
          let totalCount = dataSource.totalCount();
          console.log("Total Count (After Load):", totalCount);
          $("#totalCountDisplay").text(`Total Posts: ${totalCount}`);
        }
      },
      columns: [
        { dataField: "id", caption: "ID", width: 50, alignment: "center" },
        { dataField: "title", caption: "Title", width: 200 },
        { dataField: "body", caption: "Body", width: 770, alignment: "left" },
        {
          dataField: "views",
          caption: "Views",
          width: 100,
          alignment: "center",
        },
        {
          dataField: "reactions.likes",
          caption: "Likes",
          width: 100,
          alignment: "center",
        },
        {
          dataField: "reactions.dislikes",
          caption: "Dislikes",
          width: 100,
          alignment: "center",
        },
      ],
    })
    .dxDataGrid("instance");
});
