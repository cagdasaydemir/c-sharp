﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleBlog.Models.Models
{

    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

    }

}
