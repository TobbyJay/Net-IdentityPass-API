using Data;
using DTOs.Requests;
using DTOs.Responses.BulkVerifications;
using DTOs.Responses.SingleVerifications;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Net_IdentityPass_API.Controllers
{
    [ApiController]   
    public class ServerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IBvnVerficationTypes _bvnVerificationTypes;
        private readonly ICreditBureauVerificationType _creditBureauVerificationType;
        private readonly IDriversLicenseVerificationType _driversLicenseVerificationType;
        private readonly IVinVerificationTypes vinVerificationTypes;
        private readonly INinVerificationTypes ninVerificationTypes;
        private readonly IWebHookClient _webHookClient;

        public ServerController(AppDbContext context, 
            IBvnVerficationTypes bvnVerificationTypes, 
            IWebHookClient webHookClient, ICreditBureauVerificationType creditBureauVerificationType, 
            IDriversLicenseVerificationType driversLicenseVerificationType,
            IVinVerificationTypes vinVerificationTypes,
            INinVerificationTypes ninVerificationTypes)
        {
            _context = context;
            _bvnVerificationTypes = bvnVerificationTypes;
            _webHookClient = webHookClient;
            _creditBureauVerificationType = creditBureauVerificationType;
            _driversLicenseVerificationType = driversLicenseVerificationType;
            this.vinVerificationTypes = vinVerificationTypes;
            this.ninVerificationTypes = ninVerificationTypes;
        }

        [HttpGet]
        [Route("/api/server/getinfofromsecretkey")]
        public IActionResult GetInfoFromKey([FromQuery] string secretKey)
        {
            var getInfo = _context.Settings
                         .Where(s => s.SecretKey == secretKey)
                         .FirstOrDefault();

            return Ok(getInfo);
        }
       
        [HttpPost]
        [Route("/api/server/verify/bvn")]
        public async Task<ActionResult> VerifyBvn([FromBody] VerificationRequest request)
        {
            var response = await _bvnVerificationTypes.VerfifyBvnInfoLevel2(request.BvnNumber, "test_231qza7t1kxejz21eg26e5:m1YlNf4sqfSQ0GEKnC8j2oZ-dyc", request.UserReferenceId);

            // process the webHook
            await _webHookClient.MakeHTTPRequest(request.Url, response);

            return Ok(response.Status);

           
        }

        [HttpPost]
        [Route("/api/server/verify/credit_bureau")]
        public async Task<ActionResult> VerifyCreditBureau([FromBody] VerificationRequest request)
        {
            var response = await _creditBureauVerificationType.VerifyCreditBureau(request.PhoneNumber, request.FirstName, "test_231qza7t1kxejz21eg26e5:m1YlNf4sqfSQ0GEKnC8j2oZ-dyc", request.UserReferenceId);

            // process the webHook
            await _webHookClient.MakeHTTPRequest(request.Url, response);

            return Ok(response.Status);


        }

        [HttpPost("/api/server/verify/nin")]
        public async Task<IActionResult> VerifyNin([FromBody] VerificationRequest request)
        {
            if (string.IsNullOrEmpty(request.NiNumber) || string.IsNullOrEmpty(request.Url)) return Ok(false);
            
            var response = await ninVerificationTypes.LookUpNin(request.NiNumber, "test_231qza7t1kxejz21eg26e5:m1YlNf4sqfSQ0GEKnC8j2oZ-dyc", request.UserReferenceId);

            // process the webHook
            await _webHookClient.MakeHTTPRequest(request.Url, response);

            return Ok(response.Status);
        }

        [HttpPost("/api/server/verify/vin")]
        public async Task<IActionResult> VerifyVin([FromBody] VerificationRequest request)
        {
            if (string.IsNullOrEmpty(request.ViNumber) || string.IsNullOrEmpty(request.Url)) return Ok(false);

            var response = vinVerificationTypes.LookUpVin(request.ViNumber, request.LastName, request.State, "test_231qza7t1kxejz21eg26e5:m1YlNf4sqfSQ0GEKnC8j2oZ-dyc", request.UserReferenceId);

            // process the webHook
            await _webHookClient.MakeHTTPRequest(request.Url, response);

            return Ok(response.Status);
        }

        [HttpPost]
        [Route("/api/server/verify/drivers_license")]
        public async Task<ActionResult> VerifyDriversLicense([FromBody] VerificationRequest request)
        {
            var response = await _driversLicenseVerificationType.VerfifyDriversLicense(request.Dob, request.FrscNumber, "test_231qza7t1kxejz21eg26e5:m1YlNf4sqfSQ0GEKnC8j2oZ-dyc", request.UserReferenceId);

            // process the webHook
            await _webHookClient.MakeHTTPRequest(request.Url, response);

            return Ok(response.Status);


        }

        [HttpPost]
        [Route("/api/server/verify/bulk_verification")]
        public async Task<ActionResult> BulkVerification([FromBody] VerificationRequest request)
        {
            // coming back to clean this code like my life depends on it, it was all just vibes and inshal allah ooo

            var getTypes = GetVerificationTypes(request.Type.ToLower());

            var bvnResponse = new BvnResponse();
            var driverLicenseResponse = new DriverseLicense();
            var creditBureauResponse = new CreditBureau();

            foreach (var type in getTypes)
            {
                if (type.Trim() == "bvn")
                {
                   bvnResponse = await _bvnVerificationTypes.VerfifyBvnInfoLevel2(request.BvnNumber, "test_231qza7t1kxejz21eg26e5:m1YlNf4sqfSQ0GEKnC8j2oZ-dyc", request.UserReferenceId);

                }else if(type.Trim() == "drivers license")
                {
                    driverLicenseResponse = await _driversLicenseVerificationType.VerfifyDriversLicense(request.Dob, request.FrscNumber, "test_231qza7t1kxejz21eg26e5:m1YlNf4sqfSQ0GEKnC8j2oZ-dyc", request.UserReferenceId);

                }
            }

            var response = new BulkResponse
            {
                Message = "bulk verification successful",
                Status = bvnResponse.Status,
                Bvn = bvnResponse,
                DriverseLicense = driverLicenseResponse,
            };

            // process the webHook
            await _webHookClient.MakeHTTPRequest(request.Url, response);

            return Ok(response.Status);

        }

        private string[] GetVerificationTypes(string type)
        {
            String[] seperator = { "," };
            String[] splitEvent = type.Split(seperator,
               StringSplitOptions.RemoveEmptyEntries);

            return splitEvent;

        }
    }
}
