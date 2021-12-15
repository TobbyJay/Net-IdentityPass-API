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
    public class BvnVerficationTypes : IBvnVerficationTypes
    {
        private readonly JsonSerializerOptions _options;
        private bool disposedValue;
        private HttpClient _httpClient;
        private readonly IWebHookClient _webHookClient;

        public BvnVerficationTypes(IWebHookClient webHookClient)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _httpClient = new HttpClient();
            _webHookClient = webHookClient;
        }
        public async Task<Response> VerfifyBvnInfoLevel1(string number, string secretKey)
        {
            var value = new Dictionary<string, string>
            {
                { "number", number}
            };

            var url = $"https://sandbox.myidentitypass.com/api/v1/biometrics/merchant/data/verification/bvn_validation";

            var result = await GetHttpClientSetup(url, value, secretKey);

            var verificationDetails = JsonSerializer.Deserialize<Response>(result, _options);

            var verify = new Response
            {
                Status = "Success",
                Message = "Bvn Level 1 verification successful"
            };

            return verify;

        }

        public async Task<WebhookResponse> VerfifyBvnInfoLevel2(string number, string secretKey)
        {
            var value = new Dictionary<string, string>
            {
                { "number", number}
            };

            var url = $"https://sandbox.myidentitypass.com/api/v1/biometrics/merchant/data/verification/bvn";

            var result = await GetHttpClientSetup(url, value, secretKey);

            var verificationDetails = JsonSerializer.Deserialize<BvnVerificationLevelTwo>(result, _options);

            var verify = new WebhookResponse
            {
                Status = "success",
                Message = "Bvn Level 1 verification successful"
            };

            return verify;
        }

        public async Task<Response> VerfifyBvnInfoWithFace(string number, string image, string secretKey)
        {
            var value = new Dictionary<string, string>
            {
                { "number", number},
                {"image", image}
            };

            var url = $"https://sandbox.myidentitypass.com/api/v1/biometrics/merchant/data/verification/bvn_w_face";

            var result = await GetHttpClientSetup(url, value, secretKey);

            var verificationDetails = JsonSerializer.Deserialize<Response>(result, _options);


            var verify = new Response
            {
                Status = "Success",
                Message = "Bvn Level 1 verification successful"
            };

            return verify;
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
