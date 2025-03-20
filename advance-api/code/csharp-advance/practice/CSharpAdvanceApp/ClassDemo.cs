namespace CSharpAdvanceApp
{
    /// <summary>
    /// Demonstrates advanced C# concepts like abstraction, inheritance, polymorphism, partial and sealed classes.
    /// </summary>
    public class ClassDemo
    {
        /// <summary>
        /// Entry point for demonstrating the usage of various classes and concepts.
        /// </summary>
        public void RunClassDemo()
        {
            // Demonstrate polymorphism: an Animal reference pointing to a Cat object.
            Animal animal = new Cat();
            animal.AnimalVoice();

            // Create and display Person objects using different constructors.
            Person person1 = new Person(1); // Using integer-based constructor.
            Person person2 = new Person("Yash"); // Using string-based constructor.
            person1.PrintData();
            person2.PrintPersonData();
        }
    }

    /// <summary>
    /// Abstract class representing a generic Animal.
    /// </summary>
    abstract class Animal
    {
        /// <summary>
        /// Abstract method to be implemented by derived classes to define the animal's voice.
        /// </summary>
        public abstract void AnimalVoice();
    }

    /// <summary>
    /// Concrete class representing a Cat, inheriting from Animal.
    /// </summary>
    internal class Cat : Animal
    {
        /// <summary>
        /// Implementation of AnimalVoice for Cat, outputs a cat sound.
        /// </summary>
        public override void AnimalVoice()
        {
            Console.WriteLine("Mewww");
        }
    }

    /// <summary>
    /// Sealed class representing a Vehicle. Cannot be inherited.
    /// </summary>
    sealed class Vehicle
    {
        /// <summary>
        /// Starts the vehicle's engine.
        /// </summary>
        public void StartEngine()
        {
            Console.WriteLine("Engine Started");
        }
    }

    // Uncommenting the following code will cause a compilation error since Vehicle is a sealed class.
    // public class Car : Vehicle { }

    /// <summary>
    /// Partial class representing a Person, demonstrating the use of partial classes.
    /// </summary>
    partial class Person
    {
        public int PersonID { get; set; }

        /// <summary>
        /// Constructor to initialize a Person with an ID.
        /// </summary>
        /// <param name="personID">The ID of the person.</param>
        public Person(int personID)
        {
            PersonID = personID;
        }

        /// <summary>
        /// Prints the Person's ID to the console.
        /// </summary>
        public void PrintData()
        {
            Console.WriteLine(PersonID);
        }
    }

    /// <summary>
    /// Partial class representing additional data for a Person.
    /// </summary>
    partial class Person
    {
        public string PersonName { get; set; }

        /// <summary>
        /// Constructor to initialize a Person with a name.
        /// </summary>
        /// <param name="personName">The name of the person.</param>
        public Person(string personName)
        {
            PersonName = personName;
        }

        /// <summary>
        /// Prints the Person's name to the console.
        /// </summary>
        public void PrintPersonData()
        {
            Console.WriteLine(PersonName);
        }
    }
}