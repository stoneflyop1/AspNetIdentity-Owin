using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomUserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public CustomUserStore(CustomDbContext context) : base(context) { }
    }
}
