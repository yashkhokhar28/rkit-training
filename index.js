// Function to fetch and render the Markdown file
const ShowDocumentation = (filePath) => {
  fetch(filePath)
    .then((response) => response.text())
    .then((markdown) => {
      const markdownPreview = document.getElementById("markdown-preview");
      if (markdownPreview) {
        markdownPreview.innerHTML = marked.parse(markdown); // Use marked.parse() for the correct method
      }
    })
    .catch((error) => console.error("Error loading the Markdown file:", error));
};

// Wait for the DOM to be fully loaded before attaching event listeners
document.addEventListener("DOMContentLoaded", () => {
  // You can now add any initialization logic here if needed
});
