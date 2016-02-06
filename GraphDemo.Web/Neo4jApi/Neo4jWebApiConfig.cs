using GraphDemo.Web.Neo4j.Cache;
using Neo4jClient;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace GraphDemo.Web.Controllers.Neo4j
{
    public static class WebApiConfig
    {
        public static void Neo4jConfiguration()
        {
            try
            {
                GlobalConfiguration.Configure(Register);
            }
            catch(Exception exc)
            {
                Trace.WriteLine(exc.Message);
            }
        }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            //Use an IoC container and register as a Singleton
            string url = Properties.Settings.Default.GraphDBUrl;
            string user = Properties.Settings.Default.GraphDBUser;
            string password = Properties.Settings.Default.GraphDBPassword;

            IGraphClient client = new GraphClient(new Uri(url), user, password);
            client.Connect();

            GraphClient = client;
            Cache = new Neo4jCacheQuery();
        }

        public static IGraphClient GraphClient { get; private set; }

        // User -> Query
        public static ICacheQuery<string, string> Cache { get; private set; }
    }
}
