using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Identity.Web
{
    public class Global : System.Web.HttpApplication
    {

        private void ConfigRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            WebApi.Config();
            ConfigRoutes(RouteTable.Routes);            
        }
        
    }
}