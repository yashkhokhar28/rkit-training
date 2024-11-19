using System;

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates the use of scope and access modifiers in C#.
/// </summary>
public class ScopeAndAccessModifierDemo
{
    #region Instance Variables

    /// <summary>
    /// Public instance variable: Accessible from outside the class.
    /// </summary>
    public int publicVariable = 1;

    /// <summary>
    /// Private instance variable: Accessible only within the class.
    /// </summary>
    private int privateVariable = 2;

    /// <summary>
    /// Protected instance variable: Accessible within the class and derived classes.
    /// </summary>
    protected int protectedVariable = 3;

    /// <summary>
    /// Internal instance variable: Accessible within the same assembly.
    /// </summary>
    internal int internalVariable = 4;

    /// <summary>
    /// Static variable: Shared across all instances of the class.
    /// </summary>
    private static int staticVariable = 5;

    #endregion

    /// <summary>
    /// Runs the demonstration of scope and access modifiers.
    /// </summary>
    public static void RunScopeAndAccessModifierDemo()
    {
        #region Local Variable Example

        // Local variable: Scope is limited to this method
        int localVariable = 10;
        Console.WriteLine("Local variable = " + localVariable);

        #endregion

        #region Instance Member Access

        // Creating an object to access instance members
        ScopeAndAccessModifierDemo obj = new ScopeAndAccessModifierDemo();

        obj.PublicMethod(); // Accessing public method
        obj.PrivateMethod(); // Accessing private method within the class
        obj.ProtectedMethod(); // Accessing protected method within the class
        obj.InternalMethod(); // Accessing internal method within the same assembly

        #endregion

        #region Static Method Access

        // Accessing static method
        StaticMethod();

        #endregion
    }

    #region Methods

    /// <summary>
    /// Public method: Accessible from outside the class.
    /// </summary>
    public void PublicMethod()
    {
        Console.WriteLine("Public method called.");
        Console.WriteLine("Accessing private variable: " + privateVariable);
    }

    /// <summary>
    /// Private method: Accessible only within the class.
    /// </summary>
    private void PrivateMethod()
    {
        Console.WriteLine("Private method called.");
    }

    /// <summary>
    /// Protected method: Accessible within the class and derived classes.
    /// </summary>
    protected void ProtectedMethod()
    {
        Console.WriteLine("Protected method called.");
    }

    /// <summary>
    /// Internal method: Accessible within the same assembly.
    /// </summary>
    internal void InternalMethod()
    {
        Console.WriteLine("Internal method called.");
    }

    /// <summary>
    /// Static method: Can only access static variables directly.
    /// </summary>
    private static void StaticMethod()
    {
        Console.WriteLine("Static method called.");
        Console.WriteLine("Static variable = " + staticVariable);
    }

    #endregion
}