using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Identity.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomUserStore :IUserEmailStore<User,int>, IUserPasswordStore<User,int>, IUserRoleStore<User,int>,
        IUserLoginStore<User,int>, IUserLockoutStore<User,int>, IUserTwoFactorStore<User,int>, IUserStore<User, int>
        //UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        private readonly CustomDbContext _context;

        public CustomUserStore(CustomDbContext context)
        {
            _context = context;
        }

        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(User user)
        {
            _context.Users.Add(user);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }

        public Task<User> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(c => c.Email == email);
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return _context.Users.FirstOrDefaultAsync(c=>c.Id==userId);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return _context.Users.FirstOrDefaultAsync(c => c.UserName == userName);
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<string> GetEmailAsync(User user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            return
                Task.FromResult(user.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            IList<string> roleList = new List<string>();
            var roles = user.Roles;
            foreach(var r in roles)
            {
                roleList.Add(_context.Roles.Find(r.RoleId).Name);
            }
            return Task.FromResult(roleList);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task SetEmailAsync(User user, string email)
        {
            user.Email = email;
            return _context.SaveChangesAsync();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;
            return _context.SaveChangesAsync();
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            user.LockoutEnabled = enabled;
            return _context.SaveChangesAsync();
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDateUtc = lockoutEnd.UtcDateTime;
            return _context.SaveChangesAsync();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return _context.SaveChangesAsync();
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            user.TwoFactorEnabled = enabled;
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            return _context.SaveChangesAsync();
        }
    }
}
