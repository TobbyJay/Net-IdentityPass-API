using DTOs.Responses.SingleVerifications;

namespace Services.Interfaces
{
    public interface ICreditBureauVerificationType
    {
        public Task<CreditBureau> VerifyCreditBureau(string phoneNumber, string firstName, string secretKey, string referenceId);

    }
}
