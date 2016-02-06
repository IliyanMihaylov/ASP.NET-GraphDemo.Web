using GraphDemo.Web.Controllers.Neo4j;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GraphDemo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Properties.Settings.Default.LogFilePath));
            Trace.AutoFlush = true;

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Neo4jConfiguration();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
