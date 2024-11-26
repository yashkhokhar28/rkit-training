using System;

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates the concept of Polymorphism in OOP.
/// Includes Compile-Time (Method Overloading) and Run-Time (Method Overriding) Polymorphism.
/// </summary>
public class PolymorphismDemo
{
    public static void RunPolymorphismDemo()
    {
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
/// </summary>
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }
}

/// <summary>
/// Base class for demonstrating Run-Time Polymorphism.
/// </summary>
public class Animal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("Animal makes a sound.");
    }
}

/// <summary>
/// Derived class overriding the MakeSound method.
/// </summary>
public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Dog barks.");
    }
}