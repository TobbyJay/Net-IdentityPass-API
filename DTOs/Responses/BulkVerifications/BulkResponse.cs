using DTOs.Responses.SingleVerifications;

namespace DTOs.Responses.BulkVerifications
{
    public class BulkResponse
    {
        public BvnResponse Bvn { get; set; }
        public DriverseLicense DriverseLicense { get; set; }
        public CreditBureau CreditBureau { get; set; }
        public VinResponse Vin { get; set; }
        public NinResponse Nin { get; set; }
        public string WebhookUrl { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }




    }
}
