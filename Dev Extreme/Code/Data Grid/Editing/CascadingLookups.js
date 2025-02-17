$(() => {
  $("#gridContainer").dxDataGrid({
    dataSource: cities,
    showBorders: true,
    editing: {
      allowUpdating: true,
      allowAdding: true,
      mode: "row",
    },
    onEditorPreparing(e) {
      // Disable the City field if State is not selected
      if (e.parentType === "dataRow" && e.dataField === "CityID") {
        const isStateNotSet = !e.row.data.StateID === undefined;
        e.editorOptions.disabled = isStateNotSet;
      }

      // Disable the State field if Country is not selected
      if (e.parentType === "dataRow" && e.dataField === "StateID") {
        const isCountryNotSet = !e.row.data.CountryID === undefined;
        e.editorOptions.disabled = isCountryNotSet;

        // Filter states based on the selected Country
        if (e.row.data.CountryID) {
          e.editorOptions.dataSource = states.filter(
            (state) => state.CountryID === e.row.data.CountryID
          );
        } else {
          e.editorOptions.dataSource = states; // Show all states if no country is selected
        }
      }
    },
    columns: [
      {
        dataField: "CountryID",
        caption: "Country",
        setCellValue(rowData, value) {
          rowData.CountryID = value;
          rowData.StateID = null;
          rowData.CityID = null;
        },
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
          dataSource: states, // Initially all states will be shown
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
