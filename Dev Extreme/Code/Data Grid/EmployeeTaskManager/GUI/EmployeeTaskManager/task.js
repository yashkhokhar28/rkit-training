$(() => {
  console.log("Document is Ready!!");

  const APIURL = "http://localhost:5210/api/CLTask";

  var DisplayMessage = (message, type, displayTime) => {
    DevExpress.ui.notify(message, type, displayTime);
  };

  var taskStore = new DevExpress.data.CustomStore({
    key: "k01F01",

    load: () => {
      return $.ajax({
        url: APIURL,
        method: "GET",
        success: (result) => {
          console.log(result);

          if (result.IsError) {
            DisplayMessage(result.message, "error", 1000);
          } else {
            DisplayMessage(result.message, "success", 1000);
          }
          return result.Data;
        },
        error: function (xhr) {
          throw "Network error: " + xhr.statusText;
        },
      });
    },

    // Insert a new record
    insert: (values) => {
      const dtoTask = {
        K01102: values.k01F02 !== undefined ? values.k01F02 : "",
        K01103: values.k01F03 !== undefined ? values.k01F03 : "",
        K01104: values.k01F04 !== undefined ? values.k01F04 : 0,
        K01105: values.k01F05 !== undefined ? values.k01F05 : 0,
        K01106: values.k01F06 !== undefined ? values.k01F06 : 0,
        K01107: values.k01F07 !== undefined ? values.k01F07 : 0,
        K01108:
          values.k01F08 !== undefined
            ? values.k01F08
            : new Date().toISOString(),
      };
      return $.ajax({
        url: APIURL,
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(dtoTask),
        success: (result) => {
          if (result.IsError) {
            DisplayMessage(result.message, "error", 1000);
          } else {
            DisplayMessage(result.message, "success", 1000);
          }
        },
      });
    },

    // Update an existing record
    update: (key, values) => {
      return $.ajax({
        url: `${APIURL}/ID?ID=${key}`,
        method: "GET",
      }).then(
        (result) => {
          console.log("GET Response for ID " + key + ":", result);
          if (result.IsError) {
            DisplayMessage(result.Message, "error", 1000);
            throw result.Message;
          }
          // Extract the first task from the array in result.Data
          const existingTask = result.data[0];
          if (!existingTask || typeof existingTask !== "object") {
            throw "Invalid task data returned from API";
          }
          const dtoTask = {
            K01101: key,
            K01102:
              values.k01F02 != undefined ? values.k01F02 : existingTask.k01F02,
            K01103:
              values.k01F03 != undefined ? values.k01F03 : existingTask.k01F03,
            K01104:
              values.k01F04 != undefined ? values.k01F04 : existingTask.k01F04,
            K01105:
              values.k01F05 != undefined ? values.k01F05 : existingTask.k01F05,
            K01106:
              values.k01F06 != undefined ? values.k01F06 : existingTask.k01F06,
            K01107:
              values.k01F07 != undefined ? values.k01F07 : existingTask.k01F07,
            K01108:
              values.k01F08 != undefined ? values.k01F08 : existingTask.k01F08,
          };
          return $.ajax({
            url: APIURL,
            method: "PUT",
            contentType: "application/json",
            data: JSON.stringify(dtoTask),
          }).then(
            (result) => {
              DisplayMessage(
                result.Message,
                result.IsError ? "error" : "success",
                1000
              );
              if (result.IsError) {
                throw result.Message;
              }
            },
            (xhr) => {
              throw "Network error: " + xhr.statusText;
            }
          );
        },
        (xhr) => {
          throw "Network error: " + xhr.statusText;
        }
      );
    },

    // Delete a record
    remove: (key) => {
      return $.ajax({
        url: `${APIURL}/ID?ID=${key}`,
        method: "DELETE",
        success: (result) => {
          console.log(result);
          if (result.isError) {
            DisplayMessage(result.message, "error", 3000);
          } else {
            DisplayMessage(result.message, "success", 1000);
          }
        },
      });
    },
  });

  var dataGridInstance = $("#taskGrid").dxDataGrid({
    dataSource: taskStore,
    customizeColumns: (columns) => {
      columns[0].width = 100;
    },
    showBorders: true,
    columnAutoWidth: true,
    filterRow: {
      visible: true,
    },
    sorting: {
      mode: "multiple",
    },
    editing: {
      mode: "form",
      allowAdding: true,
      allowUpdating: true,
      allowDeleting: true,
    },
    columns: [
      {
        dataField: "k01F01",
        dataType: "number",
        caption: "Task ID",
      },
      {
        dataField: "k01F02",
        dataType: "string",
        caption: "Title",
      },
      {
        dataField: "k01F03",
        dataType: "string",
        caption: "Description",
      },
      {
        dataField: "k01F04",
        dataType: "number",
        caption: "Assigned To",
      },
      {
        dataField: "k01F05",
        dataType: "number",
        caption: "Department",
      },
      {
        dataField: "k01F06",
        caption: "Status",
        lookup: {
          dataSource: [
            { value: 0, text: "Pending" },
            { value: 1, text: "In Progress" },
            { value: 2, text: "Completed" },
            { value: 3, text: "Overdue" },
          ],
          valueExpr: "value",
          displayExpr: "text",
        },
      },
      {
        dataField: "k01F07",
        caption: "Priority",
        lookup: {
          dataSource: [
            { value: 0, text: "Low" },
            { value: 1, text: "Medium" },
            { value: 2, text: "High" },
          ],
          valueExpr: "value",
          displayExpr: "text",
        },
      },
      {
        dataField: "k01F08",
        dataType: "date",
        caption: "Due Date",
        format: "dd-MM-yyyy",
      },
    ],
  });

  taskStore.load();
});
