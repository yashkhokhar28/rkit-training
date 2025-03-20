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
    rowTemplate(container, item) {
      const { data } = item;
      const $rows = $(`  
        <tr class="dx-row main-row">
          <td class="dx-cell-focus-disabled" style="text-align: center;">
            ${data.id}
          </td>
          <td class="dx-cell-focus-disabled">
            ${data.title}
          </td>
          <td class="dx-cell-focus-disabled">
            ${data.body}
          </td>
          <td class="dx-cell-focus-disabled" style="text-align: center;">
            👀 ${data.views || 0}
          </td>
          <td class="dx-cell-focus-disabled" style="text-align: center;">
            👍 ${data.reactions?.likes || 0}
          </td>
          <td class="dx-cell-focus-disabled" style="text-align: center;">
            👎 ${data.reactions?.dislikes || 0}
          </td>
        </tr>
      `);
      container.append($rows);
    },
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
        width: 200,
      },
      {
        dataField: "body",
        caption: "Body",
        width: 800,
        alignment: "left",
      },
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
    onRowPrepared(e) {
      if (e.rowType === "data") {
        e.rowElement.addClass("dx-row-focused");
      }
    },
  });
});
