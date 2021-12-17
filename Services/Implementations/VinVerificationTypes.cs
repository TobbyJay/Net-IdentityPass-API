using DTOs.Responses;
using DTOs.Responses.SingleVerifications;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class VinVerificationTypes : IVinVerificationTypes
    {
        private readonly JsonSerializerOptions _options;
        private bool disposedValue;
        private HttpClient _httpClient;
        private readonly IWebHookClient _webHookClient;

        public VinVerificationTypes(IWebHookClient webHookClient)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _httpClient = new HttpClient();
            _webHookClient = webHookClient;
        }

        public async Task<VinResponse> LookUpVin(string number, string last_name, string state, string secretKey, string referenceId)
        {
            var value = new Dictionary<string, string>
            {
                { "number", number},
                { "last_name", last_name},
                { "state", state}
            };

            var url = $"https://sandbox.myidentitypass.com/api/v1/biometrics/merchant/data/verification/voters_card";

            var result = await GetHttpClientSetup(url, value, secretKey);

            var response = JsonSerializer.Deserialize<LookUpVinResponse>(result, _options);

            var verificationDetails = new ClientResponse<LookUpVinResponse>
            {
                Value = response
            };

            var res = new VinResponse
            {
                UserReferenceId = referenceId,
                Response = verificationDetails,
                Status = verificationDetails.Value.Status,
                Message = verificationDetails.Value.Detail
            };

            return res;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _httpClient.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private async Task<string> GetHttpClientSetup(string url, Dictionary<string, string> value, string secretKey)
        {

            var content = new FormUrlEncodedContent(value);

            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            content.Headers.Add("x-api-key", secretKey);

            HttpResponseMessage response = await _httpClient.PostAsync(new Uri(url), content);

            string result = await response.Content.ReadAsStringAsync();
            _httpClient.Dispose();
            return result;
        }
    }
}
