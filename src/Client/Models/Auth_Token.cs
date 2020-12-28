﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Auth_Token
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
