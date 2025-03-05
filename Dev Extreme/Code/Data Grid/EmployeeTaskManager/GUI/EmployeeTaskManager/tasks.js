import { taskStore } from "./taskStore.js";
import { authStore } from "./authStore.js";
import { departmentStore } from "./departmentStore.js";
import { userStore } from "./userStore.js";
import { statusOptions, priorityOptions, roleOptions } from "./enums.js";
import { getAuthState, toggleTheme } from "./utils.js";

let employeeStore;

export function loadDashboard() {
  const { userRole } = getAuthState();
  const dashboardElement = $("#dashboard");
  if (dashboardElement.children().length > 0) {
    return;
  }

  const dashboardContainer = $("<div>").appendTo("#dashboard");
  const toolbar = $("<div>").appendTo(dashboardContainer);
  const isDark = sessionStorage.getItem("theme") === "dark";
  const toolbarInstance = toolbar
    .dxToolbar({
      items: [
        {
          widget: "dxButton",
          location: "after",
          options: {
            text: isDark ? "☀️" : "🌙",
            onClick: () => {
              toggleTheme();
              const newIsDark = sessionStorage.getItem("theme") === "dark";
              toolbarInstance.option(
                "items[0].options.text",
                newIsDark ? "☀️" : "🌙"
              );
            },
          },
        },
        {
          widget: "dxButton",
          location: "after",
          options: {
            text: "🚪 Logout",
            onClick: () => {
              sessionStorage.clear();
              window.location.reload();
            },
          },
        },
      ],
    })
    .dxToolbar("instance");

  const tabPanelContainer = $("<div>").appendTo(dashboardContainer);
  tabPanelContainer.dxTabPanel({
    items: [
      {
        title: "Tasks",
        template: () => {
          return $("<div>").dxDataGrid({
            dataSource: taskStore,
            showBorders: true,
            showColumnLines: true,
            showRowLines: true,
            rowAlternationEnabled: true,
            columnAutoWidth: true,
            columnHidingEnabled: true,
            filterRow: { visible: true },
            headerFilter: { visible: true },
            sorting: { mode: "multiple" },
            paging: {
              enabled: true,
              pageSize: 10,
              pageIndex: 0,
            },
            pager: {
              displayMode: "adaptive",
              visible: true,
              showPageSizeSelector: true,
              allowedPageSizes: [10, 20, "all"],
              showInfo: true,
              label: "Navigation",
              infoText: "Page {0} of {1} ({2} items)",
            },
            grouping: {
              autoExpandAll: false,
              allowCollapsing: true,
            },
            groupPanel: {
              visible: true,
              allowColumnDragging: true,
            },
            summary: {
              groupItems: [
                {
                  column: "k01F01",
                  summaryType: "count",
                  displayFormat: "{0} tasks",
                },
                {
                  column: "k01F08",
                  summaryType: "min",
                  displayFormat: "Earliest Due: {0}",
                },
              ],
              totalItems: [
                {
                  column: "k01F01",
                  summaryType: "count",
                  alignment: "center",
                  displayFormat: "Total Tasks: {0}",
                },
              ],
            },
            export: {
              enabled: true,
              fileName: "TasksExport",
              allowExportSelectedData: true,
            },
            onToolbarPreparing: function (e) {
              const toolbarItems = e.toolbarOptions.items;
              toolbarItems.push({
                location: "after",
                widget: "dxButton",
                options: {
                  text: "Clear Filters",
                  onClick: function () {
                    e.component.clearFilter();
                  },
                },
              });
            },
            editing: {
              mode: "form",
              allowAdding: userRole === "Manager",
              allowUpdating: userRole === "Manager",
              allowDeleting: userRole === "Manager",
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
                    dataField: "k01F04",
                    label: { text: "Assigned To" },
                    editorType: "dxSelectBox",
                    editorOptions: {
                      dataSource: employeeStore,
                      valueExpr: "r01F01",
                      displayExpr: (employee) =>
                        employee
                          ? `${employee.r01F05} ${employee.r01F06 || ""}`.trim()
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
            onRowPrepared: function (e) {
              if (e.rowType === "data") {
                if (e.data.k01F07 === "High") {
                  e.rowElement.css("background-color", "#ffe6e6");
                } else if (e.data.k01F07 === "Medium") {
                  e.rowElement.css("background-color", "#fff5e6");
                }
                if (e.data.k01F08) {
                  const dueDate = new Date(e.data.k01F08);
                  const today = new Date();
                  const diffDays = Math.ceil(
                    (dueDate - today) / (1000 * 60 * 60 * 24)
                  );
                  if (diffDays < 0) {
                    e.rowElement.css("color", "red");
                  } else if (diffDays <= 3) {
                    e.rowElement.css("color", "orange");
                  }
                }
              }
            },
            columns: [
              {
                dataField: "k01F01",
                caption: "Task ID",
                // width: 100,
                allowEditing: false,
                hidingPriority: 7,
              },
              { dataField: "k01F02", caption: "Title", hidingPriority: 6 },
              {
                dataField: "k01F03",
                caption: "Description",
                hidingPriority: 5,
              },
              {
                dataField: "k01F04",
                caption: "Assigned To",
                hidingPriority: 4,
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
                hidingPriority: 3,
                lookup: {
                  dataSource: departmentStore,
                  valueExpr: "t01F01",
                  displayExpr: "t01F02",
                },
              },
              {
                dataField: "k01F06",
                caption: "Status",
                hidingPriority: 2,
                lookup: {
                  dataSource: statusOptions,
                  valueExpr: "value",
                  displayExpr: "text",
                },
              },
              {
                dataField: "k01F07",
                caption: "Priority",
                hidingPriority: 1,
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
                hidingPriority: 0,
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
            showColumnLines: true,
            showRowLines: true,
            rowAlternationEnabled: true,
            columnAutoWidth: true,
            columnHidingEnabled: true,
            paging: {
              enabled: true,
              pageSize: 10,
              pageIndex: 1,
            },
            pager: {
              displayMode: "adaptive",
              visible: true,
              showPageSizeSelector: true,
              allowedPageSizes: [10, 20, "all"],
              showInfo: true,
              label: "Navigation",
              infoText: "Page {0} of {1} ({2} items)",
            },
            filterRow: { visible: true },
            headerFilter: { visible: true },
            sorting: { mode: "multiple" },
            grouping: {
              autoExpandAll: false,
              allowCollapsing: true,
            },
            groupPanel: {
              visible: true,
              allowColumnDragging: true,
            },
            summary: {
              groupItems: [
                {
                  column: "r01F01",
                  summaryType: "count",
                  displayFormat: "{0} users",
                },
              ],
              totalItems: [
                {
                  column: "r01F01",
                  summaryType: "count",
                  displayFormat: "Total Users: {0}",
                },
              ],
            },
            export: {
              enabled: true,
              fileName: "UsersExport",
              allowExportSelectedData: true,
            },
            onToolbarPreparing: function (e) {
              const toolbarItems = e.toolbarOptions.items;
              toolbarItems.push({
                location: "after",
                widget: "dxButton",
                options: {
                  text: "Clear Filters",
                  onClick: function () {
                    e.component.clearFilter();
                  },
                },
              });
            },
            editing: {
              mode: "form",
              allowAdding: true,
              allowDeleting: true,
              allowUpdating: true,
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
                      searchEnabled: true,
                    },
                    validationRules: [
                      { type: "required", message: "Role is required" },
                    ],
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
                    validationRules: [
                      { type: "required", message: "Department is required" },
                    ],
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
            onRowPrepared: function (e) {
              if (e.rowType === "data") {
                if (e.data.r01F04 === "Admin") {
                  e.rowElement.css("background-color", "#e6f3ff");
                }
              }
            },
            columns: [
              {
                dataField: "r01F01",
                caption: "User ID",
                width: 100,
                allowEditing: false,
                hidingPriority: 8,
              },
              { dataField: "r01F02", caption: "Username", hidingPriority: 7 },
              {
                dataField: "r01F03",
                caption: "Password Hash",
                hidingPriority: 6,
              },
              {
                dataField: "r01F04",
                caption: "Role",
                hidingPriority: 5,
                lookup: {
                  dataSource: roleOptions,
                  valueExpr: "value",
                  displayExpr: "text",
                },
              },
              { dataField: "r01F05", caption: "First Name", hidingPriority: 4 },
              { dataField: "r01F06", caption: "Last Name", hidingPriority: 3 },
              { dataField: "r01F07", caption: "Email", hidingPriority: 2 },
              {
                dataField: "r01F08",
                caption: "Department",
                hidingPriority: 1,
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
                hidingPriority: 0,
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
            showColumnLines: true,
            showRowLines: true,
            rowAlternationEnabled: true,
            columnAutoWidth: true,
            columnHidingEnabled: true,
            paging: {
              enabled: true,
              pageSize: 10,
              pageIndex: 1,
            },
            pager: {
              displayMode: "adaptive",
              visible: true,
              showPageSizeSelector: true,
              allowedPageSizes: [10, 20, "all"],
              showInfo: true,
              label: "Navigation",
              infoText: "Page {0} of {1} ({2} items)",
            },
            filterRow: { visible: true },
            headerFilter: { visible: true },
            sorting: { mode: "multiple" },
            grouping: {
              autoExpandAll: false,
              allowCollapsing: true,
            },
            groupPanel: {
              visible: true,
              allowColumnDragging: true,
            },
            summary: {
              groupItems: [
                {
                  column: "t01F01",
                  summaryType: "count",
                  displayFormat: "{0} depts",
                },
              ],
              totalItems: [
                {
                  column: "t01F01",
                  summaryType: "count",
                  displayFormat: "Total Depts: {0}",
                },
              ],
            },
            export: {
              enabled: true,
              fileName: "DepartmentsExport",
              allowExportSelectedData: true,
            },
            onToolbarPreparing: function (e) {
              const toolbarItems = e.toolbarOptions.items;
              toolbarItems.push({
                location: "after",
                widget: "dxButton",
                options: {
                  text: "Clear Filters",
                  onClick: function () {
                    e.component.clearFilter();
                  },
                },
              });
            },
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
                          ? `${employee.r01F05} ${employee.r01F06 || ""}`.trim()
                          : "",
                      searchEnabled: true,
                    },
                  },
                ],
              },
            },
            onRowPrepared: function (e) {
              if (e.rowType === "data") {
                if (!e.data.t01F03) {
                  e.rowElement.css("background-color", "#fff0f0");
                }
              }
            },
            columns: [
              {
                dataField: "t01F01",
                caption: "Department ID",
                width: 100,
                allowEditing: false,
                hidingPriority: 2,
              },
              {
                dataField: "t01F02",
                caption: "Name",
                validationRules: [{ type: "required" }],
                hidingPriority: 1,
              },
              {
                dataField: "t01F03",
                caption: "Manager",
                hidingPriority: 0,
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
    loop: true,
    showNavButtons: true,
    height: "100%",
  });
}

$(() => {
  console.log("Document is Ready!!");

  employeeStore = new DevExpress.data.CustomStore({
    key: "p01F01",
    load: () => {
      return userStore.load();
    },
    byKey: (key) => {
      return userStore.byKey(key);
    },
  });

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
});
