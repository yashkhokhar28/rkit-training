$(document).ready(() => {
  console.log("Document is Ready");

  const APIURL = "https://67a9f61d65ab088ea7e526d8.mockapi.io/users";

  var customStore = new DevExpress.data.CustomStore({
    key: "id",

    load: () => {
      return $.getJSON(APIURL)
        .then((data) => {
          console.log("Data Loaded:", data);
          return data;
        })
        .fail((error) => {
          console.error("Error fetching data:", error);
          return [];
        });
    },

    insert: (values) => {
      return $.ajax({
        url: APIURL,
        method: "POST",
        data: values,
      })
        .then((response) => {
          console.log("Inserted:", response);
          customStore.load();
        })
        .fail((error) => {
          console.error("Error inserting data:", error);
        });
    },

    update: (key, values) => {
      return $.ajax({
        url: `${APIURL}/${key}`,
        method: "PUT",
        data: values,
      })
        .then((response) => {
          console.log("Updated:", response);
          customStore.load();
        })
        .fail((error) => {
          console.error("Error updating data:", error);
        });
    },

    remove: (key) => {
      return $.ajax({
        url: `${APIURL}/${key}`,
        method: "DELETE",
      })
        .then((response) => {
          console.log("Deleted:", response);
          customStore.load();
        })
        .fail((error) => {
          console.error("Error deleting data:", error);
        });
    },

    loadMode: "raw",
    errorHandler: (error) => {
      alert(error.message);
    },
  });

  customStore.load();

  const dataGrid = $("#DataGrid")
    .dxDataGrid({
      dataSource: customStore,
      customizeColumns: (columns) => {
        columns[0].width = 100;
      },
      showBorders: true,
      keyExpr: "id",
      columnHidingEnabled: true,
      allowColumnResizing: true,
      columnResizingMode: "widget",
      remoteOperations: false,
      selection: {
        mode: "multiple",
        selectAllMode: "page",
        showCheckBoxesMode: "always",
        allowSelectAll: true,
      },
      paging: {
        enabled: true,
        pageSize: 10,
        pageIndex: 0,
      },
      pager: {
        displayMode: "compact",
        visible: true,
        showPageSizeSelector: true,
        allowedPageSizes: [10, 20, "all"],
        showInfo: true,
        label: "Navigation",
        infoText: "Page {0} of {1} ({2} items)",
      },
      groupPanel: {
        visible: true,
      },
      grouping: {
        autoExpandAll: false,
      },
      filterRow: {
        visible: true,
        applyFilter: "auto",
      },
      sorting: {
        mode: "multiple",
      },
      summary: {
        totalItems: [{ column: "id", summaryType: "count" }],
        groupItems: [{ column: "Department", summaryType: "count" }],
      },
      editing: {
        mode: "popup",
        allowUpdating: true,
        allowAdding: true,
        allowDeleting: true,
        popup: {
          title: "User Details",
          showTitle: true,
          width: 700,
          height: 400,
        },
      },
      columns: [
        {
          dataField: "id",
          caption: "ID",
          dataType: "number",
          allowGrouping: false,
          allowSorting: true,
        },
        {
          dataField: "FullName",
          caption: "Full Name",
          dataType: "string",
          validationRules: [{ type: "required" }],
        },
        {
          dataField: "Email",
          caption: "Email",
          dataType: "string",
          validationRules: [{ type: "email" }, { type: "required" }],
        },
        {
          dataField: "PhoneNumber",
          caption: "Phone Number",
          dataType: "string",
          validationRules: [{ type: "required" }],
        },
        {
          dataField: "Department",
          caption: "Department",
          dataType: "string",
          validationRules: [{ type: "required" }],
        },
      ],
    })
    .dxDataGrid("instance");
});
