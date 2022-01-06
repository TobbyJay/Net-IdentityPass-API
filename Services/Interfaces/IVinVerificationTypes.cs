using DTOs.Responses.SingleVerifications;

namespace Services.Interfaces
{
    public interface IVinVerificationTypes
    {
        public Task<VinResponse> LookUpVin(string number, string last_name, string state, string secretKey, string referenceId);

    }
}
