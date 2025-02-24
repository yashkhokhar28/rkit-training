$(() => {
  $("#gridContainer").dxDataGrid({
    dataSource: cities,
    showBorders: true,
    editing: {
      allowUpdating: true,
      allowAdding: true,
      mode: "row",
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
          rowData.CityID = null;
        },
        lookup: {
          dataSource(options) {
            if (!options.data || !options.data.CountryID) {
              return [];
            }
            return {
              store: states,
              filter: ["CountryID", "=", options.data.CountryID],
            };
          },
          valueExpr: "StateID",
          displayExpr: "StateName",
        },
        calculateDisplayValue: function (rowData) {
          var state = states.find((s) => s.StateID === rowData.StateID);
          return state ? state.StateName : "";
        },
      },
      {
        dataField: "CityID",
        caption: "City",
        lookup: {
          dataSource(options) {
            if (!options.data || !options.data.StateID) {
              return []; // Return all cities if no state is selected
            }
            return {
              store: cities,
              filter: ["StateID", "=", options.data.StateID],
            };
          },
          valueExpr: "CityID",
          displayExpr: "CityName",
        },
        calculateDisplayValue: function (rowData) {
          var city = cities.find((c) => c.CityID === rowData.CityID);
          return city ? city.CityName : "";
        },
      },
    ],
  });
});
