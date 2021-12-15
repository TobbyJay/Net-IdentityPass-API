﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Client
    {
        public Guid ClientId { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}