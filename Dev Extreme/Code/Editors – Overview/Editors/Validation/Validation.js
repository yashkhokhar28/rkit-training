$(document).ready(() => {
  console.log("Document is Ready!!");

  $("#firstname-validation")
    .dxTextBox({
      inputAttr: { "aria-label": "FirstName" },
    })
    .dxValidator({
      validationRules: [
        {
          type: "required",
          message: "First Name is required",
        },
      ],
    }),
    $("#lastname-validation").dxTextBox({});
  $("#email-validation").dxTextBox({});
  $("#mobile-validation").dxNumberBox({});
  $("#birthdate-validation").dxDateBox({});
});
