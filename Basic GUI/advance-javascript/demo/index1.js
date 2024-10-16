// Car constructor function with properties: make, model, and year
function Car(make, model, year) {
  this.make = make;
  this.model = model;
  this.year = year;
}

// Method to start the car engine
Car.prototype.start = function () {
  alert(`Engine started of ${this.make} ${this.model} ${this.year}`);
};

// Method to stop the car engine
Car.prototype.stop = function () {
  alert(`Engine stopped of ${this.make} ${this.model} ${this.year}`);
};

// Function to create multiple Car objects and test the start/stop methods
let TestTraditionalWay = () => {
  let car1 = new Car("Toyota", "Corolla", 2019);
  car1.start();
  car1.stop();

  let car2 = new Car("Honda", "Civic", 2020);
  car2.start();
  car2.stop();

  let car3 = new Car("Ford", "Fiesta", 2018);
  car3.start();
  car3.stop();
};

// ES6 Class: Person
class Person {
  constructor(name, age) {
    this.name = name;
    this.age = age;
  }

  // Instance method to display person's name on the webpage
  printName() {
    document.getElementById(
      "result-name"
    ).innerHTML = `Hello, I'm ${this.name}`;
  }

  // Static method to display person's age on the webpage
  static printAge(age) {
    document.getElementById("result-age").innerHTML = `I'm ${age} years old.`;
  }
}

// Function triggered on form submission to handle Person creation and display
let MyFunc = (event) => {
  event.preventDefault();

  // Getting input values for name and age
  let name = document.getElementById("name").value;
  let age = document.getElementById("age").value;

  if (name && age) {
    // Creating a Person object and displaying name and age
    let person1 = new Person(name, age);
    person1.printName();
    Person.printAge(age);
  } else {
    // Displaying error message if fields are not filled
    document.getElementById("result").innerHTML = "Please fill in both fields.";
  }
};

// Async function to fetch data from an API and display it on the webpage
let FetchData = async () => {
  try {
    // Fetching data from a sample API
    let response = await fetch("https://jsonplaceholder.typicode.com/todos/1");
    let data = await response.json();

    // Displaying fetched data on the webpage
    document.getElementById("api-data").innerHTML = JSON.stringify(data);
    console.log(data);
  } catch (error) {
    // Handling and logging any errors
    console.error("Error fetching data:", error);
  }
};
