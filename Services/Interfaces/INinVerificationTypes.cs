using DTOs.Responses.SingleVerifications;

namespace Services.Interfaces
{
    public interface INinVerificationTypes
    {
        public Task<NinResponse> LookUpNin(string number, string secretKey, string referenceId);

    }
}
