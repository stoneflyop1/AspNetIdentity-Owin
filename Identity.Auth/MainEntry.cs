using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
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

        public static readonly string CookieName = "testauth";
        /// <summary>
        /// WebAPI中Token的认证类型
        /// </summary>
        public static readonly string TokenAuthType = DefaultAuthenticationTypes.ExternalBearer;

        public static CookieAuthenticationOptions CookieOptions { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="loginPath">/User/Login</param>
        /// <param name="cookieProvider"></param>
        public static void ConfigureAuth(IAppBuilder app, string loginPath
            , ICookieAuthenticationProvider cookieProvider=null)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information 
            // about a user logging in with a third party login provider
            // Configure the sign in cookie
            IDataProtector dataProtector = app.CreateDataProtector(
                    typeof(CookieAuthenticationMiddleware).FullName,
                    CookieAuthType, "v1");
            CookieOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthType,
                CookieName = CookieName,
                LoginPath = new PathString(loginPath),
                TicketDataFormat = new TicketDataFormat(dataProtector)
            };

            if (cookieProvider != null)
            {
                CookieOptions.Provider = cookieProvider;
            }

            app.UseCookieAuthentication(CookieOptions);

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
