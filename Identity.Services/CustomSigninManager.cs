using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Identity.Core;
using System.Security.Claims;
using Identity.Auth;

namespace Identity.Services
{
    /// <summary>
    /// Sign In Operations for users
    /// </summary>
    public class CustomSigninManager : SignInManager<User, int>
    {

        public CustomSigninManager(UserManager<User,int> userManager, IAuthenticationManager authManager) :
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
