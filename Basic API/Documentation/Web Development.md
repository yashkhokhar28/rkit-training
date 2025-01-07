## Introduction to Web Development in ASP.NET Web API (.NET Framework)

Web development refers to the process of building, designing, and maintaining web applications or websites. In the context of the ASP.NET Web API using the .NET Framework, it focuses on creating **RESTful services** that enable communication between client applications (like web browsers, mobile apps) and servers over HTTP.

---

## ASP.NET Web API Project: Overview

ASP.NET Web API is a framework for building HTTP-based services that can be consumed by a wide range of clients, including browsers, mobile devices, and desktop applications. It is designed for creating RESTful APIs in a flexible and scalable manner.

#### Key Features of ASP.NET Web API

1. **Lightweight Framework**: It is a lightweight architecture suitable for small and large-scale applications.
2. **RESTful Principles**: Designed to work seamlessly with HTTP verbs like GET, POST, PUT, DELETE, etc.
3. **Content Negotiation**: Automatically returns data in formats like JSON, XML, etc., based on client preference.
4. **Dependency Injection**: Supports IoC (Inversion of Control) for better testability and flexibility.
5. **Self-Hosting**: Can run independently without IIS, useful for services running in isolated environments.

---

### Creating a Web API Project in ASP.NET (.NET Framework)

1. **Start a New Project**:

   - Open Visual Studio.
   - Select **File > New > Project**.
   - Choose **ASP.NET Web Application (.NET Framework)**.
   - Name the project and click **OK**.

2. **Select Project Template**:

   - In the **New ASP.NET Project** dialog, choose **Empty**.
   - Enable **Web API** to include required libraries and configurations.

3. **Structure of Web API Project**:

   - **Controllers Folder**: Contains Web API controllers. Each controller handles HTTP requests.
   - **Models Folder**: Contains model classes representing the data structure.
   - **App_Start Folder**: Contains configuration files like `WebApiConfig.cs` for routing.
   - **Web.config**: Configuration file for the application.

4. **Configuring Routes**:

   - Open `WebApiConfig.cs` (in the `App_Start` folder).
   - Define the API routing using the `MapHttpRoute` method:
     ```csharp
     config.Routes.MapHttpRoute(
         name: "DefaultApi",
         routeTemplate: "api/{controller}/{id}",
         defaults: new { id = RouteParameter.Optional }
     );
     ```

5. **Creating a Controller**:

   - Add a new controller by right-clicking the **Controllers** folder and selecting **Add > Controller**.
   - Choose **Web API Controller - Empty** and name it (e.g., `ProductsController`).

6. **Adding Actions**:

   - Implement methods in the controller to handle HTTP requests:

     ```csharp
     public class ProductsController : ApiController
     {
         // GET: api/Products
         public IEnumerable<string> Get()
         {
             return new string[] { "Product1", "Product2" };
         }

         // GET: api/Products/5
         public string Get(int id)
         {
             return "Product" + id;
         }

         // POST: api/Products
         public void Post([FromBody] string value)
         {
         }
     }
     ```

7. **Testing the API**:
   - Run the application and use a tool like **Postman** or **Swagger** to test your API endpoints.

---

### Benefits of Using ASP.NET Web API

- **Cross-Platform Communication**: Exposes endpoints that can be consumed by any client capable of making HTTP requests.
- **Flexibility**: Can be used in web, mobile, IoT, or desktop applications.
- **Extensibility**: Supports custom routing, authentication mechanisms (e.g., JWT), and more.

---

## Building a Web API

### Step 1: Create a New Web API Project

1. Open Visual Studio and create a new **ASP.NET Web Application (.NET Framework)**.
2. Choose the **Empty** template and check the **Web API** checkbox to include Web API dependencies.
3. Click **OK** to create the project.

---

### Step 2: Project Structure

The default structure includes:

- **Controllers Folder**: Contains Web API controllers where you define your endpoints.
- **App_Start Folder**: Contains `WebApiConfig.cs`, which configures routing and other settings.
- **Global.asax**: Application entry point where the Web API is initialized.

---

### Step 3: Configure Web API Routing

In `App_Start/WebApiConfig.cs`, ensure the default routing is set up:

```csharp
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Web API routes
        config.MapHttpAttributeRoutes();

        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );

        // Enable JSON Formatter
        config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    }
}
```

---

### Step 4: Create a Controller

Create a controller named `ProductsController`:

1. Right-click the **Controllers** folder > **Add** > **Controller**.
2. Select **Web API Controller - Empty** and name it `ProductsController`.

---

### Step 5: Implement CRUD Operations

```csharp
using System.Collections.Generic;
using System.Web.Http;

namespace EmptyProject.Controllers
{
    public class ProductsController : ApiController
    {
        // Static list to simulate a database
        private static List<string> products = new List<string> { "Laptop", "Phone", "Tablet" };

        // GET: api/Products
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(products); // Returns 200 OK with the list of products
        }

        // GET: api/Products/1
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id < 0 || id >= products.Count)
                return NotFound(); // Returns 404 Not Found if the product doesn't exist

            return Ok(products[id]); // Returns 200 OK with the specific product
        }

        // POST: api/Products
        [HttpPost]
        public IHttpActionResult Post([FromBody] string product)
        {
            if (string.IsNullOrEmpty(product))
                return BadRequest("Product cannot be null or empty."); // Returns 400 Bad Request

            products.Add(product);
            return Created($"api/Products/{products.Count - 1}", product); // Returns 201 Created
        }

        // PUT: api/Products/1
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] string product)
        {
            if (id < 0 || id >= products.Count)
                return NotFound(); // Returns 404 Not Found

            if (string.IsNullOrEmpty(product))
                return BadRequest("Product cannot be null or empty."); // Returns 400 Bad Request

            products[id] = product;
            return Ok(product); // Returns 200 OK with the updated product
        }

        // DELETE: api/Products/1
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id < 0 || id >= products.Count)
                return NotFound(); // Returns 404 Not Found

            products.RemoveAt(id);
            return StatusCode(System.Net.HttpStatusCode.NoContent); // Returns 204 No Content
        }
    }
}
```

---

## Action Method Responses

Action methods in Web API return HTTP responses to the client. These responses typically include:

- **Status codes**: Indicating the result of the operation (e.g., 200, 404, 500).
- **Data payload**: JSON or XML, depending on the client's request.

---

### Key HTTP Status Codes

- **200 OK**: Request was successful.
- **201 Created**: A new resource was created.
- **204 No Content**: Request was successful, but no content is returned.
- **400 Bad Request**: The client request was invalid.
- **404 Not Found**: The requested resource could not be found.
- **500 Internal Server Error**: A server-side error occurred.

---

### Example: Action Method Responses

Here’s how to use different responses in the `ProductsController`:

1. **Returning `200 OK`**:

   ```csharp
   [HttpGet]
   public IHttpActionResult GetAllProducts()
   {
       return Ok(products); // Returns 200 OK with the list of products
   }
   ```

2. **Returning `404 Not Found`**:

   ```csharp
   [HttpGet]
   public IHttpActionResult GetProduct(int id)
   {
       if (id < 0 || id >= products.Count)
           return NotFound(); // Returns 404 Not Found

       return Ok(products[id]);
   }
   ```

3. **Returning `400 Bad Request`**:

   ```csharp
   [HttpPost]
   public IHttpActionResult AddProduct([FromBody] string product)
   {
       if (string.IsNullOrEmpty(product))
           return BadRequest("Product cannot be null or empty."); // Returns 400 Bad Request

       products.Add(product);
       return Created($"api/Products/{products.Count - 1}", product);
   }
   ```

4. **Returning `201 Created`**:

   ```csharp
   [HttpPost]
   public IHttpActionResult CreateProduct([FromBody] string product)
   {
       products.Add(product);
       return Created($"api/Products/{products.Count - 1}", product); // Returns 201 Created
   }
   ```

5. **Returning `204 No Content`**:

   ```csharp
   [HttpDelete]
   public IHttpActionResult RemoveProduct(int id)
   {
       if (id < 0 || id >= products.Count)
           return NotFound();

       products.RemoveAt(id);
       return StatusCode(System.Net.HttpStatusCode.NoContent); // Returns 204 No Content
   }
   ```

---

### Testing the API

- **Using Postman or Browser**:
  - Test endpoints like `GET api/Products` or `POST api/Products`.
- **Using Fiddler** or similar tools to see the response headers and status codes.

---

## Security in ASP.NET Web API

Security is a critical aspect of any web application to protect it from unauthorized access, data breaches, and malicious attacks. ASP.NET Web API provides several mechanisms to implement security, including CORS, Authentication, Authorization, Exception Handling, and JWT Tokens.

### CORS (Cross-Origin Resource Sharing)

CORS allows web applications running on different domains to interact with your API. By default, web browsers restrict cross-origin requests for security purposes. To enable CORS in ASP.NET Web API:

- Install the CORS package using NuGet.
- Enable CORS globally or for specific controllers using `EnableCorsAttribute`.

Example for enabling CORS globally in `WebApiConfig.cs`:

```csharp
using System.Web.Http;
using System.Web.Http.Cors;

public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        var cors = new EnableCorsAttribute("*", "*", "*");
        config.EnableCors(cors);
        config.MapHttpAttributeRoutes();
    }
}
```

### Authentication and Authorization

- **Authentication** verifies the identity of users.
- **Authorization** determines the access level or permissions for authenticated users.

ASP.NET Web API supports various authentication methods:

- Basic Authentication
- Token-Based Authentication (using JWT tokens)
- OAuth and OpenID Connect

Example: Securing an API endpoint using `AuthorizeAttribute`:

```csharp
[Authorize]
public IHttpActionResult GetSecureData()
{
    return Ok("This is a secure endpoint.");
}
```

### Exception Handling

Exception handling is essential to ensure your API gracefully handles errors. In ASP.NET Web API, you can use Exception Filters and Global Exception Handling to manage exceptions.

Example of a custom exception filter:

```csharp
public class CustomExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(HttpActionExecutedContext context)
    {
        // Log exception and return a custom response
        context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent("An error occurred. Please try again later."),
            ReasonPhrase = "Critical Exception"
        };
    }
}
```

### JWT Token Authentication

JWT (JSON Web Token) is a popular method for securing APIs. It provides a compact and self-contained way to transmit information securely.

Steps to implement JWT in ASP.NET Web API:

1. Install `System.IdentityModel.Tokens.Jwt` package.
2. Generate a JWT token on user authentication.
3. Validate the token on every request to protected endpoints.

---

## HTTP Caching

Caching improves performance by storing copies of responses and serving them without contacting the server repeatedly. ASP.NET Web API supports output caching and client-side caching.

### Types of Caching

1. **Server-Side Caching**: Store responses on the server.
2. **Client-Side Caching**: Use cache-related headers like `Cache-Control`.

Example of setting cache headers in an API response:

```csharp
[HttpGet]
[OutputCache(Duration = 60)]
public IHttpActionResult GetData()
{
    return Ok("This response is cached for 60 seconds.");
}
```

---

## API Versioning

Versioning ensures backward compatibility and smooth upgrades. There are several ways to implement versioning in ASP.NET Web API:

1. **URL Versioning** (e.g., `api/v1/products`)
2. **Query String Versioning** (e.g., `api/products?version=1`)
3. **Header Versioning** (e.g., `X-Version: 1`)

Example using URL versioning:

```csharp
config.Routes.MapHttpRoute(
    name: "VersionedApi",
    routeTemplate: "api/v{version}/{controller}/{id}",
    defaults: new { id = RouteParameter.Optional }
);
```

---

## Using Swagger for API Documentation

Swagger is an open-source tool that provides an interactive interface to explore and test your APIs. It automatically generates API documentation and helps developers understand and test the endpoints.

Steps to Use Swagger in ASP.NET Web API:

1. Install `Swashbuckle` package using NuGet.
2. Enable Swagger in your API project by modifying `WebApiConfig.cs`.

Example:

```csharp
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        // Enable Swagger
        config.EnableSwagger(c =>
        {
            c.SingleApiVersion("v1", "My API");
        }).EnableSwaggerUi();
    }
}
```

---

## Using POSTMAN for API Testing

Postman is a popular tool for testing APIs. It allows you to send requests to your API endpoints and analyze responses without needing a client application.

### How to Test an API with POSTMAN

1. Open Postman and create a new request.
2. Enter the API endpoint URL (e.g., `https://localhost:5001/api/products`).
3. Select the HTTP method (GET, POST, etc.).
4. Add headers and body data if required.
5. Click Send to see the response.

---

## Deployment of ASP.NET Web API

Deploying an ASP.NET Web API application involves publishing the project and hosting it on a server. Common deployment options include IIS, Azure, and Docker.

### Steps to Deploy on IIS

1. **Publish the Project**:

   - In Visual Studio, right-click the project and select Publish.
   - Choose File System and set a target folder.

2. **Set Up IIS**:

   - Open IIS Manager.
   - Add a new Site pointing to the published folder.
   - Set the application pool to use .NET Framework.

3. **Configure Firewall and Ports**:
   - Ensure the server firewall allows traffic on the specified port (e.g., 80 for HTTP).

---
