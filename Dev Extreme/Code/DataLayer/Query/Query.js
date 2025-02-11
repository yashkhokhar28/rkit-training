import { marvelCharacters } from "./marvel-names.js";

$(document).ready(() => {
  var step = function (total, marvelCharacters) {
    return total + marvelCharacters.strengthLevel;
  };

  var finalize = function (total) {
    return (total * 100) / marvelCharacters.length / 100;
  };

  // aggregate(seed, step, finalize) – Custom aggregation function
  DevExpress.data
    .query(marvelCharacters)
    .aggregate(0, step, finalize)
    .then(function (total) {
      console.log(`Average Power : ${total}%`);
    })
    .catch(() => {
      alert("Error");
    });

  // aggregate(step) – Aggregate without seed
  var step = function (total, marvelCharacters) {
    return total + marvelCharacters.strengthLevel;
  };

  DevExpress.data
    .query(marvelCharacters)
    .aggregate(0, step)
    .then(function (total) {
      console.log(`Total Power : ${total}`);
    })
    .catch(() => {
      alert("Error");
    });

  // avg() – Average strength level
});
