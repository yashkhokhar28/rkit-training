# **Basics of Devextreme**

#### **Introduction to DevExtreme**

DevExtreme is a comprehensive UI component library by DevExpress, designed for building modern web applications. It provides a wide range of feature-rich widgets for data visualization, forms, navigation, and more, supporting multiple frameworks including jQuery, Angular, React, and Vue.

#### **Installation – NuGet Package**

To install DevExtreme in an ASP.NET Core or MVC project, use the NuGet Package Manager:

```sh
Install-Package DevExtreme.Web -Version 21.1.3
```

For front-end integration, include the necessary CSS and JavaScript files from DevExtreme's CDN or via npm.

#### **Widget Basics - jQuery**

DevExtreme provides jQuery-based widgets that can be initialized using the `$()` function. Each widget has a structured API for configuration, interaction, and event handling.

#### **Create and Configure a Widget**

A widget can be created using jQuery by targeting an HTML element and applying the corresponding DevExtreme widget method:

```html
<div id="gridContainer"></div>
<script>
  $("#gridContainer").dxDataGrid({
    dataSource: myData,
    columns: ["ID", "Name", "Age"],
  });
</script>
```

Options can be set directly during initialization.

#### **Get a Widget Instance**

To access a widget instance, use the `.dxWidgetName("instance")` method:

```js
var grid = $("#gridContainer").dxDataGrid("instance");
```

This allows interaction with the widget programmatically.

#### **Get and Set Options**

Retrieve or update widget properties using the `.option()` method:

```js
var pageSize = grid.option("paging.pageSize"); // Get option
grid.option("paging.pageSize", 20); // Set option
```

#### **Call Methods**

Widgets provide built-in methods that can be called using the instance:

```js
grid.refresh(); // Refresh data in DataGrid
```

#### **Handle Events**

DevExtreme widgets support event handling via the `on` event handlers:

```js
$("#gridContainer").dxDataGrid({
  onRowClick: function (e) {
    alert("Row clicked: " + e.data.Name);
  },
});
```

#### **Destroy a Widget**

To remove a widget instance and clean up associated resources, use:

```js
$("#gridContainer").dxDataGrid("dispose");
```

This prevents memory leaks and ensures proper removal of event listeners.
