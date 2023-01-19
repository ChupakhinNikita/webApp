using System;
using System.Collections.Generic;

namespace webApp.Models
{
    public partial class User
    {
        public int IdUser { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
