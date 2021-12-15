using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Requests
{
    public class VerificationRequest
    {
        public string Type { get; set; }
        public string Number { get; set; }
        public string Url { get; set; }

    }
}
