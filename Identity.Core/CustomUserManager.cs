using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core
{
    /// <summary>
    /// Exposes User APIs
    /// </summary>
    public class CustomUserManager : UserManager<User, int>
    {
        public CustomUserManager(IUserStore<User, int> userStore) : base(userStore)
        {
        }
    }
}
