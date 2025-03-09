using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public enum Roles
    {
        customer,
        deliver,
        owner,
        admin
    }
    public class User

    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Identity { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }

    }
}
