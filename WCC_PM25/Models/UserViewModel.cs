using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WCC_PM25.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Keyword { get; set; }

        public string Page { get; set; }

        public string Operate { get; set; }
        public string NextGroup { get; set; }

        public NewUser NewUser { get; set; }
    }

    public class NewUser
    {
        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
