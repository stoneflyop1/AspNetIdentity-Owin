using Identity.Auth;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Identity.Web
{
    public static class WebApi
    {
        public static void Config()
        {
            GlobalConfiguration.Configure(Register);
        }

        private static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            // 将 Web API 配置为仅使用不记名令牌身份验证。need ref webapi.owin
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(MainEntry.TokenAuthType)); //OAuthDefaults.AuthenticationType

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}