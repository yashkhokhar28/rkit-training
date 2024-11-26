using System;

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates the concept of Encapsulation in OOP.
/// Restricts access to data using properties.
/// </summary>
public class EncapsulationDemo
{
    public static void RunEncapsulationDemo()
    {
        Product product = new Product();
        product.Name = "Laptop"; // Setting the Name property
        product.Price = 75000; // Setting the Price property

        Console.WriteLine($"Product: {product.Name}, Price: ₹{product.Price}");
    }
}

/// <summary>
/// Class demonstrating encapsulation using private fields and public properties.
/// </summary>
public class Product
{
    private string _name; // Private field
    private double _price; // Private field

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            _name = value;
        }
    }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public double Price
    {
        get { return _price; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }

            _price = value;
        }
    }
}