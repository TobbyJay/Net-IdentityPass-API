using DTOs.Responses.SingleVerifications;


namespace Services.Interfaces
{
    public interface IBvnVerficationTypes
    {
        public Task<BvnResponse> VerfifyBvnInfoLevel2(string number, string secretKey, string referenceId);

    }
}
