$(document).ready(() => {
  $.ajax({
    url: "https://673596e65995834c8a934a4d.mockapi.io/score",
    type: "GET",
    success: function (data) {
      console.log(data);
      let name = document.getElementById("name");
      let score = document.getElementById("score");
      data = data.sort((a, b) => b["score"] - a["score"]);
      for (let i = 0; i < data.length; i++) {
        rank.innerHTML += i + 1 + "<br>";
        name.innerHTML += data[i]["username"] + "<br>";
        score.innerHTML += data[i]["score"] + "<br>";
      }
    },
    error: function (error) {
      console.log(error);
    },
  });
});
