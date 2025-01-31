$(function () {
  $("#checkboxContainer").dxCheckBox({
    value: false,
    text: "Accept Terms & Conditions",
    onValueChanged: function (e) {
      console.log("Checked:", e.value);
    },
    disabled: false,
    readOnly: false,
    hint: "this is check box",
    elementAttr: { id: "chkbx" },
    focusStateEnabled: true,
    hoverStateEnabled: false,
    activeStateEnabled: true,
    rtlEnabled: true,
    visible: true,
    width: 200,
    height: 22,
    accessKey: "C",
    tabIndex: 0,
  });
});
