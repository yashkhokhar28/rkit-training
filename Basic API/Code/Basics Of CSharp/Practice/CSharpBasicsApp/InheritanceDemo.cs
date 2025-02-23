using System;


namespace CSharpBasicsApp;
/// <summary>
/// Demonstrates the concept of Inheritance in OOP.
/// Includes types of inheritance: Single, Multilevel, and Hierarchical.
/// </summary>
public class InheritanceDemo
{
    /// <summary>
    /// Runs a demonstration of various inheritance types by invoking behaviors 
    /// from the Vehicle, Car, ElectricCar, and Truck classes.
    /// </summary>
    public static void RunInheritanceDemo()
    {
        // Single Inheritance
        Console.WriteLine("Single Inheritance:");
        Car car = new Car();
        car.StartEngine(); // Inherited from Vehicle
        car.Drive(); // Car-specific method

        // Multilevel Inheritance
        Console.WriteLine("\nMultilevel Inheritance:");
        ElectricCar tesla = new ElectricCar();
        tesla.StartEngine(); // Inherited from Vehicle
        tesla.Drive(); // Inherited from Car
        tesla.ChargeBattery(); // ElectricCar-specific method

        // Hierarchical Inheritance
        Console.WriteLine("\nHierarchical Inheritance:");
        Truck truck = new Truck();
        truck.StartEngine(); // Inherited from Vehicle
        truck.LoadCargo(); // Truck-specific method
    }
}

/// <summary>
/// Base class representing generic vehicle behaviors.
/// Provides a method to start the engine.
/// </summary>
public class Vehicle
{
    /// <summary>
    /// Starts the vehicle's engine.
    /// </summary>
    public void StartEngine()
    {
        Console.WriteLine("Vehicle's engine starts.");
    }
}

/// <summary>
/// Derived class representing Car-specific behaviors (Single Inheritance).
/// Adds functionality to drive the car.
/// </summary>
public class Car : Vehicle
{
    /// <summary>
    /// Simulates driving the car.
    /// </summary>
    public void Drive()
    {
        Console.WriteLine("Car is driving.");
    }
}

/// <summary>
/// Derived class representing ElectricCar-specific behaviors (Multilevel Inheritance).
/// Extends the Car class by adding functionality for charging its battery.
/// </summary>
public class ElectricCar : Car
{
    /// <summary>
    /// Simulates charging the electric car's battery.
    /// </summary>
    public void ChargeBattery()
    {
        Console.WriteLine("Electric car is charging its battery.");
    }
}

/// <summary>
/// Derived class representing Truck-specific behaviors (Hierarchical Inheritance).
/// Extends the Vehicle class by adding functionality to load cargo.
/// </summary>
public class Truck : Vehicle
{
    /// <summary>
    /// Simulates loading cargo onto the truck.
    /// </summary>
    public void LoadCargo()
    {
        Console.WriteLine("Truck is loading cargo.");
    }
}