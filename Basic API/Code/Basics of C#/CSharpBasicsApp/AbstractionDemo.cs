using System;

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates the concept of Abstraction in OOP.
/// Uses abstract classes and interfaces.
/// </summary>
public class AbstractionDemo
{
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
/// Abstract class representing a generic Appliance.
/// </summary>
public abstract class Appliance
{
    public string Brand { get; }

    public Appliance(string brand)
    {
        Brand = brand;
    }

    public abstract void Start(); // Abstract method
}

/// <summary>
/// Derived class implementing the abstract method Start.
/// </summary>
public class WashingMachine : Appliance
{
    public WashingMachine(string brand) : base(brand)
    {
    }

    public override void Start()
    {
        Console.WriteLine($"Washing Machine ({Brand}) is starting.");
    }
}

/// <summary>
/// Interface representing Smart Device behaviors.
/// </summary>
public interface ISmartDevice
{
    void ConnectToWifi();
}

/// <summary>
/// Class implementing the ISmartDevice interface.
/// </summary>
public class SmartLight : ISmartDevice
{
    public void ConnectToWifi()
    {
        Console.WriteLine("Smart Light is connected to Wi-Fi.");
    }
}