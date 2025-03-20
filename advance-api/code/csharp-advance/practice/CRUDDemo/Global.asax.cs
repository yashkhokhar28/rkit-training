using System.Configuration;
using System.Web.Http;

namespace CRUDDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Database connection using connection string and orm lite tool.
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            Application["ConnectionString"] = connectionString;
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
