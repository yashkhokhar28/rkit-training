$(function () {
  $("#buttonContainer").dxButton({
    text: "Submit",
    type: "success",
    icon: "check",
    disabled: false,
    onClick: function () {
      alert("Form submitted!");
    },
    stylingMode: "contained",
    width: 200,
    height: 50,
    hint: "Click to submit the form",
    elementAttr: { "data-test": "submit-btn" },
    useSubmitBehavior: true,
  });
});
