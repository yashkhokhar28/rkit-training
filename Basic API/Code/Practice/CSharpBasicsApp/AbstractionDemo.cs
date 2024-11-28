using System;

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates the concept of Abstraction in Object-Oriented Programming (OOP).
/// Abstraction is achieved using abstract classes and interfaces, focusing on essential features 
/// while hiding implementation details.
/// </summary>
public class AbstractionDemo
{
    /// <summary>
    /// Runs a demonstration of abstraction by showcasing the use of an abstract class
    /// and an interface in practical scenarios.
    /// </summary>
    public static void RunAbstractionDemo()
    {
        // Using an abstract class
        Console.WriteLine("Abstract Class:");
        Appliance appliance = new WashingMachine("LG");
        appliance.Start();
        Console.WriteLine($"Brand: {appliance.Brand}");

        // Using an interface
        Console.WriteLine("\nInterface:");
        ISmartDevice smartDevice = new SmartLight();
        smartDevice.ConnectToWifi();
    }
}

/// <summary>
/// Abstract class representing a generic appliance.
/// Serves as a base for specific appliance implementations.
/// </summary>
public abstract class Appliance
{
    /// <summary>
    /// Gets the brand of the appliance.
    /// </summary>
    public string Brand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Appliance"/> class with the specified brand.
    /// </summary>
    /// <param name="brand">The brand of the appliance.</param>
    public Appliance(string brand)
    {
        Brand = brand;
    }

    /// <summary>
    /// Starts the appliance. The implementation must be provided by derived classes.
    /// </summary>
    public abstract void Start();
}

/// <summary>
/// Represents a washing machine, a specific type of appliance.
/// Implements the abstract <see cref="Start"/> method.
/// </summary>
public class WashingMachine : Appliance
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WashingMachine"/> class with the specified brand.
    /// </summary>
    /// <param name="brand">The brand of the washing machine.</param>
    public WashingMachine(string brand) : base(brand)
    {
    }

    /// <summary>
    /// Starts the washing machine by providing a brand-specific implementation.
    /// </summary>
    public override void Start()
    {
        Console.WriteLine($"Washing Machine ({Brand}) is starting.");
    }
}

/// <summary>
/// Interface representing behaviors of a smart device.
/// Defines the contract for classes that implement smart device functionality.
/// </summary>
public interface ISmartDevice
{
    /// <summary>
    /// Connects the smart device to Wi-Fi.
    /// </summary>
    void ConnectToWifi();
}

/// <summary>
/// Represents a smart light that implements the <see cref="ISmartDevice"/> interface.
/// </summary>
public class SmartLight : ISmartDevice
{
    /// <summary>
    /// Connects the smart light to Wi-Fi with a specific implementation.
    /// </summary>
    public void ConnectToWifi()
    {
        Console.WriteLine("Smart Light is connected to Wi-Fi.");
    }
}
