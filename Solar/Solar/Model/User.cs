using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solar.Model
{
    // Object to represent a user from text fields of login page
    class User
    {
        public string email { get; set; }
        public string password { get; set; }
        public string grant_type { get; set; }
    }
}
