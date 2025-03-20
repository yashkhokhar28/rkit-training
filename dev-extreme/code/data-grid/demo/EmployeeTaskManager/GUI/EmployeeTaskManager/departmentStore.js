import { DepartmentAPIURL } from "./apiConfig.js";
import { DisplayMessage, getAuthHeader } from "./utils.js";

const departmentStore = new DevExpress.data.CustomStore({
  key: "t01F01",

  /**
   * Function: load
   * Description: Fetches all departments from the API.
   * Called in: Department grid initialization and refresh.
   * Parameters: None.
   * Returns: Promise resolving with array of departments or rejecting with error.
   */
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

  /**
   * Function: byKey
   * Description: Fetches a single department by its ID from the API.
   * Called in: Department selection or update operations.
   * Parameters:
   *   - {key: string}: The ID of the department to fetch.
   * Returns: Promise resolving with department object or rejecting with error.
   */
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

  /**
   * Function: insert
   * Description: Creates a new department by sending data to the API.
   * Called in: Department creation form submission.
   * Parameters:
   *   - {values: object}: Object containing department data (t01F02: name, t01F03: numeric value).
   * Returns: Promise resolving with created department data or rejecting with error.
   */
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

  /**
   * Function: update
   * Description: Updates an existing department with new values.
   * Called in: Department edit form submission.
   * Parameters:
   *   - {key: string}: The ID of the department to update.
   *   - {values: object}: Object containing updated department data.
   * Returns: Promise resolving on success or rejecting with error.
   */
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

  /**
   * Function: remove
   * Description: Deletes a department by its ID.
   * Called in: Department deletion action.
   * Parameters:
   *   - {key: string}: The ID of the department to delete.
   * Returns: Promise resolving on success or rejecting with error.
   */
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
