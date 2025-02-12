import { marvelCharacters } from "./marvel-names.js";

$(document).ready(() => {
  // Step function to aggregate the strength level of heroes
  var step = function (total, hero) {
    return total + hero.strengthLevel;
  };

  // Finalize function to calculate the average strength level
  var finalize = function (total) {
    return (total * 100) / marvelCharacters.length / 100;
  };

  // Aggregate strength levels with a seed value and finalize function
  DevExpress.data
    .query(marvelCharacters)
    .aggregate(0, step, finalize)
    .done(function (total) {
      console.log(`Average Power : ${total}%`);
    });

  // Aggregate strength levels without a seed value
  DevExpress.data
    .query(marvelCharacters)
    .aggregate(0, step)
    .done(function (total) {
      console.log(`Total Power : ${total}`);
    });

  // Calculate the average strength level of heroes
  DevExpress.data
    .query(marvelCharacters)
    .avg("strengthLevel")
    .done(function (total) {
      console.log(`Average Power : ${total}`);
    });

  // Count the number of heroes
  DevExpress.data
    .query(marvelCharacters)
    .count()
    .done(function (total) {
      console.log(`Total Count : ${total}`);
    });

  // Convert the query result to an array with indices
  const enumeratedData = DevExpress.data.query(marvelCharacters).enumerate();
  console.log("Data:", enumeratedData);

  // Filter heroes with strength level greater than 80
  const filteredData = DevExpress.data
    .query(marvelCharacters)
    .filter(["strengthLevel", ">", 80])
    .toArray();
  console.log("Filtered Data:", filteredData);

  // Filter heroes with strength level greater than or equal to 90
  const filteredData2 = DevExpress.data
    .query(marvelCharacters)
    .filter((hero) => hero.strengthLevel >= 90)
    .toArray();
  console.log("Filtered Data:", filteredData2);

  // Group heroes by strength level (rounded to the nearest 10)
  const groupedHeroes = DevExpress.data
    .query(marvelCharacters)
    .groupBy((hero) => Math.floor(hero.strengthLevel / 10) * 10)
    .toArray();
  console.log("Grouped Heroes:", groupedHeroes);

  // Find the maximum strength level among heroes
  DevExpress.data
    .query(marvelCharacters)
    .max("strengthLevel")
    .done((strongestHero) =>
      console.log("Strongest Hero Strength:", strongestHero)
    );

  // Find the minimum strength level among heroes
  DevExpress.data
    .query(marvelCharacters)
    .min("strengthLevel")
    .done((weakestHero) => console.log("Weakest Hero Strength:", weakestHero));

  // Select hero names from the query result
  const heroNames = DevExpress.data
    .query(marvelCharacters)
    .select((hero) => hero.Name)
    .toArray();
  console.log("Hero Names:", heroNames);

  // Slice a subset of the query result
  const subset = DevExpress.data.query(marvelCharacters).slice(1, 3).toArray();
  console.log("Subset:", subset);

  // Sort heroes by age in ascending order
  const sortedByAge = DevExpress.data
    .query(marvelCharacters)
    .sortBy((hero) => hero.age)
    .toArray();
  console.log("Sorted by Age:", sortedByAge);

  // Sort heroes by age in descending order
  const sortedByAgeDesc = DevExpress.data
    .query(marvelCharacters)
    .sortBy((hero) => hero.age, true)
    .toArray();
  console.log("Sorted by Age Desc:", sortedByAgeDesc);

  // Sum up the strength levels of all heroes
  DevExpress.data
    .query(marvelCharacters)
    .sum("strengthLevel")
    .done((totalStrength) => console.log("Total Strength:", totalStrength));

  // Sort heroes by strength level in descending order, then by age in ascending order
  const sortedThenByAge = DevExpress.data
    .query(marvelCharacters)
    .sortBy((hero) => hero.strengthLevel, true)
    .thenBy((hero) => hero.age)
    .toArray();
  console.log("Sorted by Strength, then by Age:", sortedThenByAge);

  // Convert the query result to an array
  const heroesArray = DevExpress.data.query(marvelCharacters).toArray();
  console.log("Heroes Array:", heroesArray);
});
