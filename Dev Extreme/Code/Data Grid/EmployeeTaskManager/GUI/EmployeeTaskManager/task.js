$(() => {
  console.log("Document is Ready!!");

  const APIURL = "http://localhost:5210/api/CLTask";
  const EmployeeAPIURL = "http://localhost:5210/api/CLEmployee";
  const DepartmentAPIURL = "http://localhost:5210/api/CLDepartment";

  const statusOptions = [
    { value: 0, text: "Pending" },
    { value: 1, text: "In Progress" },
    { value: 2, text: "Completed" },
    { value: 3, text: "Overdue" },
  ];
  const priorityOptions = [
    { value: 0, text: "Low" },
    { value: 1, text: "Medium" },
    { value: 2, text: "High" },
  ];

  var DisplayMessage = (message, type, displayTime) => {
    DevExpress.ui.notify(message, type, displayTime);
  };

  var taskStore = new DevExpress.data.CustomStore({
    key: "k01F01",

    load: (loadOptions) => {
      let params = {
        // skip: loadOptions.skip || 0,
        take: loadOptions.take || 10,
        // filter: loadOptions.filter ? JSON.stringify(loadOptions.filter) : null,
        sort: loadOptions.sort ? JSON.stringify(loadOptions.sort) : null,
      };
      return $.ajax({
        url: APIURL,
        method: "GET",
        data: params,
      }).then(
        (result) => {
          console.log("Task Load Response:", result);
          DisplayMessage(
            result.message,
            result.isError ? "error" : "success",
            1000
          );
          if (result.isError) {
            throw result.message;
          }
          return result.data;
        },
        (xhr) => {
          throw "Network error: " + xhr.statusText;
        }
      );
    },

    byKey: (key) => {
      return $.ajax({
        url: `${APIURL}/ID?ID=${key}`,
        method: "GET",
      }).then(
        (result) => {
          console.log("byKey Response for ID " + key + ":", result);
          if (result.isError) {
            DisplayMessage(result.message, "error", 1000);
            throw result.message;
          }
          const task = result.data[0];
          if (!task || typeof task !== "object") {
            throw "Invalid task data returned from API";
          }
          return task;
        },
        (xhr) => {
          throw "Network error: " + xhr.statusText;
        }
      );
    },

    insert: (values) => {
      const dtoTask = {
        K01102: values.k01F02 !== undefined ? values.k01F02 : "",
        K01103: values.k01F03 !== undefined ? values.k01F03 : "",
        K01104: values.k01F04 !== undefined ? values.k01F04 : 0, // Employee ID from lookup
        K01105: values.k01F05 !== undefined ? values.k01F05 : 0, // Department ID from lookup
        K01106: values.k01F06 !== undefined ? values.k01F06 : 0,
        K01107: values.k01F07 !== undefined ? values.k01F07 : 0,
        K01108:
          values.k01F08 !== undefined
            ? values.k01F08
            : new Date().toISOString(),
      };
      console.log("Insert Payload:", dtoTask);
      return $.ajax({
        url: APIURL,
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(dtoTask),
      }).then(
        (result) => {
          DisplayMessage(
            result.message,
            result.isError ? "error" : "success",
            1000
          );
          if (result.isError) {
            throw result.message;
          }
          return result.Data;
        },
        (xhr) => {
          throw "Network error: " + xhr.statusText;
        }
      );
    },

    update: (key, values) => {
      return taskStore.byKey(key).then(
        (existingTask) => {
          console.log("Existing Task for Update:", existingTask);
          const dtoTask = {
            K01101: key,
            K01102:
              values.k01F02 !== undefined ? values.k01F02 : existingTask.k01F02,
            K01103:
              values.k01F03 !== undefined ? values.k01F03 : existingTask.k01F03,
            K01104:
              values.k01F04 !== undefined ? values.k01F04 : existingTask.k01F04,
            K01105:
              values.k01F05 !== undefined ? values.k01F05 : existingTask.k01F05,
            K01106:
              values.k01F06 !== undefined ? values.k01F06 : existingTask.k01F06,
            K01107:
              values.k01F07 !== undefined ? values.k01F07 : existingTask.k01F07,
            K01108:
              values.k01F08 !== undefined ? values.k01F08 : existingTask.k01F08,
          };
          console.log("Update Payload:", dtoTask);
          return $.ajax({
            url: APIURL,
            method: "PUT",
            contentType: "application/json",
            data: JSON.stringify(dtoTask),
          }).then(
            (result) => {
              DisplayMessage(
                result.message,
                result.isError ? "error" : "success",
                1000
              );
              if (result.isError) {
                throw result.message;
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

    remove: (key) => {
      return $.ajax({
        url: `${APIURL}/ID?ID=${key}`,
        method: "DELETE",
      }).then(
        (result) => {
          console.log("Delete Response:", result);
          DisplayMessage(
            result.message,
            result.isError ? "error" : "success",
            1000
          );
          if (result.isError) {
            throw result.message;
          }
        },
        (xhr) => {
          throw "Network error: " + xhr.statusText;
        }
      );
    },
  });

  var employeeStore = new DevExpress.data.CustomStore({
    key: "p01F01",

    load: () => {
      return $.ajax({
        url: EmployeeAPIURL,
        method: "GET",
      }).then(
        (result) => {
          console.log("Employee API Response:", result);
          if (result.isError) {
            DisplayMessage(result.message, "error", 1000);
            throw result.message;
          }
          return result.data;
        },
        (xhr) => {
          throw "Network error: " + xhr.statusText;
        }
      );
    },

    byKey: (key) => {
      return $.ajax({
        url: `${EmployeeAPIURL}/ID?ID=${key}`,
        method: "GET",
      }).then(
        (result) => {
          console.log("byKey Response for ID " + key + ":", result);
          if (result.isError) {
            DisplayMessage(result.message, "error", 1000);
            throw result.message;
          }
          const task = result.data[0];
          if (!task || typeof task !== "object") {
            throw "Invalid task data returned from API";
          }
          return task;
        },
        (xhr) => {
          throw "Network error: " + xhr.statusText;
        }
      );
    },
  });

  var departmentStore = new DevExpress.data.CustomStore({
    key: "t01F01",

    load: () => {
      return $.ajax({
        url: DepartmentAPIURL,
        method: "GET",
      }).then(
        (result) => {
          console.log("Department API Response:", result);
          if (result.isError) {
            DisplayMessage(result.message, "error", 1000);
            throw result.message;
          }
          return result.data;
        },
        (xhr) => {
          throw "Network error: " + xhr.statusText;
        }
      );
    },

    byKey: (key) => {
      return $.ajax({
        url: `${DepartmentAPIURL}/ID?ID=${key}`,
        method: "GET",
      }).then(
        (result) => {
          console.log("byKey Response for ID " + key + ":", result);
          if (result.isError) {
            DisplayMessage(result.message, "error", 1000);
            throw result.message;
          }
          const task = result.data[0];
          if (!task || typeof task !== "object") {
            throw "Invalid task data returned from API";
          }
          return task;
        },
        (xhr) => {
          throw "Network error: " + xhr.statusText;
        }
      );
    },
  });

  $("#taskGrid").dxDataGrid({
    dataSource: taskStore,
    customizeColumns: (columns) => {
      columns[0].width = 100;
    },
    showBorders: true,
    columnAutoWidth: true,
    // Paging and Scrolling
    paging: { pageSize: 10 },
    pager: {
      visible: true,
      showPageSizeSelector: true,
      allowedPageSizes: [5, 10, 20],
      showInfo: true,
    },
    scrolling: { mode: "virtual" },
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
        lookup: {
          dataSource: employeeStore,
          valueExpr: "p01F01", // Employee ID
          displayExpr: (employee) =>
            employee
              ? `${employee.p01F02} ${employee.p01F03 || ""}`.trim()
              : "",
        },
      },
      {
        dataField: "k01F05",
        dataType: "number",
        caption: "Department",
        lookup: {
          dataSource: departmentStore,
          valueExpr: "t01F01", // Department ID
          displayExpr: "t01F02", // Department Name
        },
      },
      {
        dataField: "k01F06",
        caption: "Status",
        lookup: {
          dataSource: statusOptions,
          valueExpr: "value",
          displayExpr: "text",
        },
      },
      {
        dataField: "k01F07",
        caption: "Priority",
        lookup: {
          dataSource: priorityOptions,
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

  taskStore.load({
    sort: [{ selector: "k01F01", desc: true }],
  });
});
