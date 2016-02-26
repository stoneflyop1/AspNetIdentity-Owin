using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Auth
{
    public class MainEntry
    {
        /// <summary>
        /// 页面登录的认证类型
        /// </summary>
        public static readonly string CookieAuthType = DefaultAuthenticationTypes.ApplicationCookie;

        public static readonly string OAuthType = DefaultAuthenticationTypes.ExternalBearer;

        public static readonly string CookieName = "testauth";
        /// <summary>
        /// WebAPI中Token的认证类型
        /// </summary>
        public static readonly string TokenAuthType = DefaultAuthenticationTypes.ExternalBearer;


        private static ISecureDataFormat<AuthenticationTicket> TokenTicketFormat;

        public static CookieAuthenticationOptions CookieOptions { get; private set; }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static void ConfigCookieAuth(IAppBuilder app, string loginPath
            , ICookieAuthenticationProvider cookieProvider = null)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information 
            // about a user logging in with a third party login provider
            // Configure the sign in cookie
            IDataProtector cookieDataProtector = app.CreateDataProtector(
                    typeof(CookieAuthenticationMiddleware).FullName,
                    CookieAuthType, "v1");
            CookieOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthType,
                CookieName = CookieName,
                LoginPath = new PathString(loginPath),
                TicketDataFormat = new TicketDataFormat(cookieDataProtector)
            };

            if (cookieProvider != null)
            {
                CookieOptions.Provider = cookieProvider;
            }

            app.UseCookieAuthentication(CookieOptions);
        }

        private static ISecureDataFormat<AuthenticationTicket> GetTokenTicketFormat(IAppBuilder app)
        {
            if (TokenTicketFormat == null)
            {
                var tokenDataProtector = app.CreateDataProtector(
                    typeof(OAuthBearerAuthenticationMiddleware).Namespace,
                    "Access_Token", "v1");
                TokenTicketFormat = new TicketDataFormat(tokenDataProtector);
            }
            return TokenTicketFormat;
        }
        /// <summary>
        /// OAuth Token Producer
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureOAuthServer(IAppBuilder app, IOAuthAuthorizationServerProvider provider)
        {
            

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                AuthenticationType = OAuthType,
                AccessTokenFormat = GetTokenTicketFormat(app),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
            if (provider != null)
            {
                OAuthOptions.Provider = provider;
            }

            app.UseOAuthBearerTokens(OAuthOptions);
        }
        /// <summary>
        /// OAuth Token Consumer
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureOAuthClient(IAppBuilder app)
        {
            app.UseOAuthBearerAuthentication(
                new OAuthBearerAuthenticationOptions{ AccessTokenFormat = GetTokenTicketFormat(app) });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="loginPath">/User/Login</param>
        /// <param name="cookieProvider"></param>
        public static void ConfigureAuth(IAppBuilder app, string loginPath
            , IOAuthAuthorizationServerProvider provider)
        {

            ConfigCookieAuth(app, loginPath, null);

            ConfigureOAuthServer(app, provider);

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information
            // when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification 
            // during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            
        }

        public static AuthenticationTicket GetTicketFromToken(string token)
        {
            if (OAuthOptions == null) return null;
            try
            {
                return OAuthOptions.AccessTokenFormat.Unprotect(token);
            }
            catch { }
            return null;
        }

        public static AuthenticationTicket GetTicketFromCookie(string cookie)
        {
            if (CookieOptions == null) return null;
            try
            {
                return CookieOptions.TicketDataFormat.Unprotect(cookie);
            }
            catch { }
            return null;
        }
    }
}
