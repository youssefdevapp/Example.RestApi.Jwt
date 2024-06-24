﻿using System.ComponentModel.DataAnnotations;

namespace Users.Core
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}