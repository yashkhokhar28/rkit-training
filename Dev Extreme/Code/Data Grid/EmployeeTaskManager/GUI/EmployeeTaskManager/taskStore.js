import { TaskAPIURL } from "./apiConfig.js";
import { getAuthHeader, DisplayMessage } from "./utils.js";

const taskStore = new DevExpress.data.CustomStore({
  key: "k01F01",

  load: (loadOptions) => {
    console.log("Load options:", loadOptions); // Log skip, take, etc.
    let params = {
      skip: loadOptions.skip || 0,
      take: loadOptions.take || 10,
      filter: loadOptions.filter ? JSON.stringify(loadOptions.filter) : null,
      sort: loadOptions.sort ? JSON.stringify(loadOptions.sort) : null,
    };

    console.log("Request params:", params); // Log what’s sent to API

    return $.ajax({
      url: TaskAPIURL,
      method: "GET",
      data: params,
      headers: getAuthHeader(),
    })
      .then((result) => {
        console.log("Raw API response:", result); // Log the full response
        if (result.isError) {
          throw new Error(result.message || "Failed to load tasks");
        }
        DisplayMessage("Tasks loaded successfully", "success", 1000);
        return {
          data: result.data || result.Data || [], // Handle both cases
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

  byKey: (key) => {
    console.log("Fetching task with key:", key);
    return $.ajax({
      url: `${TaskAPIURL}/ID?ID=${key}`,
      method: "GET",
      headers: getAuthHeader(),
    })
      .then((result) => {
        console.log("byKey response:", result);
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

  update: (key, values) => {
    console.log("Updating task with key:", key);
    return taskStore
      .byKey(key)
      .then((existingTask) => {
        console.log("Existing task fetched:", existingTask);
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

        console.log("DTO for update:", dtoTask);
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
