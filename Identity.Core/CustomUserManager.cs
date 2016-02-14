using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core
{
    public class CustomUserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public CustomUserStore(CustomDbContext context) : base(context) { }
    }

    public class CustomUserManager : UserManager<User, int>
    {
        public CustomUserManager(IUserStore<User, int> userStore) : base(userStore)
        {
        }
    }
}
