﻿using Data;
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
        private readonly IWebHookClient _webHookClient;

        public ServerController(AppDbContext context, IBvnVerficationTypes bvnVerificationTypes, IWebHookClient webHookClient, ICreditBureauVerificationType creditBureauVerificationType, IDriversLicenseVerificationType driversLicenseVerificationType)
        {
            _context = context;
            _bvnVerificationTypes = bvnVerificationTypes;
            _webHookClient = webHookClient;
            _creditBureauVerificationType = creditBureauVerificationType;
            _driversLicenseVerificationType = driversLicenseVerificationType;
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
            await _webHookClient.MakeBvnVeririfcationHTTPRequest(request.Url, response);

            return Ok(response);

           
        }

        [HttpPost]
        [Route("/api/server/verify/credit_bureau")]
        public async Task<ActionResult> VerifyCreditBureau([FromBody] VerificationRequest request)
        {
            var response = await _creditBureauVerificationType.VerifyCreditBureau(request.PhoneNumber, request.FirstName, "test_231qza7t1kxejz21eg26e5:m1YlNf4sqfSQ0GEKnC8j2oZ-dyc", request.UserReferenceId);

            // process the webHook
            await _webHookClient.MakeCreditbureauVerificationHTTPRequest(request.Url, response);

            return Ok(response);


        }

        [HttpPost]
        [Route("/api/server/verify/drivers_license")]
        public async Task<ActionResult> VerifyDriversLicense([FromBody] VerificationRequest request)
        {
            var response = await _driversLicenseVerificationType.VerfifyDriversLicense(request.Dob, request.FrscNumber, "test_231qza7t1kxejz21eg26e5:m1YlNf4sqfSQ0GEKnC8j2oZ-dyc", request.UserReferenceId);

            // process the webHook
            await _webHookClient.MakeDriversLicenseVerificationHTTPRequest(request.Url, response);

            return Ok(response);


        }

        [HttpPost]
        [Route("/api/server/verify/bulk_verification")]
        public async Task<ActionResult> BulkVerification([FromBody] VerificationRequest request)
        {
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
                Bvn = bvnResponse,
                DriverseLicense = driverLicenseResponse,
            };

            // process the webHook
            await _webHookClient.MakeBulkVerificationHTTPRequest(request.Url, response);

            return Ok(response);


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
