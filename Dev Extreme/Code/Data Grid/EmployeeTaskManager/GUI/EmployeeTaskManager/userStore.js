import { AuthAPIURL } from "./apiConfig.js";
import { DisplayMessage, getAuthHeader } from "./utils.js";

const userStore = new DevExpress.data.CustomStore({
  key: "r01F01",

  // ✅ Improved error handling in load()
  load: () => {
    return $.ajax({
      url: AuthAPIURL,
      method: "GET",
      headers: getAuthHeader(),
    })
      .then((result) => {
        console.log("User load result:", result);
        if (result.isError) {
          throw new Error(result.message || "Failed to load users");
        }
        DisplayMessage("Users loaded successfully", "success", 1000);
        return result.data || [];
      })
      .catch((xhr) => {
        console.error("Error loading users:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Unknown error";
        DisplayMessage(`Failed to load users: ${errorMessage}`, "error", 2000);
        throw new Error(errorMessage);
      });
  },

  // ✅ Improved byKey error handling
  byKey: (key) => {
    console.log("Fetching user with key:", key);
    return $.ajax({
      url: `${AuthAPIURL}/ID?ID=${key}`,
      method: "GET",
      headers: getAuthHeader(),
    })
      .then((result) => {
        console.log("byKey response:", result);
        if (result.isError) {
          throw new Error(result.message || "Unknown error fetching user");
        }
        if (
          !result.data ||
          !Array.isArray(result.data) ||
          result.data.length === 0
        ) {
          throw new Error("User not found or invalid response format");
        }
        return result.data[0];
      })
      .catch((xhr) => {
        console.error("AJAX Error in byKey:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Network error";
        DisplayMessage(`Failed to fetch user: ${errorMessage}`, "error", 2000);
        throw new Error(errorMessage);
      });
  },

  // ✅ Improved insert() method
  insert: (values) => {
    const dtoUser = {
      R01102: values.r01F02 || "",
      R01103: values.r01F03 || "",
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
    })
      .then((result) => {
        if (result.isError) {
          throw new Error(result.message || "Failed to add user");
        }
        DisplayMessage("User added successfully", "success", 2000);
        return result.data;
      })
      .catch((xhr) => {
        console.error("Error adding user:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Unknown error";
        DisplayMessage(`Failed to add user: ${errorMessage}`, "error", 3000);
        throw new Error(errorMessage);
      });
  },

  // ✅ Improved remove() method
  remove: (key) => {
    return $.ajax({
      url: `${AuthAPIURL}/ID?ID=${key}`,
      method: "DELETE",
      headers: getAuthHeader(),
    })
      .then((result) => {
        if (result.isError) {
          throw new Error(result.message || "Failed to delete user");
        }
        DisplayMessage("User deleted successfully", "success", 2000);
      })
      .catch((xhr) => {
        console.error("Error deleting user:", xhr);
        const errorMessage =
          xhr.responseJSON?.message || xhr.statusText || "Unknown error";
        DisplayMessage(`Failed to delete user: ${errorMessage}`, "error", 3000);
        throw new Error(errorMessage);
      });
  },

  // ✅ Improved update() method
  update: (key, values) => {
    console.log("Updating user with key:", key);
    return userStore
      .byKey(key)
      .then((existingUser) => {
        console.log("Existing user fetched:", existingUser);
        if (!key) {
          throw new Error("Key is null or undefined!");
        }
        if (!existingUser.r01F01) {
          throw new Error("Existing user does not have a valid key!");
        }

        const dtoUser = {
          R01101: key,
          R01102: values.r01F02 ?? existingUser.r01F02,
          R01103: values.r01F03 ?? existingUser.r01F03,
          R01104: values.r01F04 ?? existingUser.r01F04,
          R01105: values.r01F05 ?? existingUser.r01F05,
          R01106: values.r01F06 ?? existingUser.r01F06,
          R01107: values.r01F07 ?? existingUser.r01F07,
          R01108: values.r01F08 ?? existingUser.r01F08,
          R01109: values.r01F09 ?? existingUser.r01F09,
        };

        console.log("DTO for update:", dtoUser);
        return $.ajax({
          url: AuthAPIURL,
          method: "PUT",
          contentType: "application/json",
          data: JSON.stringify(dtoUser),
          headers: getAuthHeader(),
        });
      })
      .then((result) => {
        if (result.isError) {
          throw new Error(result.message || "Failed to update user");
        }
        DisplayMessage("User updated successfully", "success", 2000);
      })
      .catch((error) => {
        console.error("Error updating user:", error);
        DisplayMessage(
          `Update failed: ${error.message || error}`,
          "error",
          3000
        );
        throw error;
      });
  },
});

export { userStore };
