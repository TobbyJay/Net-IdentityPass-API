using System.Text.Json;

using DTOs.Responses;
using DTOs.Responses.SingleVerifications;
using Services.Interfaces;

namespace Services.Implementations
{
    public class CreditBureauVerificationType : ICreditBureauVerificationType
    {
        private readonly JsonSerializerOptions _options;
        private bool disposedValue;
        private HttpClient _httpClient;
        public CreditBureauVerificationType()
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _httpClient = new HttpClient();
        }
        public async Task<CreditBureau> VerifyCreditBureau(string phoneNumber, string firstName, string secretKey, string referenceId)
        {
            var value = new Dictionary<string, string>
            {
                { "phone_number", phoneNumber },
                { "first_name", firstName}
            };

            var url = $"https://sandbox.myidentitypass.com/api/v1/biometrics/merchant/data/verification/credit_bureau";

            var result = await GetHttpClientSetup(url, value, secretKey);

            var response = JsonSerializer.Deserialize<CreditBureauResponse>(result, _options);

            var verificationDetails = new ClientResponse<CreditBureauResponse>
            {
                Value = response,
            };

            var res = new CreditBureau
            {
                Status = verificationDetails.Value.Status,
                Message = verificationDetails.Value.Detail,
                UserReferenceId = referenceId,
                Response = verificationDetails
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
