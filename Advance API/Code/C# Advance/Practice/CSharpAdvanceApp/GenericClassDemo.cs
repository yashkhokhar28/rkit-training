namespace CSharpAdvanceApp
{
    /// <summary>
    /// Demonstrates the usage of generic classes, methods, and interfaces.
    /// </summary>
    public class GenericClassDemo
    {
        /// <summary>
        /// Executes various examples of generics including generic classes, methods, and variance.
        /// </summary>
        public void RunGenericClassDemo()
        {
            // Generic class example
            GenericBox<int> objGenericBox1 = new GenericBox<int>();
            objGenericBox1.Value = 1;
            Console.WriteLine(objGenericBox1.Value);

            GenericBox<string> objGenericBox2 = new GenericBox<string>();
            objGenericBox2.Value = "Yash";
            Console.WriteLine(objGenericBox2.Value);

            // Generic method example
            SwapNumber objSwapNumber = new SwapNumber();
            int num1 = 10;
            int num2 = 20;
            Console.WriteLine($"Before Swap: num1 = {num1}, num2 = {num2}");
            objSwapNumber.Swap(ref num1, ref num2);
            Console.WriteLine($"After Swap: num1 = {num1}, num2 = {num2}");

            double num3 = 10.0;
            double num4 = 20.0;
            Console.WriteLine($"Before Swap: num3 = {num3}, num4 = {num4}");
            objSwapNumber.Swap(ref num3, ref num4);
            Console.WriteLine($"After Swap: num3 = {num3}, num4 = {num4}");

            // Generic interface and repository example
            IRepository<string> objMemoryRepository = new MemoryRepository<string>();
            objMemoryRepository.Add("First Item");
            objMemoryRepository.Add("Second Item");

            Console.WriteLine("Item with ID 1: " + objMemoryRepository.GetById(1));
            Console.WriteLine("Item with ID 2: " + objMemoryRepository.GetById(2));

            // Covariance example
            ICovariant<string> stringSource = new CovariantExample<string>("Hello, Covariance!");
            ICovariant<object> objectSource = stringSource; // Covariance allows this.
            Console.WriteLine(objectSource.GetItem()); // Output: Hello, Covariance!

            // Contravariance example
            IContravariant<object> objectHandler = new ContravariantExample<object>();
            IContravariant<string> stringHandler = objectHandler; // Contravariance allows this.
            stringHandler.SetItem("Hello, Contravariance!"); // Output: Item: Hello, Contravariance!
        }
    }

    /// <summary>
    /// A generic class that holds a value of any type.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class GenericBox<T>
    {
        /// <summary>
        /// Gets or sets the value of the generic type.
        /// </summary>
        public T Value { get; set; }
    }

    /// <summary>
    /// A class demonstrating a generic method for swapping values.
    /// </summary>
    public class SwapNumber
    {
        /// <summary>
        /// Swaps the values of two variables.
        /// </summary>
        /// <typeparam name="T">The type of the values (must be a value type).</typeparam>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        public void Swap<T>(ref T a, ref T b) where T : struct
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }

    /// <summary>
    /// A generic repository interface for managing items.
    /// </summary>
    /// <typeparam name="T">The type of items managed by the repository.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds an item to the repository.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void Add(T item);

        /// <summary>
        /// Retrieves an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>The item with the specified ID.</returns>
        T GetById(int id);
    }

    /// <summary>
    /// An in-memory implementation of the generic repository interface.
    /// </summary>
    /// <typeparam name="T">The type of items managed by the repository.</typeparam>
    public class MemoryRepository<T> : IRepository<T>
    {
        private readonly Dictionary<int, T> objStorage = new Dictionary<int, T>();

        /// <summary>
        /// Adds an item to the repository and assigns it a unique ID.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            int id = objStorage.Count + 1;
            objStorage[id] = item;
            Console.WriteLine($"Item added with ID {id}");
        }

        /// <summary>
        /// Retrieves an item by its ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>The item with the specified ID.</returns>
        public T GetById(int id)
        {
            objStorage.TryGetValue(id, out T value);
            return value;
        }
    }

    /// <summary>
    /// A covariant interface for retrieving an item.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public interface ICovariant<out T>
    {
        /// <summary>
        /// Retrieves an item.
        /// </summary>
        /// <returns>The item.</returns>
        T GetItem();
    }

    /// <summary>
    /// An implementation of the covariant interface.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public class CovariantExample<T> : ICovariant<T>
    {
        private readonly T item;

        /// <summary>
        /// Initializes a new instance with the specified item.
        /// </summary>
        /// <param name="item">The item to store.</param>
        public CovariantExample(T item)
        {
            this.item = item;
        }

        /// <summary>
        /// Retrieves the item.
        /// </summary>
        /// <returns>The stored item.</returns>
        public T GetItem()
        {
            return item;
        }
    }

    /// <summary>
    /// A contravariant interface for setting an item.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public interface IContravariant<in T>
    {
        /// <summary>
        /// Sets an item.
        /// </summary>
        /// <param name="item">The item to set.</param>
        void SetItem(T item);
    }

    /// <summary>
    /// An implementation of the contravariant interface.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public class ContravariantExample<T> : IContravariant<T>
    {
        /// <summary>
        /// Sets and displays the item.
        /// </summary>
        /// <param name="item">The item to set.</param>
        public void SetItem(T item)
        {
            Console.WriteLine($"Item: {item}");
        }
    }
}