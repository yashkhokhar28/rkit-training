$(document).ready(() => {
  console.log("Document is Ready");

  var customStore = new DevExpress.data.CustomStore({
    key: "id",

    // Load data from API
    load: () => {
      return $.getJSON("https://67a70408510789ef0dfcbb1f.mockapi.io/api/users/")
        .then((data) => {
          console.log("Data Loaded:", data);
          displayData(data);
          return data; // Return data for CustomStore
        })
        .fail((error) => {
          console.error("Error fetching data:", error);
          return []; // Return an empty array to avoid errors
        });
    },

    // Insert a new record
    insert: (values) => {
      return $.ajax({
        url: "https://67a70408510789ef0dfcbb1f.mockapi.io/api/users/",
        method: "POST",
        data: values,
      })
        .then((response) => {
          console.log("Inserted:", response);
          customStore.load();
        })
        .fail((error) => {
          console.error("Error inserting data:", error);
        });
    },

    // Update an existing record
    update: (key, values) => {
      return $.ajax({
        url: `https://67a70408510789ef0dfcbb1f.mockapi.io/api/users/${key}`,
        method: "PUT",
        data: values,
      })
        .then((response) => {
          console.log("Updated:", response);
          customStore.load();
        })
        .fail((error) => {
          console.error("Error updating data:", error);
        });
    },

    // Delete a record
    remove: (key) => {
      return $.ajax({
        url: `https://67a70408510789ef0dfcbb1f.mockapi.io/api/users/${key}`,
        method: "DELETE",
      })
        .then((response) => {
          console.log("Deleted:", response);
          customStore.load();
        })
        .fail((error) => {
          console.error("Error deleting data:", error);
        });
    },

    // Triggered before data is loaded from the store
    onLoading: (loadOptions) => {
      console.log("Data is being loaded", loadOptions);
    },

    // Triggered after data is successfully loaded
    onLoaded: (result) => {
      console.log("Data has been loaded", result);
    },

    // Triggered before a new record is inserted
    onInserting: (values) => {
      console.log("A new record is being inserted", values);
    },

    // Triggered after a new record is successfully inserted
    onInserted: (key, values) => {
      console.log("A new record has been inserted", key, values);
    },

    // Triggered before an existing record is updated
    onUpdating: (key, values) => {
      console.log("A record is being updated", key, values);
    },

    // Triggered after an existing record is successfully updated
    onUpdated: (key, values) => {
      console.log("A record has been updated", key, values);
    },

    // Triggered before a record is removed
    onRemoving: (key) => {
      console.log("A record is being removed", key);
    },

    // Triggered after a record is successfully removed
    onRemoved: (key) => {
      console.log("A record has been removed", key);
    },

    // Triggered before modifications (update, delete, etc.) occur
    onModifying: () => {
      console.log("A modification operation is being performed");
    },

    // Triggered after modifications (update, delete, etc.) occur
    onModified: () => {
      console.log("A modification operation has been completed");
    },

    // Triggered when push changes (real-time updates) are received
    onPush: (changes) => {
      console.log("Real-time data updates received", changes);
    },

    loadMode: "raw", // Fixed property name
    errorHandler: (error) => {
      alert(error.message);
    },
  });

  // Load initial data
  customStore.load().done((d) => {
    displayData(d);
  });

  // Function to display data dynamically in the table
  function displayData(data) {
    let tableBody = $("table tbody");
    tableBody.empty(); // Clear existing rows before adding new ones

    data.forEach((user) => {
      let row = `<tr>
          <th scope="row">${user.id}</th>
          <td>${user.name || "N/A"}</td>
          <td>${user.email || "N/A"}</td>
          <td>
            <button class="btn btn-outline-success edit-btn" data-id="${
              user.id
            }">Edit</button>
            <button class="btn btn-outline-danger delete-btn" data-id="${
              user.id
            }">Delete</button>
          </td>
        </tr>`;
      tableBody.append(row);
    });

    attachEventListeners();
  }

  $("#addUserBtn").on("click", function () {
    let name = prompt("Enter Name:");
    let email = prompt("Enter Email:");

    if (name && email) {
      let newUser = { name: name, email: email };
      customStore.insert(newUser);
    } else {
      alert("Both fields are required.");
    }
  });

  // Attach event listeners after table data is populated
  function attachEventListeners() {
    $(".edit-btn").on("click", function () {
      let id = $(this).data("id");
      let name = prompt("Enter name:");
      let email = prompt("Enter email:");
      if (name && email) {
        customStore.update(id, { name: name, email: email });
      } else {
        alert("Both fields are required.");
      }
    });

    $(".delete-btn").on("click", function () {
      let id = $(this).data("id");
      if (confirm("Are you sure you want to delete this record?")) {
        customStore.remove(id);
      }
    });
  }
});
