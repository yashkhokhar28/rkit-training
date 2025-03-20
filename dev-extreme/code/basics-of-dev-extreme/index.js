// Call Methods
$("#popup").dxPopup({
  visible: false,
  closeOnOutsideClick: true,
  contentTemplate: () => {
    const content = $("<div />");
    content.append(
      $("<img />").attr(
        "src",
        "https://cdn.imago-images.com/Images/header/hello-we-are-imago_03-2023.jpg"
      )
    );
    return content;
  },
});

var popupInstance = $("#popup").dxPopup("instance");

// Create and Configure a Widget
$("#buttonContainer").dxButton({
  text: "button",
  type: "success",
  stylingMode: "outlined",
  icon: "favorites",
});

// Get a Widget Instance (can only get if widget is initialized)
var isClicked = false;
var buttonInstance = $("#buttonContainer").dxButton("instance");
console.log("Button Instance " + buttonInstance);
buttonInstance.option({
  onClick: () => {
    // var isClicked = $(this).dxButton("option", "text") === "Click Me";
    // $(this).dxButton("option", {
    //   text: isClicked ? "Clicked!" : "Click Me",
    //   type: isClicked ? "success" : "default",
    //   icon: isClicked ? "favorites" : "add",
    // });
    isClicked = !isClicked;
    buttonInstance.option({
      text: isClicked ? "Clicked!" : "Click Me",
      type: isClicked ? "success" : "default",
      icon: isClicked ? "favorites" : "add",
    });
    //  $("#popup").dxPopup("show");
    popupInstance.show();
  },
});

// Get and Set Options
var buttonIcon = buttonInstance.option("icon");
console.log("Button Instance " + buttonInstance);
console.log(buttonIcon);
buttonInstance.option("icon", buttonIcon == "favorites" ? "add" : "favorites");

// Destroy a Widget
// buttonInstance.dxButton("dispose");
