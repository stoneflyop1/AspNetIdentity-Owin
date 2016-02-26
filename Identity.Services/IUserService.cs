using Identity.Auth;
using Identity.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IUserService : IDisposable
    {
        Task<IdentityResult> RegisterAsync(User user, string password);

        Task<SignInStatus> PasswordSignInAsync(string email, string password, 
            bool rememberMe=false, bool shouldLockout=false);

        Task SignInAsync(User user, bool isPersistent = false, bool rememberBrowser = false);

        Task<User> GetByIdAsync(int userId);

        User GetById(int userId);

        void SignOut(string authType = null);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User, int> _userManager;
        private readonly SignInManager<User, int> _signinManager;
        private readonly IAuthenticationManager _authManager;

        public UserService(IOwinContext context)
        {
            _userManager = context.GetUserManager<UserManager<User,int>>();
            _signinManager = context.Get<SignInManager<User, int>>();
            _authManager = context.Authentication;
        }

        public UserService(UserManager<User, int> userManager, SignInManager<User, int> signinManager
            , IAuthenticationManager authManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _authManager = authManager;
        }

        public Task<SignInStatus> PasswordSignInAsync(string email, string password, 
            bool rememberMe=false, bool shouldLockout=false)
        {
            return _signinManager.PasswordSignInAsync(
                email, password, rememberMe, shouldLockout: shouldLockout);
        }

        public Task<IdentityResult> RegisterAsync(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<User> GetByIdAsync(int userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public User GetById(int userId)
        {
            return _userManager.FindById(userId);
        }

        public Task SignInAsync(User user, bool isPersistent = false, bool rememberBrowser = false)
        {
            return _signinManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }

        public void SignOut(string authType = null)
        {
            if (authType == null)
            {
                authType = MainEntry.CookieAuthType;
            }
            _authManager.SignOut(authType);
        }

        public void Dispose()
        {
            if (_userManager != null)
            {
                _userManager.Dispose();
            }

            if (_signinManager != null)
            {
                _signinManager.Dispose();
            }
        }
    }
}
