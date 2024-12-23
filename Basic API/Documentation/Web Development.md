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

Here’s a detailed explanation and example for both topics: **Building a Web API** and **Action Method Responses** in ASP.NET Web API using the .NET Framework.

---

## Building a Web API

### **Step 1: Create a New Web API Project**

1. Open Visual Studio and create a new **ASP.NET Web Application (.NET Framework)**.
2. Choose the **Empty** template and check the **Web API** checkbox to include Web API dependencies.
3. Click **OK** to create the project.

---

### **Step 2: Project Structure**

The default structure includes:

- **Controllers Folder**: Contains Web API controllers where you define your endpoints.
- **App_Start Folder**: Contains `WebApiConfig.cs`, which configures routing and other settings.
- **Global.asax**: Application entry point where the Web API is initialized.

---

### **Step 3: Configure Web API Routing**

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

### **Step 4: Create a Controller**

Create a controller named `ProductsController`:

1. Right-click the **Controllers** folder > **Add** > **Controller**.
2. Select **Web API Controller - Empty** and name it `ProductsController`.

---

### **Step 5: Implement CRUD Operations**

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

## **2. Action Method Responses**

Action methods in Web API return HTTP responses to the client. These responses typically include:

- **Status codes**: Indicating the result of the operation (e.g., 200, 404, 500).
- **Data payload**: JSON or XML, depending on the client's request.

---

### **Key HTTP Status Codes**

- **200 OK**: Request was successful.
- **201 Created**: A new resource was created.
- **204 No Content**: Request was successful, but no content is returned.
- **400 Bad Request**: The client request was invalid.
- **404 Not Found**: The requested resource could not be found.
- **500 Internal Server Error**: A server-side error occurred.

---

### **Example: Action Method Responses**

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

### **Testing the API**

- **Using Postman or Browser**:
  - Test endpoints like `GET api/Products` or `POST api/Products`.
- **Using Fiddler** or similar tools to see the response headers and status codes.

---

### **CORS (Cross-Origin Resource Sharing)**

CORS is a mechanism that allows restricted resources (e.g., API data) on a web page to be requested from another domain outside the domain from which the resource originated. It is a security feature implemented in web browsers to prevent malicious scripts from accessing sensitive data on a different domain.

### **Key Points of CORS:**

1. **Origin**: The domain, scheme, and port of a request (e.g., `https://example.com`).
2. **Simple Requests**: Requests that use standard headers like `GET` or `POST`.
3. **Preflight Requests**: Requests that browsers send to check if the server allows the actual request. These are sent before methods like `PUT`, `DELETE`, or non-standard headers are used.

---

### **CORS in ASP.NET Framework Web API**

In **ASP.NET Framework Web API**, enabling CORS involves:

1. Installing the necessary package.
2. Configuring CORS in the Web API pipeline.

---

### **Step-by-Step Implementation**

#### **1. Install the Required Package**

Use the NuGet Package Manager to install the CORS package:

```bash
Install-Package Microsoft.AspNet.WebApi.Cors
```

---

#### **2. Enable CORS Globally**

In your `WebApiConfig.cs`, enable CORS for the entire API:

```csharp
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPIDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable CORS globally
            var cors = new EnableCorsAttribute("*", "*", "*"); // Allow all origins, headers, and methods
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
```

---

#### **3. Enable CORS for Specific Controllers or Actions**

Instead of enabling CORS globally, you can apply it to specific controllers or action methods.

##### Example: Apply CORS to a Controller

```csharp
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPIDemo.Controllers
{
    [EnableCors(origins: "https://example.com", headers: "*", methods: "GET,POST")]
    public class ProductsController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Product1", "Product2" };
        }
    }
}
```

##### Example: Apply CORS to an Action Method

```csharp
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPIDemo.Controllers
{
    public class ProductsController : ApiController
    {
        [EnableCors(origins: "https://example.com", headers: "*", methods: "GET")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Product1", "Product2" };
        }
    }
}
```

---

### **Live Example**

#### **Scenario: Allow Specific Origins**

Assume your API at `https://api.example.com` is being consumed by a client hosted at `https://client.example.com`. Only this client should be allowed to access your API.

#### **Controller Code:**

```csharp
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPIDemo.Controllers
{
    [EnableCors(origins: "https://client.example.com", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Product1", "Product2" };
        }

        public string Get(int id)
        {
            return $"Product {id}";
        }
    }
}
```

#### **Client-Side Code (JavaScript):**

```javascript
fetch("https://api.example.com/api/products")
  .then((response) => response.json())
  .then((data) => console.log(data))
  .catch((error) => console.error("Error:", error));
```

If the origin `https://client.example.com` is not allowed, the browser will block the request, and you will see a CORS error in the console.

---

### **Common CORS Configurations**

#### **Allow All Origins, Headers, and Methods (Not Recommended)**

```csharp
var cors = new EnableCorsAttribute("*", "*", "*");
config.EnableCors(cors);
```

#### **Restrict to Specific Origins**

```csharp
var cors = new EnableCorsAttribute("https://example.com, https://another.com", "*", "*");
config.EnableCors(cors);
```

#### **Allow Specific HTTP Methods**

```csharp
var cors = new EnableCorsAttribute("https://example.com", "*", "GET,POST");
config.EnableCors(cors);
```

#### **Allow Specific Headers**

```csharp
var cors = new EnableCorsAttribute("https://example.com", "Content-Type,Authorization", "*");
config.EnableCors(cors);
```

---

### **Testing CORS**

1. **With a Browser**: Open the developer console and check the network request for CORS errors.
2. **Using cURL**: Send a request with custom headers to test if preflight requests succeed.
3. **Postman**: Postman bypasses CORS restrictions, so it won't raise CORS-related issues.

---

### **Key Considerations**

1. **Security**: Allowing all origins (`*`) is insecure and should only be used for testing.
2. **Preflight Requests**: Ensure your server handles `OPTIONS` requests when a preflight request is sent.
3. **Authorization**: CORS should work seamlessly with authentication mechanisms like JWT or cookies.

---

Here’s how to implement **Basic Authentication** using the `AuthorizationFilterAttribute` in **ASP.NET Web API**. This method provides a more flexible way to apply custom authentication and authorization logic directly via filters.

---

### **Basic Authentication in ASP.NET Web API using AuthorizationFilter Attribute**

---

### **How Basic Authentication Works**

1. **Client Request**:

   - The client sends a request with an `Authorization` header in the following format:
     ```
     Authorization: Basic Base64(username:password)
     ```

2. **Server Validation**:

   - The server decodes the Base64-encoded string to retrieve the `username` and `password`.
   - It validates the credentials, often against a user database or predefined values.

3. **Response**:
   - If credentials are valid:
     - The request proceeds, and the server returns the appropriate response.
   - If credentials are invalid:
     - The server returns a `401 Unauthorized` status code.

---

### **Security Considerations**

- Always use HTTPS to prevent credentials from being intercepted during transmission.
- Avoid storing plaintext passwords. Store passwords in a hashed and salted form for security.

---

### **Live Example: Basic Authentication in ASP.NET Web API using AuthorizationFilterAttribute**

---

#### **Step 1: Create a Custom Authorization Filter**

To extend the `AuthorizationFilterAttribute`, create a custom filter class that will handle the Basic Authentication logic.

```csharp
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
{
    public override void OnAuthorization(HttpActionContext actionContext)
    {
        var headers = actionContext.Request.Headers;

        if (headers.Authorization == null || headers.Authorization.Scheme != "Basic")
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"localhost\"");
            return;
        }

        var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(headers.Authorization.Parameter));
        var parts = credentials.Split(':');

        var username = parts[0];
        var password = parts[1];

        if (ValidateUser(username, password))
        {
            var identity = new GenericIdentity(username);
            var principal = new GenericPrincipal(identity, null);

            Thread.CurrentPrincipal = principal;
            if (System.Web.HttpContext.Current != null)
            {
                System.Web.HttpContext.Current.User = principal;
            }
        }
        else
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }

    private bool ValidateUser(string username, string password)
    {
        // Example: Hardcoded credentials (replace with database or other validation)
        return username == "admin" && password == "password123";
    }
}
```

---

#### **Step 2: Register the Filter in Global Configuration**

Once the custom filter is created, register it in the `WebApiConfig` file so that it applies globally or to specific controllers/actions.

```csharp
using System.Web.Http;

namespace BasicAuthDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable Basic Authentication globally by adding the custom filter
            config.Filters.Add(new BasicAuthenticationAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
```

---

#### **Step 3: Create a Sample Controller**

Create a controller to test authentication. The `[Authorize]` attribute can also be used to ensure only authenticated users can access the actions.

```csharp
using System.Web.Http;

namespace BasicAuthDemo.Controllers
{
    [Authorize] // This ensures the user is authenticated before accessing any action
    public class ProductsController : ApiController
    {
        // GET api/products
        public IHttpActionResult Get()
        {
            return Ok(new string[] { "Product1", "Product2", "Product3" });
        }

        // GET api/products/5
        public IHttpActionResult Get(int id)
        {
            return Ok($"Product {id}");
        }
    }
}
```

---

#### **Step 4: Testing Basic Authentication**

1. **Postman**:

   - Use **Postman** to test the API.
   - Go to the **Authorization** tab.
   - Choose **Basic Auth**.
   - Enter the `username` and `password` (`admin` and `password123` in this example).

2. **curl**:

   - Use `curl` to test the API from the command line:
     ```bash
     curl -u admin:password123 https://localhost:44388/api/products
     ```

3. **If Credentials are Correct**:

   - The server will return a response like:
     ```json
     ["Product1", "Product2", "Product3"]
     ```

4. **If Credentials are Incorrect**:
   - The server will return a `401 Unauthorized` response with a message like:
     ```json
     {
       "Message": "Authorization has been denied for this request."
     }
     ```

---
