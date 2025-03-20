$(() => {
  $("#gridContainer").dxDataGrid({
    dataSource: new DevExpress.data.CustomStore({
      load() {
        return $.ajax({
          url: "https://dummyjson.com/posts",
          dataType: "json",
        })
          .then((result) => result.posts)
          .catch((error) => {
            console.error("Data Loading Error:", error);
            return [];
          });
      },
    }),
    keyExpr: "id",
    wordWrapEnabled: true,
    showBorders: true,
    showColumnLines: true,
    showRowLines: true,
    columns: [
      {
        dataField: "id",
        caption: "ID",
        width: 50,
        alignment: "center",
      },
      {
        dataField: "title",
        caption: "Title",
        cellTemplate(container, options) {
          const titleLink = $("<div>")
            .text(options.value)
            .addClass("title-link")
            .attr("id", `title-${options.data.id}`);

          container.append(titleLink);

          // Create popover for this specific cell
          const popover = $("<div>")
            .attr("id", `popover-${options.data.id}`)
            .dxPopover({
              target: `#title-${options.data.id}`,
              contentTemplate: (contentElement) => {
                contentElement.append($("<div>").text(options.data.body));
              },
              closeOnOutsideClick: true,
              showEvent: "mouseenter",
              hideEvent: "mouseleave",
              position: "top",
              width: 300,
              animation: {
                show: {
                  type: "pop",
                  from: { scale: 0 },
                  to: { scale: 1 },
                },
                hide: {
                  type: "fade",
                  from: 1,
                  to: 0,
                },
              },
            });

          container.append(popover);
        },
      },
      {
        dataField: "views",
        caption: "Views",
        alignment: "center",
      },
      {
        dataField: "reactions.likes",
        caption: "Likes",
        alignment: "center",
      },
      {
        dataField: "reactions.dislikes",
        caption: "Dislikes",
        alignment: "center",
      },
    ],
  });
});
