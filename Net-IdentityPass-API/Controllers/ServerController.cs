using Data;
using DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Net_IdentityPass_API.Controllers
{
    [ApiController]   
    public class ServerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IBvnVerficationTypes _bvnVerificationTypes;
        private readonly IWebHookClient _webHookClient;

        public ServerController(AppDbContext context, IBvnVerficationTypes bvnVerificationTypes, IWebHookClient webHookClient)
        {
            _context = context;
            _bvnVerificationTypes = bvnVerificationTypes;
            _webHookClient = webHookClient;
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
            var response = await _bvnVerificationTypes.VerfifyBvnInfoLevel2(request.Number, "test_231qza7t1kxejz21eg26e5:m1YlNf4sqfSQ0GEKnC8j2oZ-dyc");

            // process the webHook
            await _webHookClient.MakeHTTPRequest(request.Url, response);

            return Ok(response);

           
        }
    }
}
