using DTOs.Responses;
using DTOs.Responses.SingleVerifications;
using Services.Interfaces;
using System.Text.Json;

namespace Services.Implementations
{
    public class DriversLicenseVerificationType : IDriversLicenseVerificationType
    {
        private readonly JsonSerializerOptions _options;
        private bool disposedValue;
        private HttpClient _httpClient;
        public DriversLicenseVerificationType()
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _httpClient = new HttpClient();
        }

        public async Task<DriverseLicense> VerfifyDriversLicense(string dob, string number, string secretKey, string referenceId)
        {
            var value = new Dictionary<string, string>
            {
                { "dob", dob },
                { "number", number}
            };

            var url = $"https://sandbox.myidentitypass.com/api/v1/biometrics/merchant/data/verification/drivers_license";

            var result = await GetHttpClientSetup(url, value, secretKey);

            var response = JsonSerializer.Deserialize<DriverseLicenseResponse>(result, _options);

            var verificationDetails = new ClientResponse<DriverseLicenseResponse>
            {
                Value = response,
            };

            var res = new DriverseLicense
            {
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
