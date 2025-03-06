### **5 Navigation**

#### **5.1 Overview**

- DevExtreme provides navigation components for seamless UI interaction, including **Menu**, **TreeView**, and **Tabs**.
- Supports **adaptive layouts**, **icons**, **hierarchical structures**, and **event handling**.

---

#### **5.2 Menu**

- **Properties:**

  - `items` → Defines menu items as an array.
  - `orientation` → Sets the menu direction (`"horizontal"` / `"vertical"`).
  - `submenuDirection` → Controls submenu placement (`"auto"`, `"right"`, `"left"`).

- **Methods:**

  - `.option("items", newItems)` → Dynamically updates menu items.

- **Events:**

  - `onItemClick` → Fires when a menu item is clicked.

- **Example:**
  ```js
  $("#menu").dxMenu({
    items: [
      { text: "Home" },
      {
        text: "Products",
        items: [{ text: "Electronics" }, { text: "Clothing" }],
      },
    ],
    onItemClick: function (e) {
      console.log("Clicked:", e.itemData.text);
    },
  });
  ```

---

#### **5.3 TreeView**

- **Properties:**

  - `items` → Defines the hierarchical structure.
  - `showCheckBoxesMode` → Enables checkboxes (`"normal"`, `"selectAll"`, `"none"`).
  - `expandNodesRecursive` → Expands all nodes by default.

- **Methods:**

  - `.expandItem(itemKey)` → Expands a specific node.
  - `.collapseItem(itemKey)` → Collapses a specific node.

- **Events:**

  - `onItemClick` → Fires when a node is clicked.
  - `onItemSelectionChanged` → Fires when an item is selected.

- **Example:**
  ```js
  $("#treeView").dxTreeView({
    items: [
      {
        id: 1,
        text: "Parent 1",
        expanded: true,
        items: [{ id: 2, text: "Child 1" }],
      },
      { id: 3, text: "Parent 2", items: [{ id: 4, text: "Child 2" }] },
    ],
    showCheckBoxesMode: "normal",
    onItemClick: function (e) {
      console.log("Selected Node:", e.itemData.text);
    },
  });
  ```
