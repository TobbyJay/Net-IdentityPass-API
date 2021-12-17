using Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Net_IdentityPass_API.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Settings(Settings settings)
        {

            var secretKey = GenerateSecretKey();

            // get and save company name and url
            if (!ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(settings.VerificationTypes))
                {

                    var setting = new Settings
                    {
                        CompanyName = settings.CompanyName,
                        SecretKey = secretKey,
                        WebHookUrl = settings.WebHookUrl,
                        VerificationTypes = ""
                    };
                    _context.Add(setting);
                    _context.SaveChanges();
                }
                else
                {
                    var setting = new Settings
                    {
                        CompanyName = settings.CompanyName,
                        SecretKey = secretKey,
                        WebHookUrl = settings.WebHookUrl,
                        VerificationTypes = settings.VerificationTypes
                    };
                    _context.Add(setting);
                    _context.SaveChanges();
                }
              

            }

            ViewBag.SecretKey = $"Your secret Key is: {secretKey}";
            return View();
        }

        private string GenerateSecretKey()
        {
            var random = new Random();

            var getRandomNumbers = random.Next(1000, 100000);
            var guid = Guid.NewGuid();

            var key = $"{guid}-{getRandomNumbers}";
            var getSecretKey = Regex.Replace(key, "-", String.Empty);

            return getSecretKey;
        }
    }
}
