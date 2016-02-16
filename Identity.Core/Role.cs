using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core
{
    public class Role : IRole<int>//IdentityRole<int, UserRole>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        private List<UserRole> _users;

        public virtual ICollection<UserRole> Users
        {
            get { return _users ?? (_users = new List<UserRole>()); }
        }
    }
}
