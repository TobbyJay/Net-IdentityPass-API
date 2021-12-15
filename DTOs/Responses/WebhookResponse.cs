using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Responses
{
    public class WebhookResponse
    {
        public string Status { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
    }
}
