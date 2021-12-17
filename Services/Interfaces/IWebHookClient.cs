using DTOs.Responses;
using DTOs.Responses.BulkVerifications;
using DTOs.Responses.SingleVerifications;

namespace Services.Interfaces
{
    public interface IWebHookClient
    {
        public Task<(HttpResponseMessage Response, string url)> MakeHTTPRequest(string url, Object sendHook);

        public Task<(HttpResponseMessage Response, string url)> MakeCreditbureauVerificationHTTPRequest(string url, CreditBureau sendHook);

        public Task<(HttpResponseMessage Response, string url)> MakeDriversLicenseVerificationHTTPRequest(string url, DriverseLicense sendHook);

        public Task<(HttpResponseMessage Response, string url)> MakeBulkVerificationHTTPRequest(string url, BulkResponse sendHook);

    }
}
