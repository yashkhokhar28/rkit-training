$(document).ready(async () => {
  console.log("Document is Ready!!");

  // Fetch and store data in localStorage
  const fetchData = async () => {
    try {
      let data = await $.getJSON(
        "https://67a70408510789ef0dfcbb1f.mockapi.io/api/users/"
      );
      return data;
    } catch (error) {
      console.error("Can't fetch data:", error);
    }
  };

  // Ensure data exists before initializing LocalStore
  let users = JSON.parse(localStorage.getItem("Users"));
  if (!users || users.length === 0) {
    users = await fetchData();
  }

  // Initialize LocalStore with loaded data
  var localStore = new DevExpress.data.LocalStore({
    // The dataset that will be managed by localStore
    name: "Users",

    // Unique key property to identify records
    key: "id",

    data: users,
    immediate: true,

    // flushInterval: 3000,

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

    // Error handling mechanism
    // errorHandler: (error) => {
    //   console.log("Error:", error);
    // },
  });

  console.log("------ Testing byKey Method ------");

  // Fetch a record by key (valid key)
  localStore
    .byKey("5")
    .done((dataItem) => {
      console.log("Record found:", dataItem);
    })
    .fail((error) => {
      console.log("Error fetching record:", error);
    });

  console.log("------ Testing Insert Method ------");

  // Insert a new record (use lowercase `id`)
  localStore
    .insert({ id: "100", name: "New Name", email: "new@gmail.com" })
    .done((dataObj, key) => {
      console.log("Inserted record:", dataObj, "with key:", key);
    })
    .fail((error) => {
      console.log("Error inserting record:", error);
    });

  console.log("------ Testing Key Methods ------");

  // Get the key property of the store
  var keyProps = localStore.key();
  console.log("Key Property:", keyProps);

  console.log("------ Testing Load & Clear Methods ------");

  // Load data from store
  localStore
    .load()
    .then((data) => {
      console.log("Data Loaded Successfully:", data);
    })
    .then(() => {
      // localStore.clear();
      // console.log("Data Cleared");
    });

  console.log("------ Testing Remove Method ------");

  // Remove a record by key
  localStore
    .remove("1") // Ensure the key is a string
    .done((key) => {
      console.log("Record removed with key:", key);
    })
    .fail((error) => {
      console.log("Error removing record:", error);
    });

  console.log("------ Testing Update Method ------");

  // Update a record (use lowercase `id`)
  localStore
    .update("2", { name: "Updated Name", email: "Updated Email" })
    .done((dataObj, key) => {
      console.log("Updated record:", dataObj, "with key:", key);
    })
    .fail((error) => {
      console.log("Error updating record:", error);
    });

  console.log("------ Testing Total Count Method ------");

  // Get the total count of records in the store
  localStore
    .totalCount()
    .done((count) => {
      console.log("Total record count:", count);
    })
    .fail((error) => {
      console.log("Error fetching total count:", error);
    });
});
