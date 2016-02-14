﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core
{
    /// <summary>
    /// DbContext for users
    /// </summary>
    public class CustomDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public CustomDbContext(): base("DefaultConnection")
        {
        }
    }
}
