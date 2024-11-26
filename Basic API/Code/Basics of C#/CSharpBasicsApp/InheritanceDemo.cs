namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates the concept of Inheritance in OOP.
/// Includes types of inheritance: Single, Multilevel, and Hierarchical.
/// </summary>
public class InheritanceDemo
{
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
/// </summary>
public class Vehicle
{
    public void StartEngine()
    {
        Console.WriteLine("Vehicle's engine starts.");
    }
}

/// <summary>
/// Derived class representing Car-specific behaviors (Single Inheritance).
/// </summary>
public class Car : Vehicle
{
    public void Drive()
    {
        Console.WriteLine("Car is driving.");
    }
}

/// <summary>
/// Derived class representing ElectricCar-specific behaviors (Multilevel Inheritance).
/// </summary>
public class ElectricCar : Car
{
    public void ChargeBattery()
    {
        Console.WriteLine("Electric car is charging its battery.");
    }
}

/// <summary>
/// Derived class representing Truck-specific behaviors (Hierarchical Inheritance).
/// </summary>
public class Truck : Vehicle
{
    public void LoadCargo()
    {
        Console.WriteLine("Truck is loading cargo.");
    }
}