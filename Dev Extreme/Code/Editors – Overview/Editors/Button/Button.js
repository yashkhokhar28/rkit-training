$(document).ready(() => {
  console.log("Document is Ready!!");

  var normalButton = $("#normalButton")
    .dxButton({
      stylingMode: "contained",
      text: "filled",
      type: "normal",
    })
    .dxButton("instance");
  var successButton = $("#successButton")
    .dxButton({
      stylingMode: "outlined",
      text: "Outlined",
      type: "success",
    })
    .dxButton("instance");
  var defaultButton = $("#defaultButton")
    .dxButton({
      stylingMode: "contained",
      text: "filled",
      type: "default",
    })
    .dxButton("instance");
  var dangerButton = $("#dangerButton")
    .dxButton({
      stylingMode: "outlined",
      text: "outlined",
      type: "danger",
    })
    .dxButton("instance");
});
