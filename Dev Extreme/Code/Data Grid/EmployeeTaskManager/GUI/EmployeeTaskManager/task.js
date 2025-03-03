$(() => {
  console.log("Document is Ready!!");

  const TaskAPIURL = "http://localhost:5210/api/CLTask";
  const DepartmentAPIURL = "http://localhost:5210/api/CLDepartment";
  const AuthAPIURL = "http://localhost:5210/api/CLAuth";

  const statusOptions = [
    { value: 0, text: "Pending" },
    { value: 1, text: "InProgress" },
    { value: 2, text: "Completed" },
    { value: 3, text: "Overdue" },
  ];
  const priorityOptions = [
    { value: 0, text: "Low" },
    { value: 1, text: "Medium" },
    { value: 2, text: "High" },
  ];
  const roleOptions = [
    { value: "Admin", text: "Admin" },
    { value: "Manager", text: "Manager" },
    { value: "Employee", text: "Employee" },
  ];

  var DisplayMessage = (message, type, displayTime) => {
    DevExpress.ui.notify({
      message: message,
      type: type,
      displayTime: displayTime,
      position: { my: "bottom center", at: "bottom center", offset: "0 -20" },
      width: "auto",
      shading: true,
    });
  };

  let token = null;
  let userRole = null;
  let userId = null;

  var getAuthHeader = () => {
    return token ? { Authorization: `Bearer ${token}` } : {};
  };

  // Authentication Store
  const authStore = {
    login: (username, password) => {
      return $.ajax({
        url: `${AuthAPIURL}/login`,
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({ R01102: username, R01103: password }), // Match DTOUSR02
      }).then(
        (result) => {
          if (result.IsError) {
            throw new Error(result.Message);
          }
          // Extract token, userId, and role from the response
          const loginData = result.data[0]; // First item in the data array
          token = loginData.token.token; // Nested token property
          userId = loginData.userID;
          userRole = loginData.role;
          localStorage.setItem("jwtToken", token);
          localStorage.setItem("userRole", userRole);
          localStorage.setItem("userId", userId);
          DisplayMessage("Logged in successfully", "success", 2000);
          $("#loginPanel").hide();
          $("#dashboard").show();
          loadDashboard();
        },
        (xhr) => {
          DisplayMessage(
            "Login failed: " + (xhr.responseJSON?.Message || "Unknown error"),
            "error",
            3000
          );
          throw "Login failed";
        }
      );
    },
  };

  var taskStore = new DevExpress.data.CustomStore({
    key: "k01F01",
    load: (loadOptions) => {
      let params = {
        skip: loadOptions.skip || 0,
        take: loadOptions.take || 10,
        filter: loadOptions.filter ? JSON.stringify(loadOptions.filter) : null,
        sort: loadOptions.sort ? JSON.stringify(loadOptions.sort) : null,
      };
      return $.ajax({
        url: TaskAPIURL,
        method: "GET",
        data: params,
        headers: getAuthHeader(),
      }).then(
        (result) => {
          console.log("Task Load Response:", result);
          DisplayMessage(
            result.Message,
            result.IsError ? "error" : "success",
            1000
          );
          if (result.IsError) {
            throw result.Message;
          }
          return { data: result.data, totalCount: result.totalCount };
        },
        (xhr) => {
          DisplayMessage(
            "Failed to load tasks: " +
              (xhr.responseJSON?.Message || xhr.statusText),
            "error",
            2000
          );
          throw "Network error: " + xhr.statusText;
        }
      );
    },

    byKey: (key) => {
      return $.ajax({
        url: `${TaskAPIURL}/${key}`,
        method: "GET",
        headers: getAuthHeader(),
      }).then((result) => {
        if (result.IsError) {
          DisplayMessage(result.Message, "error", 2000);
          throw result.Message;
        }
        return result.Data[0];
      });
    },

    insert: (values) => {
      const dtoTask = {
        K01102: values.k01F02 || "",
        K01103: values.k01F03 || "",
        K01104: values.k01F04 || 0,
        K01105: values.k01F05 || 0,
        K01106: values.k01F06 || 0,
        K01107: values.k01F07 || 0,
        K01108: values.k01F08 || new Date().toISOString(),
      };
      return $.ajax({
        url: TaskAPIURL,
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(dtoTask),
        headers: getAuthHeader(),
      }).then((result) => {
        if (result.IsError) {
          DisplayMessage(result.Message, "error", 3000);
          throw result.Message;
        }
        DisplayMessage("Task added successfully", "success", 2000);
        return result.Data;
      });
    },

    update: (key, values) => {
      return taskStore.byKey(key).then((existingTask) => {
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
        return $.ajax({
          url: TaskAPIURL,
          method: "PUT",
          contentType: "application/json",
          data: JSON.stringify(dtoTask),
          headers: getAuthHeader(),
        }).then((result) => {
          if (result.IsError) {
            DisplayMessage(result.Message, "error", 3000);
            throw result.Message;
          }
          DisplayMessage("Task updated successfully", "success", 2000);
        });
      });
    },

    remove: (key) => {
      return $.ajax({
        url: `${TaskAPIURL}/${key}`,
        method: "DELETE",
        headers: getAuthHeader(),
      }).then((result) => {
        if (result.IsError) {
          DisplayMessage(result.Message, "error", 3000);
          throw result.Message;
        }
        DisplayMessage("Task deleted successfully", "success", 2000);
      });
    },
  });

  var userStore = new DevExpress.data.CustomStore({
    key: "r01F01",
    load: () => {
      return $.ajax({
        url: AuthAPIURL,
        method: "GET",
        headers: getAuthHeader(),
      }).then((result) => {
        if (result.IsError) {
          DisplayMessage(result.Message, "error", 2000);
          throw result.message;
        }
        return result.data;
      });
    },
    byKey: (key) => {
      return $.ajax({
        url: `${AuthAPIURL}/${key}`,
        method: "GET",
        headers: getAuthHeader(),
      }).then((result) => {
        if (result.IsError) {
          DisplayMessage(result.Message, "error", 2000);
          throw result.message;
        }
        return result.data[0];
      });
    },
    insert: (values) => {
      const dtoUser = {
        R01102: values.r01F02 || "",
        R01103: values.r01F03 || "", // Password hash placeholder
        R01104: values.r01F04 || "Employee",
        R01105: values.r01F05 || "",
        R01106: values.r01F06 || "",
        R01107: values.r01F07 || "",
        R01108: values.r01F08 || 0,
        R01109: values.r01F09 || new Date().toISOString().split("T")[0],
      };
      return $.ajax({
        url: `${AuthAPIURL}/register`,
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(dtoUser),
        headers: getAuthHeader(),
      }).then((result) => {
        if (result.IsError) {
          DisplayMessage(result.Message, "error", 3000);
          throw result.Message;
        }
        DisplayMessage("User added successfully", "success", 2000);
        return result.Data;
      });
    },
    update: (key, values) => {
      return userStore.byKey(key).then((existingUser) => {
        const dtoUser = {
          R01101: key,
          R01102:
            values.r01F02 !== undefined ? values.r01F02 : existingUser.r01F02,
          R01103:
            values.r01F03 !== undefined ? values.r01F03 : existingUser.r01F03,
          R01104:
            values.r01F04 !== undefined ? values.r01F04 : existingUser.r01F04,
          R01105:
            values.r01F05 !== undefined ? values.r01F05 : existingUser.r01F05,
          R01106:
            values.r01F06 !== undefined ? values.r01F06 : existingUser.r01F06,
          R01107:
            values.r01F07 !== undefined ? values.r01F07 : existingUser.r01F07,
          R01108:
            values.r01F08 !== undefined ? values.r01F08 : existingUser.r01F08,
          R01109:
            values.r01F09 !== undefined ? values.r01F09 : existingUser.r01F09,
        };
        return $.ajax({
          url: `${AuthAPIURL}/update`, // Ensure your backend has an update endpoint
          method: "PUT",
          contentType: "application/json",
          data: JSON.stringify(dtoUser),
          headers: getAuthHeader(),
        }).then((result) => {
          if (result.IsError) {
            DisplayMessage(result.Message, "error", 3000);
            throw result.Message;
          }
          DisplayMessage("User updated successfully", "success", 2000);
        });
      });
    },
    remove: (key) => {
      return $.ajax({
        url: `${AuthAPIURL}/${key}`,
        method: "DELETE",
        headers: getAuthHeader(),
      }).then((result) => {
        if (result.IsError) {
          DisplayMessage(result.Message, "error", 3000);
          throw result.Message;
        }
        DisplayMessage("User deleted successfully", "success", 2000);
      });
    },
  });

  var employeeStore = new DevExpress.data.CustomStore({
    key: "p01F01", // Placeholder for employee, now using USR01
    load: () => {
      return userStore.load(); // Use userStore since USR01 combines Users and Employees
    },
    byKey: (key) => {
      return userStore.byKey(key);
    },
  });

  var departmentStore = new DevExpress.data.CustomStore({
    key: "t01F01",
    load: () => {
      return $.ajax({
        url: DepartmentAPIURL,
        method: "GET",
        headers: getAuthHeader(),
      }).then((result) => {
        if (result.IsError) {
          DisplayMessage(result.Message, "error", 2000);
          throw result.message;
        }
        return result.data;
      });
    },
    byKey: (key) => {
      return $.ajax({
        url: `${DepartmentAPIURL}/${key}`,
        method: "GET",
        headers: getAuthHeader(),
      }).then((result) => {
        if (result.IsError) {
          DisplayMessage(result.Message, "error", 2000);
          throw result.message;
        }
        return result.data[0];
      });
    },
    insert: (values) => {
      const dtoDepartment = {
        T01102: values.t01F02 || "",
        T01103: values.t01F03 || 0,
      };
      return $.ajax({
        url: DepartmentAPIURL,
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(dtoDepartment),
        headers: getAuthHeader(),
      }).then((result) => {
        if (result.IsError) {
          DisplayMessage(result.Message, "error", 3000);
          throw result.Message;
        }
        DisplayMessage("Department added successfully", "success", 2000);
        return result.Data;
      });
    },
    update: (key, values) => {
      return departmentStore.byKey(key).then((existingDepartment) => {
        const dtoDepartment = {
          T01101: key,
          T01102:
            values.t01F02 !== undefined
              ? values.t01F02
              : existingDepartment.t01F02,
          T01103:
            values.t01F03 !== undefined
              ? values.t01F03
              : existingDepartment.t01F03,
        };
        return $.ajax({
          url: DepartmentAPIURL,
          method: "PUT",
          contentType: "application/json",
          data: JSON.stringify(dtoDepartment),
          headers: getAuthHeader(),
        }).then((result) => {
          if (result.IsError) {
            DisplayMessage(result.Message, "error", 3000);
            throw result.Message;
          }
          DisplayMessage("Department updated successfully", "success", 2000);
        });
      });
    },
    remove: (key) => {
      return $.ajax({
        url: `${DepartmentAPIURL}/${key}`,
        method: "DELETE",
        headers: getAuthHeader(),
      }).then((result) => {
        if (result.IsError) {
          DisplayMessage(result.Message, "error", 3000);
          throw result.Message;
        }
        DisplayMessage("Department deleted successfully", "success", 2000);
      });
    },
  });

  function loadDashboard() {
    $("#dashboard").dxTabPanel({
      items: [
        {
          title: "Tasks",
          template: () => {
            return $("<div>").dxDataGrid({
              dataSource: taskStore,
              showBorders: true,
              columnAutoWidth: true,
              paging: { pageSize: 10 },
              pager: {
                visible: true,
                showPageSizeSelector: true,
                allowedPageSizes: [5, 10, 20],
                showInfo: true,
              },
              scrolling: { mode: "virtual" },
              filterRow: { visible: true },
              sorting: { mode: "multiple" },
              editing: {
                mode: "form",
                allowAdding: userRole === "Manager" || userRole === "Admin",
                allowUpdating: userRole === "Manager" || userRole === "Admin",
                allowDeleting: userRole === "Manager" || userRole === "Admin",
                form: {
                  items: [
                    { dataField: "k01F02", label: { text: "Title" } },
                    {
                      dataField: "k01F03",
                      label: { text: "Description" },
                      editorType: "dxTextArea",
                      editorOptions: { height: 100 },
                    },
                    {
                      dataField: "k01F05",
                      label: "Department",
                      editorType: "dxSelectBox",
                      editorOptions: {
                        dataSource: departmentStore,
                        valueExpr: "t01F01",
                        displayExpr: "t01F02",
                        searchEnabled: true,
                        disabled: userRole === "Manager",
                      },
                    },
                    {
                      dataField: "k01F04",
                      label: { text: "Assigned To" },
                      editorType: "dxSelectBox",
                      editorOptions: {
                        dataSource: employeeStore,
                        valueExpr: "r01F01",
                        displayExpr: (employee) =>
                          employee
                            ? `${employee.r01F05} ${
                                employee.r01F06 || ""
                              }`.trim()
                            : "",
                        searchEnabled: true,
                      },
                    },
                    {
                      dataField: "k01F06",
                      label: { text: "Status" },
                      editorType: "dxSelectBox",
                      editorOptions: {
                        dataSource: statusOptions,
                        valueExpr: "value",
                        displayExpr: "text",
                      },
                    },
                    {
                      dataField: "k01F07",
                      label: { text: "Priority" },
                      editorType: "dxSelectBox",
                      editorOptions: {
                        dataSource: priorityOptions,
                        valueExpr: "value",
                        displayExpr: "text",
                      },
                    },
                    {
                      dataField: "k01F08",
                      label: { text: "Due Date" },
                      editorType: "dxDateBox",
                      editorOptions: { type: "date" },
                    },
                  ],
                },
              },
              columns: [
                {
                  dataField: "k01F01",
                  caption: "Task ID",
                  width: 100,
                  allowEditing: false,
                },
                { dataField: "k01F02", caption: "Title" },
                { dataField: "k01F03", caption: "Description" },
                {
                  dataField: "k01F04",
                  caption: "Assigned To",
                  lookup: {
                    dataSource: employeeStore,
                    valueExpr: "r01F01",
                    displayExpr: (employee) =>
                      employee
                        ? `${employee.r01F05} ${employee.r01F06 || ""}`.trim()
                        : "",
                  },
                },
                {
                  dataField: "k01F05",
                  caption: "Department",
                  lookup: {
                    dataSource: departmentStore,
                    valueExpr: "t01F01",
                    displayExpr: "t01F02",
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
                  caption: "Due Date",
                  dataType: "date",
                  format: "dd-MM-yyyy",
                },
              ],
            });
          },
        },
        {
          title: "Users",
          visible: userRole === "Admin",
          template: () => {
            return $("<div>").dxDataGrid({
              dataSource: userStore,
              showBorders: true,
              columnAutoWidth: true,
              paging: { pageSize: 10 },
              pager: {
                visible: true,
                showPageSizeSelector: true,
                allowedPageSizes: [5, 10, 20],
                showInfo: true,
              },
              scrolling: { mode: "virtual" },
              filterRow: { visible: true },
              sorting: { mode: "multiple" },
              editing: {
                mode: "form",
                allowAdding: true,
                allowUpdating: true,
                allowDeleting: true,
                form: {
                  items: [
                    { dataField: "r01F02", label: { text: "Username" } },
                    { dataField: "r01F03", label: { text: "Password Hash" } },
                    {
                      dataField: "r01F04",
                      label: { text: "Role" },
                      editorType: "dxSelectBox",
                      editorOptions: {
                        dataSource: roleOptions,
                        valueExpr: "value",
                        displayExpr: "text",
                      },
                    },
                    { dataField: "r01F05", label: { text: "First Name" } },
                    { dataField: "r01F06", label: { text: "Last Name" } },
                    { dataField: "r01F07", label: { text: "Email" } },
                    {
                      dataField: "r01F08",
                      label: { text: "Department" },
                      editorType: "dxSelectBox",
                      editorOptions: {
                        dataSource: departmentStore,
                        valueExpr: "t01F01",
                        displayExpr: "t01F02",
                        searchEnabled: true,
                      },
                    },
                    {
                      dataField: "r01F09",
                      label: { text: "Hire Date" },
                      editorType: "dxDateBox",
                      editorOptions: { type: "date" },
                    },
                  ],
                },
              },
              columns: [
                {
                  dataField: "r01F01",
                  caption: "User ID",
                  width: 100,
                  allowEditing: false,
                },
                { dataField: "r01F02", caption: "Username" },
                { dataField: "r01F03", caption: "Password Hash" },
                {
                  dataField: "r01F04",
                  caption: "Role",
                  lookup: {
                    dataSource: roleOptions,
                    valueExpr: "value",
                    displayExpr: "text",
                  },
                },
                { dataField: "r01F05", caption: "First Name" },
                { dataField: "r01F06", caption: "Last Name" },
                { dataField: "r01F07", caption: "Email" },
                {
                  dataField: "r01F08",
                  caption: "Department",
                  lookup: {
                    dataSource: departmentStore,
                    valueExpr: "t01F01",
                    displayExpr: "t01F02",
                  },
                },
                {
                  dataField: "r01F09",
                  caption: "Hire Date",
                  dataType: "date",
                  format: "dd-MM-yyyy",
                },
              ],
            });
          },
        },
        {
          title: "Departments",
          visible: userRole === "Admin",
          template: () => {
            return $("<div>").dxDataGrid({
              dataSource: departmentStore,
              showBorders: true,
              columnAutoWidth: true,
              paging: { pageSize: 10 },
              pager: {
                visible: true,
                showPageSizeSelector: true,
                allowedPageSizes: [5, 10, 20],
                showInfo: true,
              },
              scrolling: { mode: "virtual" },
              filterRow: { visible: true },
              sorting: { mode: "multiple" },
              editing: {
                mode: "form",
                allowAdding: true,
                allowUpdating: true,
                allowDeleting: true,
                form: {
                  items: [
                    { dataField: "t01F02", label: { text: "Name" } },
                    {
                      dataField: "t01F03",
                      label: { text: "Manager" },
                      editorType: "dxSelectBox",
                      editorOptions: {
                        dataSource: employeeStore,
                        valueExpr: "r01F01",
                        displayExpr: (employee) =>
                          employee
                            ? `${employee.r01F05} ${
                                employee.r01F06 || ""
                              }`.trim()
                            : "",
                        searchEnabled: true,
                      },
                    },
                  ],
                },
              },
              columns: [
                {
                  dataField: "t01F01",
                  caption: "Department ID",
                  width: 100,
                  allowEditing: false,
                },
                {
                  dataField: "t01F02",
                  caption: "Name",
                  validationRules: [{ type: "required" }],
                },
                {
                  dataField: "t01F03",
                  caption: "Manager",
                  lookup: {
                    dataSource: employeeStore,
                    valueExpr: "r01F01",
                    displayExpr: (employee) =>
                      employee
                        ? `${employee.r01F05} ${employee.r01F06 || ""}`.trim()
                        : "",
                  },
                },
              ],
            });
          },
        },
      ],
      animationEnabled: true,
      swipeEnabled: true,
      tabWidth: 150,
      height: "100%",
    });
  }

  // Login Form
  $("#loginPanel").dxForm({
    formData: { username: "", password: "" },
    items: [
      {
        dataField: "username",
        label: { text: "Username" },
        editorOptions: { placeholder: "Enter username" },
        validationRules: [
          { type: "required", message: "Username is required" },
        ],
      },
      {
        dataField: "password",
        label: { text: "Password" },
        editorType: "dxTextBox",
        editorOptions: { mode: "password", placeholder: "Enter password" },
        validationRules: [
          { type: "required", message: "Password is required" },
        ],
      },
      {
        itemType: "button",
        buttonOptions: {
          text: "Login",
          type: "success",
          useSubmitBehavior: true,
          onClick: () => {
            const form = $("#loginPanel").dxForm("instance");
            const validationResult = form.validate();
            if (validationResult.isValid) {
              const formData = form.option("formData");
              authStore
                .login(formData.username, formData.password)
                .catch(() => {});
            }
          },
        },
      },
    ],
  });

  // Check for existing token
  token = localStorage.getItem("jwtToken");
  userRole = localStorage.getItem("userRole");
  userId = localStorage.getItem("userId");
  if (token) {
    $("#loginPanel").hide();
    $("#dashboard").show();
    loadDashboard();
  } else {
    $("#loginPanel").show();
    $("#dashboard").hide();
  }
});
