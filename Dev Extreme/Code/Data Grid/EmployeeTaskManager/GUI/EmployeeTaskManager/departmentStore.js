import { DepartmentAPIURL } from "./apiConfig.js";
import { DisplayMessage } from "./utils.js";
import { getAuthHeader } from "./utils.js";

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
      url: `${DepartmentAPIURL}/ID?ID=${key}`,
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
      url: `${DepartmentAPIURL}/ID?ID=${key}`,
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

export { departmentStore };
