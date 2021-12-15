using Data;
using Microsoft.AspNetCore.Mvc;

namespace Net_IdentityPass_API.Controllers
{
    [ApiController]   
    public class ServerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ServerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/api/server/getinfofromsecretkey")]
        public IActionResult GetInfoFromKey(string secretKey)
        {
            return Ok(new List<string>());
        }
    }
}
