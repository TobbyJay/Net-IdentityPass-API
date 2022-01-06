using DTOs.Responses.SingleVerifications;


namespace Services.Interfaces
{
    public interface IDriversLicenseVerificationType
    {
        public Task<DriverseLicense> VerfifyDriversLicense(string dob, string number, string secretKey, string referenceId);
    }
}
