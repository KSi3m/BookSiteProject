﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSiteProject.Infrastructure.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public bool MustChangePassword { get; set; } = false;
    }
}
