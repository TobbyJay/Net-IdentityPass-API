

namespace Services.Interfaces
{
    public interface IWebHookClient
    {
        public Task<(HttpResponseMessage Response, string url)> MakeHTTPRequest(string url, Object sendHook);

    }
}
