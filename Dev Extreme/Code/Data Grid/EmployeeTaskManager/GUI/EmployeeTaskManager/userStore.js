import { AuthAPIURL } from "./apiConfig.js";
import { DisplayMessage, getAuthHeader } from "./utils.js";

const userStore = new DevExpress.data.CustomStore({
  key: "r01F01",

  /**
   * Function: load
   * Description: Fetches all users from the API.
   * Called in: User grid initialization and refresh.
   * Parameters: None.
   * Returns: Promise resolving with array of users or rejecting with error.
   */
  load: () => {
    return $.ajax({
      url: AuthAPIURL,
      method: "GET",
      headers: getAuthHeader(),
    })
      .then((result) => {
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

  /**
   * Function: byKey
   * Description: Fetches a single user by its ID from the API.
   * Called in: User selection or update operations.
   * Parameters:
   *   - {key: string}: The ID of the user to fetch.
   * Returns: Promise resolving with user object or rejecting with error.
   */
  byKey: (key) => {
    return $.ajax({
      url: `${AuthAPIURL}/ID?ID=${key}`,
      method: "GET",
      headers: getAuthHeader(),
    })
      .then((result) => {
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

  /**
   * Function: insert
   * Description: Creates a new user by sending data to the API.
   * Called in: User creation form submission.
   * Parameters:
   *   - {values: object}: Object containing user data.
   * Returns: Promise resolving with created user data or rejecting with error.
   */
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

  /**
   * Function: remove
   * Description: Deletes a user by its ID.
   * Called in: User deletion action.
   * Parameters:
   *   - {key: string}: The ID of the user to delete.
   * Returns: Promise resolving on success or rejecting with error.
   */
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

  /**
   * Function: update
   * Description: Updates an existing user with new values.
   * Called in: User edit form submission.
   * Parameters:
   *   - {key: string}: The ID of the user to update.
   *   - {values: object}: Object containing updated user data.
   * Returns: Promise resolving on success or rejecting with error.
   */
  update: (key, values) => {
    return userStore
      .byKey(key)
      .then((existingUser) => {
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
