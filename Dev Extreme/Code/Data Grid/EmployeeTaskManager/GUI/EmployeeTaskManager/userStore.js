import { AuthAPIURL } from "./apiConfig.js";
import { DisplayMessage, getAuthHeader } from "./utils.js";

var userStore = new DevExpress.data.CustomStore({
  key: "r01F01",
  load: () => {
    return $.ajax({
      url: AuthAPIURL,
      method: "GET",
      headers: getAuthHeader(),
    }).then((result) => {
      if (result.IsError) {
        DisplayMessage(result.message, "error", 2000);
        throw result.message;
      }
      return result.data;
    });
  },
  byKey: (key) => {
    return $.ajax({
      url: `${AuthAPIURL}/ID?ID=${key}`,
      method: "GET",
      headers: getAuthHeader(),
    }).then((result) => {
      if (result.IsError) {
        DisplayMessage(result.message, "error", 2000);
        throw result.message;
      }
      return result.data;
    });
  },
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
    }).then((result) => {
      if (result.IsError) {
        DisplayMessage(result.message, "error", 3000);
        throw result.message;
      }
      DisplayMessage("User added successfully", "success", 2000);
      return result.data;
    });
  },
  remove: (key) => {
    return $.ajax({
      url: `${AuthAPIURL}/ID?ID=${key}`,
      method: "DELETE",
      headers: getAuthHeader(),
    }).then((result) => {
      if (result.IsError) {
        DisplayMessage(result.message, "error", 3000);
        throw result.message;
      }
      DisplayMessage("User deleted successfully", "success", 2000);
    });
  },
});

export { userStore };
