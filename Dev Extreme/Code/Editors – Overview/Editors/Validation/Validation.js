import {
  optionChangedHandler,
  initializedHandler,
  disposeHandler,
} from "../Events.js";

// Document ready event handler using jQuery
$(document).ready(() => {
  // Log confirmation when document is fully loaded
  console.log("Document is Ready!!");

  // First Name Validation Setup
  $("#firstname-validation")
    .dxTextBox({
      // Always show validation message
      validationMessageMode: "always",

      // Accessibility attribute for input
      inputAttr: { "aria-label": "FirstName" },
    })
    .dxValidator({
      // Name for identification
      name: "FirstName",

      // Handle widget disposal
      onDisposing: disposeHandler,

      // Handle widget initialization
      onInitialized: initializedHandler,

      // Handle option changes
      onOptionChanged: optionChangedHandler,

      // Handle validation completion
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },

      // Validation rules
      validationRules: [
        {
          type: "required",
          message: "First Name is required",
        },
      ],
    });

  // Last Name Validation Setup
  $("#lastname-validation")
    .dxTextBox({
      // Always show validation message
      validationMessageMode: "always",

      // Accessibility attribute for input
      inputAttr: { "aria-label": "LastName" },
    })
    .dxValidator({
      // Name for identification
      name: "LastName",

      // Handle widget disposal
      onDisposing: disposeHandler,

      // Handle widget initialization
      onInitialized: initializedHandler,

      // Handle option changes
      onOptionChanged: optionChangedHandler,

      // Handle validation completion
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },

      // Validation rules
      validationRules: [
        {
          type: "required",
          message: "Last Name is required",
        },
      ],
    });

  // Email Address Validation Setup
  $("#email-validation")
    .dxTextBox({
      // Always show validation message
      validationMessageMode: "always",

      // Accessibility attribute for input
      inputAttr: { "aria-label": "EmailAddress" },
    })
    .dxValidator({
      // Name for identification
      name: "EmailAddress",

      // Handle widget disposal
      onDisposing: disposeHandler,

      // Handle widget initialization
      onInitialized: initializedHandler,

      // Handle option changes
      onOptionChanged: optionChangedHandler,

      // Handle validation completion
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },

      // Validation rules
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

  // Password Validation Setup
  $("#password-validation")
    .dxTextBox({
      // Always show validation message
      validationMessageMode: "always",

      // Accessibility attribute for input
      inputAttr: { "aria-label": "Password" },

      // Set input type to password
      mode: "password",
    })
    .dxValidator({
      // Name for identification
      name: "Password",

      // Handle widget disposal
      onDisposing: disposeHandler,

      // Handle widget initialization
      onInitialized: initializedHandler,

      // Handle option changes
      onOptionChanged: optionChangedHandler,

      // Handle validation completion
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },

      // Validation rules
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

  // Confirm Password Validation Setup
  $("#confirm-password-validation")
    .dxTextBox({
      // Always show validation message
      validationMessageMode: "always",

      // Accessibility attribute for input
      inputAttr: { "aria-label": "ConfirmPassword" },

      // Set input type to password
      mode: "password",
    })
    .dxValidator({
      // Name for identification
      name: "ConfirmPassword",

      // Handle widget disposal
      onDisposing: disposeHandler,

      // Handle widget initialization
      onInitialized: initializedHandler,

      // Handle option changes
      onOptionChanged: optionChangedHandler,

      // Handle validation completion
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },

      // Validation rules
      validationRules: [
        {
          type: "required",
          message: "Confirm Password is required",
        },
        {
          type: "compare",
          // Compare with password field value
          comparisonTarget: function () {
            return $("#password-validation")
              .dxTextBox("instance")
              .option("value");
          },
          message: "Passwords do not match!",
        },
      ],
    });

  // Mobile Number Validation Setup
  $("#mobile-validation")
    .dxTextBox({
      // Always show validation message
      validationMessageMode: "always",

      // Accessibility attribute for input
      inputAttr: { "aria-label": "MobileNumber" },
    })
    .dxValidator({
      // Name for identification
      name: "MobileNumber",

      // Handle widget disposal
      onDisposing: disposeHandler,

      // Handle widget initialization
      onInitialized: initializedHandler,

      // Handle option changes
      onOptionChanged: optionChangedHandler,

      // Handle validation completion
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },

      // Validation rules
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

  // Birth Date Validation Setup
  $("#birthdate-validation")
    .dxDateBox({
      // Always show validation message
      validationMessageMode: "always",

      // Initial value
      value: null,

      // Confirmation button text
      applyButtonText: "Confirm",

      // Date picker style
      pickerType: "rollers",

      // Enable mask behavior
      useMaskBehavior: true,

      // Cancel button text
      cancelButtonText: "Close",

      // Accessibility attribute for input
      inputAttr: { "aria-label": "BirthDate" },
    })
    .dxValidator({
      // Name for identification
      name: "BirthDate",

      // Handle widget disposal
      onDisposing: disposeHandler,

      // Handle widget initialization
      onInitialized: initializedHandler,

      // Handle option changes
      onOptionChanged: optionChangedHandler,

      // Handle validation completion
      onValidated: (e) => {
        if (e.isValid) {
          DevExpress.ui.notify(
            e.component.option("name") + " Validated Successfully!",
            "success",
            2000
          );
        }
      },

      // Validation rules
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

  // Register Button with Form Validation
  $("#button").dxButton({
    // Button text
    text: "Register",

    // Button type/style
    type: "success",

    // Button styling mode
    stylingMode: "outlined",

    // Enable form submission behavior
    useSubmitBehavior: true,

    // Handle button click and validate all fields
    onClick: function () {
      let result = DevExpress.validationEngine.validateGroup();
      console.log(result);
      if (result.isValid) {
        // Show success message on valid form
        DevExpress.ui.notify("Registration Successful!", "success", 5000);
      } else {
        // Show error message on invalid form
        DevExpress.ui.notify("Please fix the errors!", "error", 5000);
      }
    },
  });
});
