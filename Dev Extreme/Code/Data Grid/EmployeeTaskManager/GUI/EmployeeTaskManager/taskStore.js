import { TaskAPIURL } from "./apiConfig.js";
import { getAuthHeader, DisplayMessage } from "./utils.js";

const taskStore = new DevExpress.data.CustomStore({
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
          `Failed to load tasks: ${
            xhr.responseJSON?.Message || xhr.statusText
          }`,
          "error",
          2000
        );
        throw `Network error: ${xhr.statusText}`;
      }
    );
  },

  byKey: (key) => {
    console.log("Fetching task with key:", key);
    console.log("Request URL:", `${TaskAPIURL}/ID?ID=${key}`);
    console.log("Auth Headers:", getAuthHeader());
    return $.ajax({
      url: `${TaskAPIURL}/ID?ID=${key}`,
      method: "GET",
      headers: getAuthHeader(),
    })
      .then((result) => {
        console.log("byKey response:", result);
        if (result.IsError) {
          DisplayMessage(result.Message || "Unknown error", "error", 2000);
          throw new Error(result.Message || "Unknown error");
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
        console.error("AJAX Error in byKey:", {
          status: xhr.status,
          statusText: xhr.statusText,
          response: xhr.responseJSON || xhr.responseText,
          url: xhr.responseURL || `${TaskAPIURL}/ID?ID=${key}`,
        });
        const errorMessage =
          xhr.responseJSON?.Message || xhr.statusText || "Network error";
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
    }).then((result) => {
      if (result.IsError) {
        DisplayMessage(result.Message, "error", 3000);
        throw result.Message;
      }
      DisplayMessage("Task added successfully", "success", 2000);
      return result.data; // Fixed to match expected `data` casing
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
        console.log("DTO for update:", dtoTask);
        return $.ajax({
          url: `${TaskAPIURL}`,
          method: "PUT",
          contentType: "application/json",
          data: JSON.stringify(dtoTask),
          headers: getAuthHeader(),
        }).then((result) => {
          if (result.IsError) {
            // Fixed casing to match API
            DisplayMessage(result.Message, "error", 3000);
            throw result.Message;
          }
          DisplayMessage("Task updated successfully", "success", 2000);
        });
      })
      .catch((error) => {
        console.error("Error in update method:", error);
        DisplayMessage(
          `Failed to fetch task for update: ${error.message || error}`,
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
    }).then((result) => {
      if (result.IsError) {
        DisplayMessage(result.Message, "error", 3000);
        throw result.Message;
      }
      DisplayMessage("Task deleted successfully", "success", 2000);
    });
  },
});

export { taskStore };
