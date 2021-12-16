using DTOs.Responses;
using DTOs.Responses.SingleVerifications;

namespace Services.Interfaces
{
    public interface IWebHookClient
    {
        public Task<(HttpResponseMessage Response, string url)> MakeBvnVeririfcationHTTPRequest(string url, BvnResponse sendHook);

        public Task<(HttpResponseMessage Response, string url)> MakeCreditbureauVerificationHTTPRequest(string url, CreditBureau sendHook);

        public Task<(HttpResponseMessage Response, string url)> MakeDriversLicenseVerificationHTTPRequest(string url, DriverseLicense sendHook);

    }
}
