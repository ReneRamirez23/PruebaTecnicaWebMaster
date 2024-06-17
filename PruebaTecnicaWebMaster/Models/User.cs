using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace PruebaTecnicaWebMaster.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string TypeUser { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;

    }

}
