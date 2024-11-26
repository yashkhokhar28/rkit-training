using Newtonsoft.Json; // Example of using a NuGet package reference

namespace CSharpBasicsApp;

/// <summary>
/// Demonstrates the use of assemblies and references in C#.
/// </summary>
public class AssembliesReferencesDemo
{
    /// <summary>
    /// Runs the demo that showcases the use of external assemblies and references.
    /// </summary>
    public static async Task RunAssembliesReferencesDemo()
    {
        Console.WriteLine("Assemblies and References Demo");

        #region HTTP Request Using System.Net.Http

        // Create an instance of HttpClient to make an HTTP request
        HttpClient client = new HttpClient();

        // URL to make the GET request
        string url = "https://jsonplaceholder.typicode.com/posts/1";

        try
        {
            // Asynchronously send a GET request to the specified URL
            string response = await client.GetStringAsync(url);
            Console.WriteLine("Response from HTTP request: " + response);

            #region JSON Parsing Using Newtonsoft.Json

            // Deserialize the JSON response into an object using Newtonsoft.Json
            var jsonObject = JsonConvert.DeserializeObject(response);
            Console.WriteLine("Deserialized JSON object: " + jsonObject);

            #endregion
        }
        catch (Exception ex)
        {
            // Catch and display any exceptions that occur during the HTTP request or JSON parsing
            Console.WriteLine("Error: " + ex.Message);
        }

        #endregion
    }
}