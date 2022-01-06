

namespace DTOs.Responses.SingleVerifications
{
    public class BvnResponse
    {
        public string WebhookUrl { get; set; }
        public string UserReferenceId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public BvnVerificationLevelTwo Response { get; set; }

    }
}
