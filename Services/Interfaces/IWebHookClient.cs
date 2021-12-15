using DTOs.Responses;
using DTOs.Responses.SingleVerifications;

namespace Services.Interfaces
{
    public interface IWebHookClient
    {
        public Task<(HttpResponseMessage Response, string url)> MakeHTTPRequest(string url, WebhookResponse sendHook);

    }
}
