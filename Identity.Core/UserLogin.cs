using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core
{
    public class UserLogin
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        public int UserId { get; set; }
    }
}
