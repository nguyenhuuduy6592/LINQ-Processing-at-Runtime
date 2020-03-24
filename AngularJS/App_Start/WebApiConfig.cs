using AngularJS.Utilities;
using System.Web.Configuration;
using System.Web.Http;

namespace AngularJS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            GlobalConstants.BookstoreDatabaseSettings = new ConfigModels.BookstoreDatabaseSettings
            {
                BooksCollectionName = WebConfigurationManager.AppSettings["BooksCollectionName"],
                ConnectionString = WebConfigurationManager.AppSettings["ConnectionString"],
                DatabaseName = WebConfigurationManager.AppSettings["DatabaseName"],
            };

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
