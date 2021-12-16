using DTOs.Responses.SingleVerifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Responses.BulkVerifications
{
    public class BulkResponse
    {
        public BvnResponse Bvn { get; set; }
        public DriverseLicense DriverseLicense { get; set; }
        public CreditBureau CreditBureau { get; set; }
        public string WebhookUrl { get; set; }
        public string Message { get; set; }



    }
}
