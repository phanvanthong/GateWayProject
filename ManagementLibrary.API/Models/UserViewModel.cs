﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetWay.API.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool Remember { get; set; }

    }
}