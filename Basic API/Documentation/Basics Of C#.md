# C# Beginner Guide

## Introduction to C#

C# is a modern, object-oriented programming language developed by Microsoft. It's used to build a variety of applications, from web and desktop to mobile and cloud-based apps. C# is part of the .NET framework, which provides a comprehensive environment for application development.

## Create Your First C# Program: 'Hello World'

To create a simple "Hello World" program in C#, follow these steps:

1. **Install Visual Studio** - Download and install Visual Studio for a full-featured C# IDE.
2. **Create a New Project** - Open Visual Studio, click `Create a new project`, select `Console App`, and name it `HelloWorld`.
3. **Write the Code**:

```csharp
using System;

namespace HelloWorld
{
   class Program
   {
      static void Main(string[] args)
      {
        Console.WriteLine("Hello World!");
      }
   }
}
```

4. **Run the Program** - Click on the `Run` button or press `Ctrl + F5` to see the output.

## Understanding C# Program Structure

- **Namespace**: Used to organize code and prevent naming conflicts.
- **Class**: A blueprint for creating objects.
- **Main Method**: The entry point of a C# program.
- **Statements**: Lines of code that perform actions.

Example:

```csharp
using System; // Namespace

namespace MyApp // Namespace declaration
{
   class Program // Class declaration
   {
      static void Main(string[] args) // Main method
      {
        Console.WriteLine("Hello World!"); // Statement
      }
   }
}
```

## Working with Code Files, Projects & Solutions

- **Code File**: A file containing C# code, typically with a `.cs` extension.
- **Project**: A container that holds code files, resources, and references.
- **Solution**: A collection of related projects.

## Datatypes & Variables with Conversion

- **Value Types**: `int`, `float`, `char`, `bool`
- **Reference Types**: `string`, `object`, arrays, and classes
- **Type Conversion**: Implicit and explicit conversions

  ```csharp
  int number = 10;
  double converted = number; // Implicit conversion

  double value = 9.8;
  int intValue = (int)value; // Explicit conversion
  ```

## Operators & Expressions

- **Arithmetic Operators**: `+`, `-`, `*`, `/`, `%`
- **Relational Operators**: `==`, `!=`, `>`, `<`, `>=`, `<=`
- **Logical Operators**: `&&`, `||`, `!`
- **Expressions**: Combine variables and operators to perform calculations.

Example:

```csharp
int a = 5, b = 10;
int sum = a + b;
bool result = (a < b) && (b > 0);
```

## Statements

- **Conditional Statements**: `if`, `else`, `switch`
- **Looping Statements**: `for`, `while`, `do-while`, `foreach`
- **Jump Statements**: `break`, `continue`, `return`

Example:

```csharp
if (a > b)
{
   Console.WriteLine("a is greater than b");
}
else
{
   Console.WriteLine("b is greater than or equal to a");
}
```

## Understanding Arrays

- **Single-Dimensional Array**: `int[] numbers = {1, 2, 3, 4, 5};`
- **Multi-Dimensional Array**: `int[,] matrix = {{1, 2}, {3, 4}};`
- **Jagged Array**: `int[][] jagged = new int[2][];`

Example:

```csharp
int[] numbers = {1, 2, 3};
Console.WriteLine(numbers[0]); // Output: 1
```

## Define & Calling Methods

- **Method Definition**: Code that performs a specific task.
- **Calling a Method**: Execute the method using its name.

Example:

```csharp
void SayHello()
{
   Console.WriteLine("Hello!");
}

SayHello(); // Calling the method
```

## Understanding Classes & OOP Concepts

- **Classes**: Templates for objects. Example: `class Car { }`
- **Objects**: Instances of classes.
- **OOP Principles**:
  - **Encapsulation**: Wrapping data and methods.
  - **Inheritance**: Deriving new classes from existing ones.
  - **Polymorphism**: Methods with the same name but different implementations.
  - **Abstraction**: Hiding implementation details.

Example:

```csharp
class Car
{
   public string Brand { get; set; }
   public void Drive() { Console.WriteLine("Driving..."); }
}
```

## Interface & Inheritance

- **Interface**: Defines a contract. Example: `interface IVehicle { void Drive(); }`
- **Inheritance**: Allows a class to inherit from another class.

Example:

```csharp
class Vehicle { }
class Car : Vehicle { } // Inheritance
```

## Scope & Accessibility Modifier

- **Access Modifiers**: `public`, `private`, `protected`, `internal`
- **Scope**: Defines the visibility of variables.

Example:

```csharp
public class MyClass
{
   private int number; // Accessible only within this class
}
```

## Namespace & .Net Library

- **Namespace**: Grouping classes logically.
- **.NET Library**: A collection of pre-built classes and methods.

Example:

```csharp
using System.IO; // Namespace for file operations
```

## Creating & Adding Reference to Assemblies

- **Assemblies**: Compiled code libraries used in applications.
- **Adding References**: Add references to assemblies for additional functionality.

## Working with Collections

- **List**: `List<int> numbers = new List<int>();`
- **Dictionary**: `Dictionary<string, int> dict = new Dictionary<string, int>();`

## Enumerations

- **Enum**: A set of named constants.
  ```csharp
  enum Days { Sunday, Monday, Tuesday };
  Days today = Days.Monday;
  ```

## Data Table

- **DataTable**: Represents in-memory data.
  ```csharp
  DataTable table = new DataTable();
  table.Columns.Add("Name");
  ```

## Exception Handling

- **Try-Catch Block**: Handle runtime errors gracefully.
  ```csharp
  try
  {
    int result = 10 / 0;
  }
  catch (Exception ex)
  {
    Console.WriteLine(ex.Message);
  }
  ```

## Different Project Types

- **Console Application**
- **Web Application**
- **Windows Forms Application**
- **Class Library**

## Working with String Class

- **String Methods**: `Length`, `Substring`, `Replace`, `Split`
  ```csharp
  string name = "John";
  Console.WriteLine(name.Length); // Output: 4
  ```

## Working with DateTime Class

- **DateTime Properties & Methods**: `Now`, `AddDays`, `ToString`
  ```csharp
  DateTime today = DateTime.Now;
  Console.WriteLine(today.ToString("dd-MM-yyyy"));
  ```

## Basic File Operations

- **File Reading & Writing**:
  ```csharp
  File.WriteAllText("file.txt", "Hello World!");
  string content = File.ReadAllText("file.txt");
  ```

## Introduction to Web Development

- **ASP.NET**: Framework for building web applications.
- **MVC**: Model-View-Controller architecture.

## Web API Project

- **Creating a Web API**: Use ASP.NET Core to build RESTful APIs.
- **Routing & HTTP Methods**: `GET`, `POST`, `PUT`, `DELETE`

## Building Web API

- **Controllers & Actions**: Define endpoints and methods.
  ```csharp
  [ApiController]
  [Route("api/[controller]")]
  public class MyController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get() => Ok("Hello API!");
  }
  ```
