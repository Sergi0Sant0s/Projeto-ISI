using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class UserRoles
    {
        public UserRoles(User _user, List<string> _roles)
        {
            this.User = _user;
            this.Roles = _roles;
        }

        public User User { get; }
        public List<string> Roles { get; }
    }
}
