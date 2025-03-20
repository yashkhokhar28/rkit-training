$(document).ready(() => {
  console.log("Document is Ready!!");

  console.log("Marvel Characters Data:", marvelCharacters);

  // Initializing ArrayStore with sample data
  var arrayStore = new DevExpress.data.ArrayStore({
    // The dataset that will be managed by ArrayStore
    data: marvelCharacters,

    // Unique key property to identify records
    key: "ID",

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
    errorHandler: (error) => {
      console.log("Error:", error);
    },
  });

  console.log("------ Testing byKey Method ------");

  // Fetch a record by key (valid key)
  arrayStore
    .byKey(1)
    .done((dataItem) => {
      console.log("Record found:", dataItem);
    })
    .fail((error) => {
      console.log("Error fetching record:", error);
    });

  // Fetch a record by key (invalid key)
  arrayStore
    .byKey(100)
    .done((dataItem) => {
      console.log("Record found:", dataItem);
    })
    .fail((error) => {
      console.log("Record not found:", error);
    });

  console.log("------ Testing Insert Method ------");

  // Insert a new record
  arrayStore
    .insert({ ID: 100, Name: "New Character" })
    .done((dataObj, key) => {
      console.log("Inserted record:", dataObj, "with key:", key);
    })
    .fail((error) => {
      console.log("Error inserting record:", error);
    });

  console.log("------ Testing Key Methods ------");

  // Get the key property of the store
  var keyProps = arrayStore.key();
  console.log("Key Property:", keyProps);

  // Get the key of an object
  var key = arrayStore.keyOf({ ID: 1, Name: "Iron Man" });
  console.log("Key of given object:", key);

  console.log("------ Testing Load & Clear Methods ------");

  // Load data from the store and then clear it
  arrayStore
    .load()
    .then((data) => {
      console.log("Data Loaded Successfully:", data);
    })
    .then(() => {
      arrayStore.clear();
      console.log("Data Cleared");
    });

  console.log("------ Testing Push Method ------");

  // New character to be added using push
  var newMarvelCharacter = {
    ID: 16,
    Name: "Silver Surfer",
    Real_Name: "Norrin Radd",
    Affiliation: "Herald of Galactus, Defenders",
    First_Appearance: 1966,
    Abilities: [
      "Cosmic Energy Manipulation",
      "Superhuman Strength",
      "Interstellar Travel",
      "Energy Projection",
    ],
    Status: "Active",
    ImageSrc: "images/marvel/silver_surfer.png",
  };

  // Push a new record into the store
  arrayStore.push([
    {
      type: "insert",
      data: newMarvelCharacter,
    },
  ]);
  console.log("Record pushed:", newMarvelCharacter);

  console.log("------ Testing Remove Method ------");

  // Remove a record by key
  arrayStore
    .remove(1)
    .done((key) => {
      console.log("Record removed with key:", key);
    })
    .fail((error) => {
      console.log("Error removing record:", error);
    });

  console.log("------ Testing Total Count Method ------");

  // Get the total count of records in the store
  arrayStore
    .totalCount()
    .done((count) => {
      console.log("Total record count:", count);
    })
    .fail((error) => {
      console.log("Error fetching total count:", error);
    });

  console.log("------ Testing Update Method ------");

  // Update a record
  arrayStore
    .update(2, { Name: "Updated Name" })
    .done((dataObj, key) => {
      console.log("Updated record:", dataObj, "with key:", key);
    })
    .fail((error) => {
      console.log("Error updating record:", error);
    });
});
