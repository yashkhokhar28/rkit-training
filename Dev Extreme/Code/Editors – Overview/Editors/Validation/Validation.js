import {
  optionChangedHandler,
  initializedHandler,
  disposeHandler,
} from "../Events.js";

$(document).ready(() => {
  console.log("Document is Ready!!");

  // First Name
  $("#firstname-validation")
    .dxTextBox({
      validationMessageMode: "always",
      inputAttr: { "aria-label": "FirstName" },
    })
    .dxValidator({
      name: "FirstName",
      onDisposing: disposeHandler,
      onInitialized: initializedHandler,
      onOptionChanged: optionChangedHandler,
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },
      validationRules: [
        {
          type: "required",
          message: "First Name is required",
        },
      ],
    });

  // Last Name
  $("#lastname-validation")
    .dxTextBox({
      validationMessageMode: "always",
      inputAttr: { "aria-label": "LastName" },
    })
    .dxValidator({
      name: "LastName",
      onDisposing: disposeHandler,
      onInitialized: initializedHandler,
      onOptionChanged: optionChangedHandler,
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },
      validationRules: [
        {
          type: "required",
          message: "Last Name is required",
        },
      ],
    });

  // Email Address
  $("#email-validation")
    .dxTextBox({
      validationMessageMode: "always",
      inputAttr: { "aria-label": "EmailAddress" },
    })
    .dxValidator({
      name: "EmailAddress",
      onDisposing: disposeHandler,
      onInitialized: initializedHandler,
      onOptionChanged: optionChangedHandler,
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },
      validationRules: [
        {
          type: "required",
          message: "Email Address is required",
        },
        {
          type: "email",
          message: "Invalid Email Address!",
        },
      ],
    });

  // Password
  $("#password-validation")
    .dxTextBox({
      validationMessageMode: "always",
      inputAttr: { "aria-label": "Password" },
      mode: "password",
    })
    .dxValidator({
      name: "Password",
      onDisposing: disposeHandler,
      onInitialized: initializedHandler,
      onOptionChanged: optionChangedHandler,
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },
      validationRules: [
        {
          type: "required",
          message: "Password is required",
        },
        {
          type: "stringLength",
          min: 6,
          max: 12,
          message: "Password must be 6-12 characters!",
        },
      ],
    });

  // Confirm Password
  $("#confirm-password-validation")
    .dxTextBox({
      validationMessageMode: "always",
      inputAttr: { "aria-label": "ConfirmPassword" },
      mode: "password",
    })
    .dxValidator({
      name: "ConfirmPassword",
      onDisposing: disposeHandler,
      onInitialized: initializedHandler,
      onOptionChanged: optionChangedHandler,
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },
      validationRules: [
        {
          type: "required",
          message: "Confirm Password is required",
        },
        {
          type: "compare",
          comparisonTarget: function () {
            return $("#password-validation")
              .dxTextBox("instance")
              .option("value");
          },
          message: "Passwords do not match!",
        },
      ],
    });

  // Mobile Number
  $("#mobile-validation")
    .dxTextBox({
      validationMessageMode: "always",
      inputAttr: { "aria-label": "MobileNumber" },
    })
    .dxValidator({
      name: "MobileNumber",
      onDisposing: disposeHandler,
      onInitialized: initializedHandler,
      onOptionChanged: optionChangedHandler,
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },
      validationRules: [
        {
          type: "required",
          message: "Mobile Number is required",
        },
        {
          type: "pattern",
          pattern: "^[0-9]{10}$",
          message: "Enter a valid 10-digit number!",
        },
      ],
    });

  // Birth Date
  $("#birthdate-validation")
    .dxDateBox({
      validationMessageMode: "always",
      value: null,
      applyButtonText: "Confirm",
      pickerType: "rollers",
      useMaskBehavior: true,
      cancelButtonText: "Close",
      inputAttr: { "aria-label": "BirthDate" },
    })
    .dxValidator({
      name: "BirthDate",
      onDisposing: disposeHandler,
      onInitialized: initializedHandler,
      onOptionChanged: optionChangedHandler,
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },
      validationRules: [
        {
          type: "required",
          message: "Birth Date is required",
        },
        {
          type: "range",
          max: new Date(),
          min: new Date(2000, 0, 1),
          message: "Date is Not Valid",
        },
      ],
    });

  // Register Button with Validation Check
  $("#button").dxButton({
    text: "Register",
    type: "success",
    stylingMode: "outlined",
    useSubmitBehavior: true,
    onClick: function () {
      let result = DevExpress.validationEngine.validateGroup();
      console.log(result);
      if (result.isValid) {
        DevExpress.ui.notify("Registration Successful!", "success", 5000);
      } else {
        DevExpress.ui.notify("Please fix the errors!", "error", 5000);
      }
    },
  });
});
