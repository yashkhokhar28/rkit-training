import { DepartmentAPIURL } from "./apiConfig.js";
import { DisplayMessage, getAuthHeader } from "./utils.js";

const departmentStore = new DevExpress.data.CustomStore({
  key: "t01F01",

  load: () => {
    return $.ajax({
      url: DepartmentAPIURL,
      method: "GET",
      headers: getAuthHeader(),
    })
      .then((result) => {
        if (result.isError) {
          DisplayMessage(result.message, "error", 2000);
          throw new Error(result.message || "Failed to load departments");
        }

        return result.data || [];
      })
      .catch((xhr) => {
        console.error("Load error:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Unknown error";
        DisplayMessage(errorMessage, "error", 2000);
        throw new Error(errorMessage);
      });
  },

  byKey: (key) => {
    return $.ajax({
      url: `${DepartmentAPIURL}/ID?ID=${key}`,
      method: "GET",
      headers: getAuthHeader(),
    })
      .then((result) => {
        if (result.isError) {
          DisplayMessage(result.message, "error", 2000);
          throw new Error(result.message || "Failed to fetch department");
        }

        return result.data?.[0] || null;
      })
      .catch((xhr) => {
        console.error("byKey error:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Unknown error";
        DisplayMessage(errorMessage, "error", 2000);
        throw new Error(errorMessage);
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
    })
      .then((result) => {
        if (result.isError) {
          DisplayMessage(result.message, "error", 3000);
          throw new Error(result.message || "Failed to add department");
        }

        DisplayMessage(
          result.message || "Department added successfully",
          "success",
          2000
        );
        return result.data;
      })
      .catch((xhr) => {
        console.error("Insert error:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Unknown error";
        DisplayMessage(errorMessage, "error", 3000);
        throw new Error(errorMessage);
      });
  },

  update: (key, values) => {
    return departmentStore
      .byKey(key)
      .then((existingDepartment) => {
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
        });
      })
      .then((result) => {
        if (result.isError) {
          DisplayMessage(result.message, "error", 3000);
          throw new Error(result.message || "Failed to update department");
        }

        DisplayMessage(
          result.message || "Department updated successfully",
          "success",
          2000
        );
      })
      .catch((xhr) => {
        console.error("Update error:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Unknown error";
        DisplayMessage(errorMessage, "error", 3000);
        throw new Error(errorMessage);
      });
  },

  remove: (key) => {
    return $.ajax({
      url: `${DepartmentAPIURL}/ID?ID=${key}`,
      method: "DELETE",
      headers: getAuthHeader(),
    })
      .then((result) => {
        if (result.isError) {
          DisplayMessage(result.message, "error", 3000);
          throw new Error(result.message || "Failed to delete department");
        }

        DisplayMessage(
          result.message || "Department deleted successfully",
          "success",
          2000
        );
      })
      .catch((xhr) => {
        console.error("Remove error:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Unknown error";
        DisplayMessage(errorMessage, "error", 3000);
        throw new Error(errorMessage);
      });
  },
});

export { departmentStore };
