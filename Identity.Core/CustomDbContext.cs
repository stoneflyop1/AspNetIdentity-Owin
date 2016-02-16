using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core
{
    /// <summary>
    /// DbContext for users, see https://aspnetidentity.codeplex.com/
    /// </summary>
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(): base("DefaultConnection")
        {
        }

        /// <summary>
        ///     IDbSet of Users
        /// </summary>
        public virtual IDbSet<User> Users { get; set; }

        /// <summary>
        ///     IDbSet of Roles
        /// </summary>
        public virtual IDbSet<Role> Roles { get; set; }

        /// <summary>
        ///     If true validates that emails are unique
        /// </summary>
        public bool RequireUniqueEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var users = modelBuilder.Entity<User>().ToTable("CustomUsers");
            users.HasKey(c => c.Id);
            users.Property(c => c.UserName).IsRequired().HasMaxLength(100).HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));
            users.Property(c => c.Email).IsRequired().HasMaxLength(256);
            users.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            users.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            users.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);

            var roles = modelBuilder.Entity<Role>().ToTable("CustomRoles");
            roles.HasKey(c => c.Id);
            roles.Property(c => c.Name).IsRequired().HasMaxLength(100).HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") { IsUnique = true }));
            roles.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);


            modelBuilder.Entity<UserRole>()
                .HasKey(r => new { r.UserId, r.RoleId })
                .ToTable("CustomUserRoles");

            modelBuilder.Entity<UserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("CustomUserLogins");

            modelBuilder.Entity<UserClaim>()
                .ToTable("CustomUserClaims");

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        ///     Validates that UserNames are unique and case insenstive
        /// </summary>
        /// <param name="entityEntry"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry,
            IDictionary<object, object> items)
        {
            if (entityEntry != null && entityEntry.State == EntityState.Added)
            {
                var errors = new List<DbValidationError>();
                var user = entityEntry.Entity as User;
                //check for uniqueness of user name and email
                if (user != null)
                {
                    if (Users.Any(u => String.Equals(u.UserName, user.UserName)))
                    {
                        errors.Add(new DbValidationError("User",
                            String.Format(CultureInfo.CurrentCulture, "Duplicate UserName", user.UserName)));
                    }
                    if (RequireUniqueEmail && Users.Any(u => String.Equals(u.Email, user.Email)))
                    {
                        errors.Add(new DbValidationError("User",
                            String.Format(CultureInfo.CurrentCulture, "Duplicate Email", user.Email)));
                    }
                }
                else
                {
                    var role = entityEntry.Entity as Role;
                    //check for uniqueness of role name
                    if (role != null && Roles.Any(r => String.Equals(r.Name, role.Name)))
                    {
                        errors.Add(new DbValidationError("Role",
                            String.Format(CultureInfo.CurrentCulture, "Role Already Exists", role.Name)));
                    }
                }
                if (errors.Any())
                {
                    return new DbEntityValidationResult(entityEntry, errors);
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }
    }
}
