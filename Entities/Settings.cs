using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class Settings
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string WebHookUrl { get; set; }
        public string SecretKey { get; set; }
        public string VerificationTypes { get; set; }

    }
}