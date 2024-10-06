function callPopup(params) {
  alert("hii");
}

const checkAge = () => {
  const ageInput = document.getElementById("age");
  const age = ageInput.value;
  console.log(age);

  if (age > 18) {
    alert("Adult");
  } else {
    alert("Minor");
  }

  ageInput.value = "";
};

const buttonIds = [
  "mouse-over",
  "click",
  "dblclick",
  "mousedown",
  "mouseup",
  "mousemove",
  "mouseout",
  "mouseenter",
];

buttonIds.forEach((id) => {
  const button = document.getElementById(id);

  // Add mouseover event
  if (id === "mouse-over") {
    button.addEventListener("mouseover", () => {
      alert("Mouse Over on " + id);
    });
  }

  // Add click event
  if (id === "click") {
    button.addEventListener("click", () => {
      alert("Button Clicked: " + id);
    });
  }

  // Add double click event
  if (id === "dblclick") {
    button.addEventListener("dblclick", () => {
      alert("Button Double Clicked: " + id);
    });
  }

  // Add mouse down event
  if (id === "mousedown") {
    button.addEventListener("mousedown", () => {
      alert("Mouse Down on " + id);
    });
  }

  // Add mouse up event
  if (id === "mouseup") {
    button.addEventListener("mouseup", () => {
      alert("Mouse Up on " + id);
    });
  }

  // Add mouse move event
  if (id === "mousemove") {
    button.addEventListener("mousemove", () => {
      alert("Mouse Move on " + id);
    });
  }

  // Add mouse out event
  if (id === "mouseout") {
    button.addEventListener("mouseout", () => {
      alert("Mouse Out from " + id);
    });
  }

  // Add mouse enter event
  if (id === "mouseenter") {
    button.addEventListener("mouseenter", () => {
      alert("Mouse Enter on " + id);
    });
  }
});
