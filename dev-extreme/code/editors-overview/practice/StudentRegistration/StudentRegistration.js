import { initializedHandler } from "../Events.js";

$(document).ready(() => {
  console.log("Document is Ready!!");

  // Utility function to create an async data source from a JSON file
  const makeAsyncDataSource = (jsonFile) => {
    return new DevExpress.data.CustomStore({
      loadMode: "raw",
      key: "id",
      load: () => $.getJSON(jsonFile),
    });
  };

  // First Name TextBox
  $("#firstname")
    .dxTextBox({
      activeStateEnabled: true,
      focusStateEnabled: true,
      hoverStateEnabled: true,
      hint: "First Name",
      maxLength: 15,
      inputAttr: { autocomplete: "off" },
      placeholder: "First Name",
      spellcheck: true,
      stylingMode: "filled",
      onInitialized: initializedHandler,
    })
    .dxValidator({
      name: "FirstName",
      validationRules: [
        { type: "required", message: "First Name is required" },
      ],
    });

  // Last Name TextBox
  $("#lastname")
    .dxTextBox({
      activeStateEnabled: true,
      focusStateEnabled: true,
      hoverStateEnabled: true,
      hint: "Last Name",
      maxLength: 15,
      inputAttr: { autocomplete: "off" },
      placeholder: "Last Name",
      spellcheck: true,
      stylingMode: "filled",
      onInitialized: initializedHandler,
    })
    .dxValidator({
      name: "LastName",
      validationRules: [{ type: "required", message: "Last Name is required" }],
    });

  // Gender RadioGroup
  $("#gender")
    .dxRadioGroup({
      activeStateEnabled: true,
      focusStateEnabled: true,
      hoverStateEnabled: true,
      items: ["Male", "Female"],
      layout: "horizontal",
      value: null,
      onInitialized: initializedHandler,
    })
    .dxValidator({
      name: "Gender",
      validationRules: [{ type: "required", message: "Gender is required" }],
    });

  // Date of Birth DateBox
  $("#dob")
    .dxDateBox({
      activeStateEnabled: true,
      focusStateEnabled: true,
      hoverStateEnabled: true,
      acceptCustomValue: false,
      type: "date",
      pickerType: "rollers",
      placeholder: "Birth Date",
      showClearButton: true,
      applyButtonText: "Confirm",
      cancelButtonText: "Close",
      min: new Date(2000, 0, 1),
      max: new Date(),
      dateSerializationFormat: "yyyy-MM-dd",
      stylingMode: "filled",
      openOnFieldClick: false,
      onInitialized: initializedHandler,
    })
    .dxValidator({
      name: "BirthDate",
      validationRules: [
        { type: "required", message: "Birth Date is required" },
      ],
    });

  // Email TextBox
  $("#email")
    .dxTextBox({
      activeStateEnabled: true,
      focusStateEnabled: true,
      hoverStateEnabled: true,
      hint: "Email Address",
      maxLength: 40,
      inputAttr: { autocomplete: "off" },
      placeholder: "Email Address",
      spellcheck: true,
      stylingMode: "filled",
      onInitialized: initializedHandler,
    })
    .dxValidator({
      name: "EmailAddress",
      validationRules: [
        { type: "required", message: "Email Address is required" },
        { type: "email", message: "Invalid Email Address!" },
      ],
    });

  // Phone Number TextBox with Mask
  $("#phone")
    .dxTextBox({
      activeStateEnabled: true,
      focusStateEnabled: true,
      hoverStateEnabled: true,
      hint: "Phone Number",
      maxLength: 14,
      inputAttr: { autocomplete: "off" },
      placeholder: "Phone Number",
      spellcheck: true,
      stylingMode: "filled",
      mask: "+91 00000-00000",
      useMaskedValue: true,
      maskChar: "_",
      onInitialized: initializedHandler,
    })
    .dxValidator({
      name: "PhoneNumber",
      validationRules: [
        { type: "required", message: "Phone Number is required" },
        {
          type: "pattern",
          pattern: "^\\+91 [0-9]{5}-[0-9]{5}$",
          message: "Enter a valid phone number in the format +91 00000-00000!",
        },
      ],
    });

  // Address TextArea
  $("#address")
    .dxTextArea({
      activeStateEnabled: true,
      focusStateEnabled: true,
      hoverStateEnabled: true,
      hint: "Address",
      maxLength: 1000,
      inputAttr: { autocomplete: "off" },
      placeholder: "Address",
      spellcheck: true,
      stylingMode: "filled",
      onInitialized: initializedHandler,
      maxHeight: 500,
      minHeight: 100,
    })
    .dxValidator({
      name: "Address",
      validationRules: [{ type: "required", message: "Address is required" }],
    });

  // Job Role DropDownBox with async data source
  $("#jobrole")
    .dxDropDownBox({
      dataSource: makeAsyncDataSource("jobRole.json"),
      displayExpr: "name",
      valueExpr: "id",
      placeholder: "Job Role",
      dropDownButtonTemplate: () => $("<span>").text("▼"),
      dropDownOptions: { closeOnOutsideClick: true },
      openOnFieldClick: true,
      contentTemplate: (e) => {
        const $list = $("<div>").dxList({
          searchEnabled: true,
          searchEditorOptions: {
            placeholder: "Enter Job Role",
            showClearButton: true,
          },
          searchExpr: ["name"],
          dataSource: e.component.getDataSource(),
          showSelectionControls: true,
          selectionMode: "single",
          itemTemplate: (itemData) => $("<div>").text(itemData.name),
          onItemClick: (args) => {
            const selectedItems = args.itemData;
            e.component.option("value", selectedItems.id);
            e.component.close();
          },
        });
        return $list;
      },
    })
    .dxValidator({
      name: "JobRole",
      validationRules: [{ type: "required", message: "Job Role is required" }],
    });

  // Preferred Contact Method SelectBox
  $("#contact-method")
    .dxSelectBox({
      items: ["Email", "Phone", "WhatsApp"],
      placeholder: "Preferred Contact Method",
      dropDownButtonTemplate: () => $("<span>").text("▼"),
      dropDownOptions: { closeOnOutsideClick: true },
      openOnFieldClick: true,
    })
    .dxValidator({
      name: "PreferredContactMethod",
      validationRules: [
        { type: "required", message: "Preferred Contact Method is required" },
      ],
    });

  // Password TextBox
  $("#password")
    .dxTextBox({
      mode: "password",
      activeStateEnabled: true,
      focusStateEnabled: true,
      hoverStateEnabled: true,
      hint: "Password",
      inputAttr: { autocomplete: "off" },
      placeholder: "Password",
      spellcheck: true,
      stylingMode: "filled",
      onInitialized: initializedHandler,
    })
    .dxValidator({
      name: "Password",
      validationRules: [
        { type: "required", message: "Password is required" },
        {
          type: "stringLength",
          min: 6,
          max: 12,
          message: "Password must be 6-12 characters!",
        },
      ],
    });

  // Confirm Password TextBox
  $("#confirm-password")
    .dxTextBox({
      mode: "password",
      activeStateEnabled: true,
      focusStateEnabled: true,
      hoverStateEnabled: true,
      hint: "Confirm Password",
      inputAttr: { autocomplete: "off" },
      placeholder: "Confirm Password",
      spellcheck: true,
      stylingMode: "filled",
      onInitialized: initializedHandler,
    })
    .dxValidator({
      name: "ConfirmPassword",
      validationRules: [
        { type: "required", message: "Confirm Password is required" },
        {
          type: "compare",
          comparisonTarget: () =>
            $("#password").dxTextBox("instance").option("value"),
          message: "Passwords do not match!",
        },
      ],
    });

  // Profile Picture FileUploader
  $("#fileuploader")
    .dxFileUploader({
      activeStateEnabled: true,
      focusStateEnabled: true,
      hoverStateEnabled: true,
      maxFileSize: 1000000, // (1MB)
      minFileSize: 1000, // (1KB)
      uploadMode: "useButtons",
      multiple: false,
      labelText: "Drag & Drop Profile Picture",
      accept: "image/*",
      allowCanceling: true,
      uploadUrl:
        "https://js.devexpress.com/Demos/WidgetsGalleryDataService/api/ChunkUpload",
      onInitialized: initializedHandler,
    })
    .dxValidator({
      name: "ProfilePicture",
      validationRules: [
        { type: "required", message: "Profile Picture is required" },
      ],
    });

  // Terms & Conditions CheckBox
  $("#terms")
    .dxCheckBox({
      focusStateEnabled: true,
      hoverStateEnabled: true,
      activeStateEnabled: true,
      text: "I Agree to Terms & Conditions",
      onInitialized: initializedHandler,
    })
    .dxValidator({
      validationRules: [
        {
          type: "compare",
          comparisonTarget: () => true,
          message: "You must agree to the Terms and Conditions",
        },
      ],
    });

  // Submit Button Click Event
  $("#submit-button").dxButton({
    text: "Register",
    type: "success",
    useSubmitBehavior: true,
    stylingMode: "contained",
    onClick: () => {
      const result = DevExpress.validationEngine.validateGroup();
      if (result.isValid) {
        saveToSession();
        $("#pop-up").dxPopup("instance").show();
      } else {
        DevExpress.ui.notify("Please fix the validation errors", "error", 5000);
      }
    },
  });

  // Initialize Popup
  $("#pop-up").dxPopup({
    title: "Registration Details",
    width: 500,
    height: "auto",
    visible: false,
    showCloseButton: true,
    dragEnabled: true,
    closeOnOutsideClick: true,
    onHidden: () => {
      window.location.reload();
    },
    contentTemplate: () => {
      const data = JSON.parse(sessionStorage.getItem("formData"));
      const $content = $("<div>");
      if (data) {
        $content.append(`
          <p><strong>First Name:</strong> ${data.firstName}</p>
          <p><strong>Last Name:</strong> ${data.lastName}</p>
          <p><strong>Gender:</strong> ${data.gender}</p>
          <p><strong>DOB:</strong> ${data.dob}</p>
          <p><strong>Email:</strong> ${data.email}</p>
          <p><strong>Phone:</strong> ${data.phone}</p>
          <p><strong>Address:</strong> ${data.address}</p>
          <p><strong>Job Role:</strong> ${data.jobRole}</p>
          <p><strong>Preferred Contact:</strong> ${data.contactMethod}</p>
        `);
      }
      return $content;
    },
  });

  // Function to Save Data to Session Storage
  const saveToSession = () => {
    const formData = {
      firstName: $("#firstname").dxTextBox("instance").option("value"),
      lastName: $("#lastname").dxTextBox("instance").option("value"),
      gender: $("#gender").dxRadioGroup("instance").option("value"),
      dob: $("#dob").dxDateBox("instance").option("value"),
      email: $("#email").dxTextBox("instance").option("value"),
      phone: $("#phone").dxTextBox("instance").option("value"),
      address: $("#address").dxTextArea("instance").option("value"),
      jobRole: $("#jobrole").dxDropDownBox("instance").option("value"),
      contactMethod: $("#contact-method")
        .dxSelectBox("instance")
        .option("value"),
      password: $("#password").dxTextBox("instance").option("value"),
    };

    sessionStorage.setItem("formData", JSON.stringify(formData));
  };
});
