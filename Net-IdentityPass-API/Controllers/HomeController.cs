using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using Entities;
using IdentityPassTestLibrary.V1.API.Interfaces;


namespace Net_IdentityPass_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBvnVerificationTypes bvnVerificationTypes;
        public HomeController(IBvnVerificationTypes bvnVerificationTypes)
        {
            this.bvnVerificationTypes = bvnVerificationTypes;
        }
        public IActionResult Index()
        {
            var bvn = bvnVerificationTypes.VerfifyBvnInfoLevel2("54651333604", "test_231qza7t1kxejz21eg26e5:m1YlNf4sqfSQ0GEKnC8j2oZ-dyc", false);

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
