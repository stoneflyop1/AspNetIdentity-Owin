using Identity.Auth;
using Identity.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Identity.Web
{
    public class CustomSigninManager : SignInManager<User, int>
    {        

        public CustomSigninManager(CustomUserManager userManager, IAuthenticationManager authManager):
            base(userManager, authManager)
        {

        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            var userIdentity = UserManager.CreateIdentityAsync(user, MainEntry.CookieAuthType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}