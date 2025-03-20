$(document).ready(() => {
  console.log("Document is Ready!!");

  var FileUploaderInstance = $("#FileUploader")
    .dxFileUploader({
      // Function to abort the upload process
      abortUpload: () => {},

      // Accept only image files
      accept: "image/*",

      // Shortcut key for focusing on the widget (Alt+X)
      accessKey: "X",

      // Enables the active state when the widget is focused or clicked
      activeStateEnabled: true,

      // Allows the user to cancel the file upload
      allowCanceling: true,

      // Allowed file extensions (image formats)
      allowedFileExtensions: [".jpg", ".jpeg", ".gif", ".png"],

      // The widget is not disabled
      disabled: false,

      // Custom attributes for the widget's root element
      elementAttr: {
        // Custom ID for the widget's root element
        id: "file-upload-id",

        // Custom class for the widget's root element
        class: "file-upload-class",
      },

      // Enables focus state when the widget is focused
      focusStateEnabled: true,

      // Enables hover effect when the widget is hovered
      hoverStateEnabled: true,

      // Tooltip hint text when hovering over the widget
      hint: "This is file uploader",

      // Message for invalid file extension
      invalidFileExtensionMessage: "This extension is invalid!!",

      // Message for exceeding max file size
      invalidMaxFileSizeMessage: "Uploaded file size is too big!!",

      // Message for too small file size
      invalidMinFileSizeMessage: "Uploaded file size is too small!!",

      // Marks the widget as valid initially
      isValid: true,

      // Max file size allowed (in bytes)
      maxFileSize: "100000000000000000",

      // Min file size allowed (in bytes)
      minFileSize: "1000",

      // Allow multiple file selection
      multiple: true,

      // Custom message for when the file is ready to upload
      readyToUploadMessage: "The file is ready to upload",

      // Custom text for the file selection button
      selectButtonText: "Click here to upload a file",

      // Show the file list after selecting files
      showFileList: true,

      // Message for when file uploading is aborted
      uploadAbortedMessage: "File uploading cancelled",

      // Text for the upload button
      uploadButtonText: "Start Upload",

      // Custom data to send with the upload
      uploadCustomData: {},

      // Message displayed after a successful upload
      uploadedMessage: "File uploaded!!",

      // Message displayed when file upload fails
      uploadFailedMessage: "File uploading failed!!",

      // HTTP method for the upload request
      uploadMethod: "POST",

      // How the file should be uploaded (use buttons to trigger upload)
      uploadMode: "useButtons",

      // Event handler before sending the file to the server
      onBeforeSend: (e) => {
        // Notify the user when the file upload is about to begin
        DevExpress.ui.notify("File is being sent", "info", 5000);
      },
    })
    .dxFileUploader("instance");

  // FileUploader with drag & drop and multiple file types allowed
  $("#fileuploader").dxFileUploader({
    multiple: true,
    accept: "*",
    uploadMode: "useButtons",
    dropZone: "#fileuploader",
    allowCanceling: true,

    // Show the file list after selecting files
    showFileList: true,

    // Custom label text for the file uploader
    labelText: "Drag & Drop files here or click to select",

    // Custom text for the upload button
    uploadButtonText: "Upload Selected Files",

    // Highlight the border on drag enter/leave
    onDropZoneEnter: (e) => {
      DevExpress.ui.notify("Drop zone entered", "info", 5000);
      $("#fileuploader").css("border", "2px solid green");
    },

    onDropZoneLeave: (e) => {
      DevExpress.ui.notify("Drop zone left", "info", 5000);
      $("#fileuploader").css("border", "2px dashed #007bff");
    },

    onProgress: function (e) {
      var percent = (e.bytesLoaded / e.bytesTotal) * 100;
      DevExpress.ui.notify(
        "Upload progress: " + percent + "%",
        "success",
        5000
      );
    },

    // Event handler when upload is aborted
    onUploadAborted: (e) => {
      // Notify the user when the file upload is aborted
      DevExpress.ui.notify(
        `File upload cancelled: ${e.file.name}`,
        "error",
        5000
      );
    },

    // Event handler when upload is completed
    onUploaded: (e) => {
      // Notify the user when the file upload is completed successfully
      DevExpress.ui.notify(
        `File uploaded successfully: ${e.file.name}`,
        "success",
        5000
      );
    },

    // Event handler for upload errors
    onUploadError: (e) => {
      // Notify the user when an error occurs during file upload
      DevExpress.ui.notify(
        `Error occurred while uploading file: ${e.file.name}`,
        "error",
        5000
      );
    },

    // Event handler when upload starts
    onUploadStarted: (e) => {
      // Notify the user when the file upload starts
      DevExpress.ui.notify(
        `File uploading started: ${e.file.name}`,
        "info",
        5000
      );
    },
  });

  // Cancel button that aborts the upload process
  $("#cancleButton").dxButton({
    type: "danger",
    stylingMode: "outlined",
    text: "Click here to abort upload",
    onClick: () => {
      FileUploaderInstance.abortUpload();
    },
  });

  // Upload button that triggers the file upload process
  $("#uploadButton").dxButton({
    type: "success",
    stylingMode: "outlined",
    text: "Click here to start upload",
    onClick: () => {
      FileUploaderInstance.upload();
    },
  });
});
