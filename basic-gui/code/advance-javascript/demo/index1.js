/**
 * Constructor function: Car
 * Description: Creates a Car object with the specified make, model, and year.
 * Parameters:
 *   - make (string): The manufacturer of the car.
 *   - model (string): The model of the car.
 *   - year (number): The year of manufacture.
 */
function Car(make, model, year) {
  this.make = make;
  this.model = model;
  this.year = year;
}

/**
 * Method: start
 * Description: Alerts that the engine of the car has started, displaying
 * the make, model, and year of the car.
 * Called on: Instance of Car object.
 * Parameters: None
 * Returns: None
 */
Car.prototype.start = function () {
  alert(`Engine started of ${this.make} ${this.model} ${this.year}`);
};

/**
 * Method: stop
 * Description: Alerts that the engine of the car has stopped, displaying
 * the make, model, and year of the car.
 * Called on: Instance of Car object.
 * Parameters: None
 * Returns: None
 */
Car.prototype.stop = function () {
  alert(`Engine stopped of ${this.make} ${this.model} ${this.year}`);
};

/**
 * Function: TestTraditionalWay
 * Description: Creates multiple instances of Car and tests their start
 * and stop methods by invoking them for each car.
 * Called in: <button type="button" onclick="TestTraditionalWay()">Start Engine</button>
 * Parameters: None
 * Returns: None
 */
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

/**
 * Class: Person
 * Description: Represents a person with a name and age. Provides methods
 * to display the person's name and age.
 */
class Person {
  /**
   * Constructor: Person
   * Description: Initializes a Person object with a name and age.
   * Parameters:
   *   - name (string): The name of the person.
   *   - age (number): The age of the person.
   */
  constructor(name, age) {
    this.name = name;
    this.age = age;
  }

  /**
   * Instance method: printName
   * Description: Displays the person's name on the webpage in the element
   * with id "result-name".
   * Called on: Instance of Person object.
   * Parameters: None
   * Returns: None
   */
  printName() {
    document.getElementById(
      "result-name"
    ).innerHTML = `Hello, I'm ${this.name}`;
  }

  /**
   * Static method: printAge
   * Description: Displays the specified age on the webpage in the element
   * with id "result-age".
   * Called on: Person class.
   * Parameters:
   *   - age (number): The age of the person to display.
   * Returns: None
   */
  static printAge(age) {
    document.getElementById("result-age").innerHTML = `I'm ${age} years old.`;
  }
}

/**
 * Function: MyFunc
 * Description: Handles the form submission event, creating a Person object
 * with the provided name and age, then displaying the name and age on the
 * webpage. If the fields are empty, an error message is displayed.
 * Called in: <form onsubmit="MyFunc(event)">
 * Parameters:
 *   - event (Event): The form submission event.
 * Returns: None
 */
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

/**
 * Function: FetchData
 * Description: An asynchronous function that fetches data from a sample API
 * and displays it on the webpage. If an error occurs during the fetch,
 * it logs the error to the console.
 * Called in: <button type="button" onclick="FetchData()">Test API</button>
 * Parameters: None
 * Returns: None
 */
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
