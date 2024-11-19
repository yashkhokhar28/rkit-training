using System;

namespace CSharpBasicsApp;

/// <summary>
/// Base class demonstrating inheritance and encapsulation.
/// </summary>
public class Animal
{
    /// <summary>
    /// Gets or sets the name of the animal.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Virtual method that can be overridden by derived classes to make a sound.
    /// </summary>
    public virtual void MakeSound()
    {
        Console.WriteLine("Animal sound");
    }
}

/// <summary>
/// Derived class representing a Dog, which inherits from Animal.
/// </summary>
public class Dog : Animal
{
    /// <summary>
    /// Overrides the MakeSound method to provide Dog-specific behavior.
    /// </summary>
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says: Woof!");
    }
}

/// <summary>
/// Interface defining actions for an animal, demonstrating abstraction.
/// </summary>
public interface IAnimalActions
{
    /// <summary>
    /// Method to be implemented for eating behavior.
    /// </summary>
    void Eat();

    /// <summary>
    /// Method to be implemented for sleeping behavior.
    /// </summary>
    void Sleep();
}

/// <summary>
/// Class representing a Cat, which inherits from Animal and implements IAnimalActions.
/// </summary>
public class Cat : Animal, IAnimalActions
{
    /// <summary>
    /// Overrides the MakeSound method to provide Cat-specific behavior.
    /// </summary>
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says: Meow!");
    }

    /// <summary>
    /// Implements the Eat method from the IAnimalActions interface.
    /// </summary>
    public void Eat()
    {
        Console.WriteLine($"{Name} is eating.");
    }

    /// <summary>
    /// Implements the Sleep method from the IAnimalActions interface.
    /// </summary>
    public void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping.");
    }
}

/// <summary>
/// Class demonstrating Object-Oriented Programming concepts in C#.
/// </summary>
public class OOPSDemo
{
    /// <summary>
    /// Runs the OOP demonstration by creating and interacting with Dog and Cat objects.
    /// </summary>
    public static void RunOOPSDemo()
    {
        #region Dog Example

        // Creating an instance of Dog and demonstrating inheritance
        Dog dog = new Dog { Name = "Buddy" };
        dog.MakeSound(); // Output: Buddy says: Woof!

        #endregion

        #region Cat Example

        // Creating an instance of Cat and demonstrating interface implementation and polymorphism
        Cat cat = new Cat { Name = "Whiskers" };
        cat.MakeSound(); // Output: Whiskers says: Meow!
        cat.Eat(); // Output: Whiskers is eating.
        cat.Sleep(); // Output: Whiskers is sleeping.

        #endregion

        #region Polymorphism Example

        // Demonstrating polymorphism with base class reference
        Animal myAnimal = new Dog { Name = "Max" };
        myAnimal.MakeSound(); // Output: Max says: Woof! (Calls the Dog's overridden method)

        #endregion
    }
}