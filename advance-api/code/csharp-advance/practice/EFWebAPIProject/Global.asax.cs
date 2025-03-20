using ServiceStack.OrmLite;
using System.Configuration;
using System.Web.Http;

namespace EFWebAPIProject
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Database connection using connection string and orm lite tool.
            string connectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            OrmLiteConnectionFactory objOrmLiteConnectionFactory = new OrmLiteConnectionFactory(connectionString, MySql55Dialect.Provider);

            // Storing OrmLiteConnectionFactory instance for further usage in any other component.
            Application["DbFactory"] = objOrmLiteConnectionFactory;
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
