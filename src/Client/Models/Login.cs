using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Login
    {
        [Required(ErrorMessage = "User Name é obrigatório")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password é obrigatório")]
        public string Password { get; set; }
    }
}
