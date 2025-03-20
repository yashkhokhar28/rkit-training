$(function () {
  var dataGrid = $("#dataGridContainer")
    .dxDataGrid({
      dataSource: marvelCharacters,
      columnChooser: {
        enabled: false,
      },
      columns: [
        { dataField: "Name", caption: "Name", visible: true },
        { dataField: "Real_Name", caption: "Real Name", visible: true },
        {
          dataField: "Affiliation",
          caption: "Affiliation",
          visible: true,
        },
        {
          dataField: "First_Appearance",
          caption: "First Appearance",
          visible: true,
        },
        { dataField: "Abilities", caption: "Abilities", visible: true },
        { dataField: "Status", caption: "Status", visible: true },
      ],
      onToolbarPreparing: (e) => {
        e.toolbarOptions.items.push({
          widget: "dxButton",
          options: {
            icon: "columnchooser",
            onClick: () => {
              $("#customColumnChooser").dxPopup("show");
            },
          },
          location: "after",
        });
      },
    })
    .dxDataGrid("instance");

  var getAllColumns = (dataGrid) => {
    var columnCount = dataGrid.columnCount();
    var columns = [];
    for (let i = 0; i < columnCount; i++) {
      var column = dataGrid.columnOption(i);
      columns.push({
        dataField: column.dataField,
        caption: column.caption,
        visible: column.visible,
      });
    }
    return columns;
  };

  $("#customColumnChooser").dxPopup({
    title: "Custom Column Chooser",
    closeOnOutsideClick: true,
    maxHeight: 500,
    minHeight: 300,
    contentTemplate: (contentElement) => {
      var $searchBox = $("<div>").dxTextBox({
        placeholder: "Search columns...",
        valueChangeEvent: "input",
        onValueChanged: (e) => {
          var searchValue = e.value.trim();
          var allColumns = getAllColumns(dataGrid);
          var filteredColumns = searchValue
            ? allColumns.filter((column) =>
                column.caption.includes(searchValue)
              ) // Case-sensitive
            : allColumns;
          updateColumnList(filteredColumns, searchValue);
        },
      });

      var $columnList = $("<div>").dxList({
        selectionMode: "multiple",
        keyExpr: "dataField",
        displayExpr: "caption",
        showSelectionControls: true,
        scrollingEnabled: false,
      });

      var $applyButton = $("<div>").dxButton({
        text: "Apply",
        onClick: () => {
          var listInstance = $columnList.dxList("instance");
          var selectedKeys = listInstance.option("selectedItemKeys");
          var allColumns = getAllColumns(dataGrid);
          allColumns.forEach((column) => {
            dataGrid.columnOption(
              column.dataField,
              "visible",
              selectedKeys.includes(column.dataField)
            );
          });
          $("#customColumnChooser").dxPopup("hide");
        },
      });

      contentElement.append($searchBox);
      contentElement.append($columnList);
      contentElement.append($applyButton);

      var updateColumnList = (columns, searchValue) => {
        var listInstance = $columnList.dxList("instance");

        var items = columns.map((column) => {
          var highlightedText = searchValue
            ? column.caption.replace(
                new RegExp(searchValue, "g"), // Case-sensitive highlighting
                (match) => `<span class='highlight'>${match}</span>`
              )
            : column.caption;
          return {
            dataField: column.dataField,
            caption: highlightedText,
            visible: column.visible,
          };
        });

        listInstance.option("items", items);

        setTimeout(() => {
          listInstance.option(
            "selectedItemKeys",
            items.filter((i) => i.visible).map((i) => i.dataField)
          );
        }, 0);

        $columnList.find(".dx-item-content").each(function (index) {
          if (items[index]) {
            console.log(items[index]);

            $(this).html(items[index].caption);
          }
        });
      };

      var allColumns = getAllColumns(dataGrid);
      updateColumnList(allColumns, "");
    },
  });
});
