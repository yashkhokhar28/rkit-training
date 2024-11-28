using System;

namespace CSharpBasicsApp;

using MathLibrary;

/// <summary>
/// Demonstrates the concept of Polymorphism in OOP.
/// Includes Compile-Time (Method Overloading) and Run-Time (Method Overriding) Polymorphism.
/// </summary>
public class PolymorphismDemo
{
    /// <summary>
    /// Runs a demonstration of polymorphism, showcasing method overloading
    /// (Compile-Time Polymorphism) and method overriding (Run-Time Polymorphism).
    /// </summary>
    public static void RunPolymorphismDemo()
    {
        MathOperations math = new MathOperations();
        Console.WriteLine("Addition: " + math.Add(5, 10));
        Console.WriteLine("Multiplication: " + math.Multiply(5, 10));

        // Compile-Time Polymorphism (Method Overloading)
        Console.WriteLine("Compile-Time Polymorphism:");
        Calculator calc = new Calculator();
        Console.WriteLine(calc.Add(5, 10)); // Add(int, int)
        Console.WriteLine(calc.Add(5.5, 10.5)); // Add(double, double)

        // Run-Time Polymorphism (Method Overriding)
        Console.WriteLine("\nRun-Time Polymorphism:");
        Animal animal = new Dog();
        animal.MakeSound(); // Calls overridden method in Dog class
    }
}

/// <summary>
/// Demonstrates Compile-Time Polymorphism using method overloading.
/// Contains overloaded methods for adding integers and doubles.
/// </summary>
public class Calculator
{
    /// <summary>
    /// Adds two integers and returns the result.
    /// </summary>
    /// <param name="a">The first integer.</param>
    /// <param name="b">The second integer.</param>
    /// <returns>The sum of the two integers.</returns>
    public int Add(int a, int b)
    {
        return a + b;
    }

    /// <summary>
    /// Adds two doubles and returns the result.
    /// </summary>
    /// <param name="a">The first double.</param>
    /// <param name="b">The second double.</param>
    /// <returns>The sum of the two doubles.</returns>
    public double Add(double a, double b)
    {
        return a + b;
    }
}

/// <summary>
/// Base class for demonstrating Run-Time Polymorphism.
/// Provides a virtual method that can be overridden in derived classes.
/// </summary>
public class Animal
{
    /// <summary>
    /// Simulates the sound made by an animal.
    /// This method can be overridden by derived classes.
    /// </summary>
    public virtual void MakeSound()
    {
        Console.WriteLine("Animal makes a sound.");
    }
}

/// <summary>
/// Derived class overriding the MakeSound method to provide a Dog-specific implementation.
/// </summary>
public class Dog : Animal
{
    /// <summary>
    /// Simulates the sound made by a dog (barking).
    /// Overrides the MakeSound method of the base class.
    /// </summary>
    public override void MakeSound()
    {
        Console.WriteLine("Dog barks.");
    }
}