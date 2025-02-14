$(() => {
  $("#gridContainer").dxDataGrid({
    dataSource: cities, // Should be cities if managing city-level data
    showBorders: true,
    editing: {
      allowUpdating: true,
      allowAdding: true,
      mode: "row",
    },
    onEditorPreparing(e) {
      if (e.parentType === "dataRow" && e.dataField === "CityID") {
        const isStateNotSet = !e.row.data.StateID;
        e.editorOptions.disabled = isStateNotSet;
      }
      if (e.parentType === "dataRow" && e.dataField === "StateID") {
        const isCountryNotSet = !e.row.data.CountryID;
        e.editorOptions.disabled = isCountryNotSet;
      }
    },
    columns: [
      {
        dataField: "CountryID",
        caption: "Country",
        lookup: {
          dataSource: countries,
          valueExpr: "CountryID",
          displayExpr: "CountryName",
        },
      },
      {
        dataField: "StateID",
        caption: "State",
        setCellValue(rowData, value) {
          rowData.StateID = value;
          rowData.CityID = null; // Reset city when state changes
        },
        lookup: {
          dataSource: states,
          valueExpr: "StateID",
          displayExpr: "StateName",
        },
      },
      {
        dataField: "CityID",
        caption: "City",
        lookup: {
          dataSource(options) {
            if (!options.data || !options.data.StateID) {
              return cities; // Return all cities if no state is selected
            }
            return {
              store: cities,
              filter: ["StateID", "=", options.data.StateID],
            };
          },
          valueExpr: "CityID",
          displayExpr: "CityName",
        },
      },
    ],
  });
});
