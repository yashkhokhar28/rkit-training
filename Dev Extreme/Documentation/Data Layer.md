# ArrayStore

DevExtreme's **ArrayStore** is a client-side data store that manages JavaScript arrays efficiently. Below are key functionalities demonstrated:

#### **Initialization & Events**

- **`data`**: Stores dataset (`marvelCharacters`).
- **`key`**: Unique identifier (`ID`).
- Event handlers for loading, inserting, updating, deleting, modifying, and real-time push updates.

#### **Operations**

1. **Fetch Record** (`byKey(key)`) – Retrieves a record by ID.
2. **Insert Record** (`insert(data)`) – Adds a new record.
3. **Get Key Info** (`key()` & `keyOf(object)`) – Returns key properties.
4. **Load & Clear** (`load()` & `clear()`) – Loads data and clears the store.
5. **Push Changes** (`push([{ type: "insert", data }])`) – Adds records dynamically.
6. **Remove Record** (`remove(key)`) – Deletes a record by ID.
7. **Total Count** (`totalCount()`) – Retrieves the total number of records.
8. **Update Record** (`update(key, data)`) – Modifies an existing record.

# CustomStore

DevExtreme's **CustomStore** enables CRUD operations with an external API. Key functionalities include:

#### **Initialization & Events**

- **`key`**: Unique identifier (`id`).
- **`load`**: Fetches data from API and displays it dynamically.
- **CRUD Events**: Triggers before/after inserting, updating, and deleting records.

#### **Operations**

1. **Fetch Data** (`load()`) – Loads data from API.
2. **Insert Record** (`insert(values)`) – Adds a new user.
3. **Update Record** (`update(id, values)`) – Modifies an existing user.
4. **Delete Record** (`remove(id)`) – Deletes a user.
5. **Real-Time Updates** (`onPush(changes)`) – Handles live data changes.
