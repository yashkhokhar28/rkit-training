$(document).ready(function () {
  document.getElementById("example").innerHTML = "Using JavaScript";
  $("#example").text("Using jQuery");

  $("h2").css("color", "blue");
  $("#example").html("<b>HTML content changed with jQuery</b>");

  $(".class-selector").text("Using class selector");
  $("#id-selector").text("Using ID selector");
  $("div").text("Using element selector");

  $("#clickButton").click(function () {
    alert("Button clicked!");
  });

  $("#fireEventButton").click(function () {
    $("#clickButton").trigger("click"); // Triggering the click event programmatically
  });

  $("#customEvent").on("customEvent", function () {
    console.log("Custom event fired!");
  });
  $("#customEvent").trigger("customEvent"); // Trigger custom event

  $("#form").on("submit", function (event) {
    event.preventDefault();
    const email = $("#email").val();
    if (!email) {
      alert("Email is required!");
    } else {
      alert("Form submitted with email: " + email);
    }
  });

  const arr = [1, 2, 3, 4];
  const mappedArr = $.map(arr, function (n) {
    return n * 2;
  });
  console.log("Mapped Array:", mappedArr);

  const filteredArr = $.grep(arr, function (n) {
    return n % 2 === 0;
  });
  console.log("Filtered Array:", filteredArr);

  $.each(arr, function (index, value) {
    console.log("Value at index", index, "is", value);
  });

  const obj1 = { name: "Alice" };
  const obj2 = { age: 25 };
  const mergedObj = $.extend({}, obj1, obj2);
  console.log("Merged Object:", mergedObj);

  $("#ajaxButton").click(function () {
    $.ajax({
      url: "https://jsonplaceholder.typicode.com/todos/1",
      type: "GET",
      success: function (data) {
        $("#ajaxContent").html(`<p>${JSON.stringify(data)}</p>`);
      },
      error: function () {
        $("#ajaxContent").html("<p>Error fetching data</p>");
      },
    });
  });
});
