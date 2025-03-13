import { TaskAPIURL } from "./apiConfig.js";
import { getAuthHeader, DisplayMessage } from "./utils.js";

const taskStore = new DevExpress.data.CustomStore({
  key: "k01F01",

  /**
   * Function: load
   * Description: Fetches tasks from the API with support for pagination, filtering, and sorting.
   * Called in: Task grid initialization and refresh.
   * Parameters:
   *   - {loadOptions: object}: Options for skip, take, filter, and sort.
   * Returns: Promise resolving with data and total count or rejecting with error.
   */
  load: (loadOptions) => {
    let params = {
      skip: loadOptions.skip || 0,
      take: loadOptions.take || 100,
      filter: loadOptions.filter ? JSON.stringify(loadOptions.filter) : null,
      sort: loadOptions.sort ? JSON.stringify(loadOptions.sort) : null,
    };
    return $.ajax({
      url: TaskAPIURL,
      method: "GET",
      data: params,
      headers: getAuthHeader(),
    })
      .then((result) => {
        if (result.isError) {
          throw new Error(result.message || "Failed to load tasks");
        }
        DisplayMessage("Tasks loaded successfully", "success", 1000);
        return {
          data: result.data || result.Data || [],
          totalCount: result.totalCount || result.TotalCount || 0,
        };
      })
      .catch((xhr) => {
        console.error("Error loading tasks:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Unknown error";
        DisplayMessage(`Failed to load tasks: ${errorMessage}`, "error", 2000);
        throw new Error(errorMessage);
      });
  },

  /**
   * Function: byKey
   * Description: Fetches a single task by its ID from the API.
   * Called in: Task selection or update operations.
   * Parameters:
   *   - {key: string}: The ID of the task to fetch.
   * Returns: Promise resolving with task object or rejecting with error.
   */
  byKey: (key) => {
    return $.ajax({
      url: `${TaskAPIURL}/ID?ID=${key}`,
      method: "GET",
      headers: getAuthHeader(),
    })
      .then((result) => {
        if (result.isError) {
          throw new Error(result.message || "Unknown error fetching task");
        }
        if (
          !result.data ||
          !Array.isArray(result.data) ||
          result.data.length === 0
        ) {
          throw new Error("Task not found or invalid response format");
        }
        return result.data[0];
      })
      .catch((xhr) => {
        console.error("AJAX Error in byKey:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Network error";
        DisplayMessage(`Failed to fetch task: ${errorMessage}`, "error", 2000);
        throw new Error(errorMessage);
      });
  },

  /**
   * Function: insert
   * Description: Creates a new task by sending data to the API.
   * Called in: Task creation form submission.
   * Parameters:
   *   - {values: object}: Object containing task data.
   * Returns: Promise resolving with created task data or rejecting with error.
   */
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
    })
      .then((result) => {
        if (result.isError) {
          throw new Error(result.message || "Failed to add task");
        }
        DisplayMessage("Task added successfully", "success", 2000);
        return result.data;
      })
      .catch((xhr) => {
        console.error("Error adding task:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Unknown error";
        DisplayMessage(`Failed to add task: ${errorMessage}`, "error", 3000);
        throw new Error(errorMessage);
      });
  },

  /**
   * Function: update
   * Description: Updates an existing task with new values.
   * Called in: Task edit form submission.
   * Parameters:
   *   - {key: string}: The ID of the task to update.
   *   - {values: object}: Object containing updated task data.
   * Returns: Promise resolving on success or rejecting with error.
   */
  update: (key, values) => {
    return taskStore
      .byKey(key)
      .then((existingTask) => {
        const dtoTask = {
          K01101: key,
          K01102: values.k01F02 ?? existingTask.k01F02,
          K01103: values.k01F03 ?? existingTask.k01F03,
          K01104: values.k01F04 ?? existingTask.k01F04,
          K01105: values.k01F05 ?? existingTask.k01F05,
          K01106: values.k01F06 ?? existingTask.k01F06,
          K01107: values.k01F07 ?? existingTask.k01F07,
          K01108: values.k01F08 ?? existingTask.k01F08,
        };
        return $.ajax({
          url: `${TaskAPIURL}`,
          method: "PUT",
          contentType: "application/json",
          data: JSON.stringify(dtoTask),
          headers: getAuthHeader(),
        });
      })
      .then((result) => {
        if (result.isError) {
          throw new Error(result.message || "Failed to update task");
        }
        DisplayMessage("Task updated successfully", "success", 2000);
      })
      .catch((error) => {
        console.error("Error in update method:", error);
        DisplayMessage(
          `Update failed: ${error.message || error}`,
          "error",
          3000
        );
        throw error;
      });
  },

  /**
   * Function: remove
   * Description: Deletes a task by its ID.
   * Called in: Task deletion action.
   * Parameters:
   *   - {key: string}: The ID of the task to delete.
   * Returns: Promise resolving on success or rejecting with error.
   */
  remove: (key) => {
    return $.ajax({
      url: `${TaskAPIURL}/${key}`,
      method: "DELETE",
      headers: getAuthHeader(),
    })
      .then((result) => {
        if (result.isError) {
          throw new Error(result.message || "Failed to delete task");
        }
        DisplayMessage("Task deleted successfully", "success", 2000);
      })
      .catch((xhr) => {
        console.error("Error deleting task:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Unknown error";
        DisplayMessage(`Failed to delete task: ${errorMessage}`, "error", 3000);
        throw new Error(errorMessage);
      });
  },
});

export { taskStore };
