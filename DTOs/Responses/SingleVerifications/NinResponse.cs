﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Responses.SingleVerifications
{
    public class NinResponse
    {
        public string WebhookUrl { get; set; }
        public string UserReferenceId { get; set; }
        public LookUpNinResponse Response { get; set; }
        public bool Status { get; set; }
        public string Details { get; set; }
    }
}