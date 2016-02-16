using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Identity.Core
{
    /// <summary>
    /// IUser&lt;int&gt;
    /// </summary>
    public class User : IUser<int>//IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        ///     Is lockout enabled for this user
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        ///     Is two factor enabled for the user
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        private List<UserRole> _roles;

        public virtual ICollection<UserRole> Roles
        {
            get { return _roles ?? (_roles = new List<UserRole>()); }
        }

        private List<UserClaim> _claims;

        public virtual ICollection<UserClaim> Claims
        {
            get { return _claims ?? (_claims = new List<UserClaim>()); }
        }

        private List<UserLogin> _logins;

        public virtual ICollection<UserLogin> Logins
        {
            get { return _logins ?? (_logins = new List<UserLogin>()); }
        }
    }
}
