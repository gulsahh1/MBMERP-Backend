using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }
    }
}
